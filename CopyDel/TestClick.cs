﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace CopyDel
{
    public partial class TestClick : Form
    {
        [DllImport("User32.dll")]
        static extern int GetSystemMetrics(int nIndex);
        [DllImport("User32.dll")]
        static extern void mouse_event(MouseFlags dwFlags, int dx, int dy, int dwData, UIntPtr dwExtraInfo);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDc, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);
        [DllImport("gdi32.dll")]
        public static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        //для удобства использования создаем перечисление с необходимыми флагами (константами), которые определяют действия мыши: 
        [Flags]
        enum MouseFlags
        {
            Move = 0x0001,
            LeftDown = 0x0002, LeftUp = 0x0004,
            MIDDLEDOWN = 0x0020, MIDDLEUP = 0x0040,
            RightDown = 0x0008, RightUp = 0x0010,
            XDOWN = 0x0080, XUP = 0x0100,
            WHEEL = 0x0800, HWHEEL = 0x01000,
            Absolute = 0x8000
        };

        public TestClick() => InitializeComponent();

        private int SCREENX = -1;
        private int SCREENY = -1;
        private void GetRezolution()
        {
            SCREENX = GetSystemMetrics(0);
            SCREENY = GetSystemMetrics(1);
        }
        private void TestBtn1_Click(object sender, EventArgs e)
        {
            // Получаем разрешение экрана
            GetRezolution();

            double dx = 65536 / (double)SCREENX;
            double dy = 65536 / (double)SCREENY;

            // и использование - клик левой примерно в центре экрана
            // (подробнее о координатах, передаваемых в mouse_event см. в MSDN): 
            const int x = 32000;
            const int y = 32000;

            Cursor.Position = new Point(x, y);
            Thread.Sleep(500);
            mouse_event(MouseFlags.Absolute | MouseFlags.Move, x, y, 0, UIntPtr.Zero);
            mouse_event(MouseFlags.Absolute | MouseFlags.LeftDown, x, y, 0, UIntPtr.Zero);
            mouse_event(MouseFlags.Absolute | MouseFlags.Move, x + 20000, y + 1000, 0, UIntPtr.Zero);
            mouse_event(MouseFlags.Absolute | MouseFlags.LeftUp, x, y, 0, UIntPtr.Zero);
            mouse_event(MouseFlags.Absolute | MouseFlags.RightUp, x, y, 0, UIntPtr.Zero);
        }
        private List<RawColor> RawColorList = new List<RawColor>();

        public class Window
        {
            public int LeftPoint { get; set; } = -1;
            public int RightPoint { get; set; } = -1;
            public int Up { get; set; } = -1;
            public int Dn { get; set; } = -1;
            public int GetX() => (RightPoint + LeftPoint) / 2;
            public int GetY() => (Up + Dn) / 2;
            public bool Check() => LeftPoint > 0 && RightPoint > 0 && Up > 0 && Dn > 0 && Up < Dn && LeftPoint < RightPoint;
        }

        public RawColor WindowColor = new RawColor(31);
        private void TestBtn2_Click(object sender, EventArgs e)
        {
            FindWindow();
        }
        private void FindWindow()
        {
            GetRezolution();
            picBox.Image = null;
            picBox.BackColor = Color.Black;
            //RTB.Text = null;
            string text = string.Empty;
            IntPtr desktopPtr = GetDesktopWindow();
            IntPtr hdc = GetDC(desktopPtr);

            Window window = new Window();
            window.LeftPoint = FindLeftPoint(hdc);
            window.RightPoint = FindRightPoint(hdc);
            int X = (window.LeftPoint + window.RightPoint) / 2;

            window.Up = FindUpPoint(hdc, X);
            window.Dn = FindDnPoint(hdc, X);
            var check = window.Check();

            ReleaseDC(desktopPtr, hdc);
            List<int> ints = new List<int>();
            ints.Add(window.LeftPoint);
            ints.Add(window.RightPoint);
            ints.Add(window.Up);
            ints.Add(window.Dn);
        }

        private int LeftPoint = 96;
        private int RightPoint = 249;
        private int Up = 7;
        private int Dn = 1069;

        private void button1_Click(object sender, EventArgs e)
        {
            Scaner scaner = new Scaner(LeftPoint, RightPoint, Up, Dn);
            var rawColor = scaner.GetLine(EnumDirection.Dn);
            if (rawColor.Count != 0)
            {
                Bitmap myBitmap = new Bitmap(picBox.Width, picBox.Height, PixelFormat.Format32bppArgb);
                int to = rawColor.Count > picBox.Height ? picBox.Height - 1 : rawColor.Count;
                for (int i = 0; i < to; i++)
                {
                    myBitmap.SetPixel(picBox.Width/2 - 1, i, Color.FromArgb(250, rawColor[i].R, rawColor[i].G, rawColor[i].B));
                    myBitmap.SetPixel(picBox.Width/2, i, Color.FromArgb(250, rawColor[i].R, rawColor[i].G, rawColor[i].B));
                    myBitmap.SetPixel(picBox.Width/2 + 1, i, Color.FromArgb(250, rawColor[i].R, rawColor[i].G, rawColor[i].B));
                }

                picBox.Image = myBitmap;
            }
        }

        public class Scaner
        {
            public int LeftPoint { get; set; } = -1;
            public int RightPoint { get; set; } = -1;
            public int Up { get; set; } = -1;
            public int Dn { get; set; } = -1;

            public Scaner(int leftPoint, int rightPoint, int up, int dn)
            {
                LeftPoint = leftPoint;
                RightPoint = rightPoint;
                Up = up;
                Dn = dn;
            }

            public Scaner()
            {
                LeftPoint = 0;
                RightPoint = GetSystemMetrics(0);
                Up = 0;
                Dn = GetSystemMetrics(1);
            }

            public void ChangeArea(int leftPoint, int rightPoint, int up, int dn)
            {
                LeftPoint = leftPoint;
                RightPoint = rightPoint;
                Up = up;
                Dn = dn;
            }
            public void ChangeArea()
            {
                LeftPoint = 0;
                RightPoint = GetSystemMetrics(0);
                Up = 0;
                Dn = GetSystemMetrics(1);
            }

            public int GetX() => (RightPoint + LeftPoint) / 2;
            public int GetY() => (Up + Dn) / 2;

            public bool Check() => LeftPoint > 0 && RightPoint > 0 && Up > 0 && Dn > 0 && Up < Dn && LeftPoint < RightPoint;
            public List<RawColor> GetLine(EnumDirection Direction)
            {
                if (!Check()) return new List<RawColor>();
                if (Direction == EnumDirection.Dn)
                {
                    int X = GetX();
                    return GetLine(X, Up, X, Dn);
                }
                else return new List<RawColor>();
            }
            public List<RawColor> GetLine(int fx, int fy, int tx, int ty)
            {
                List<RawColor> rawList = new List<RawColor>();
                IntPtr desktopPtr = GetDesktopWindow();
                IntPtr hdc = GetDC(desktopPtr);
                if (fx == tx)
                {
                    for (int Y = fy; Y < ty; Y++)
                    {
                        uint pixel = GetPixel(hdc, fx, Y);
                        rawList.Add(new RawColor((byte)(pixel & 0x000000FF), (byte)((pixel & 0x0000FF00) >> 8), (byte)((pixel & 0x00FF0000) >> 16)));
                    }
                }
                else
                {

                }

                return rawList;
            }
        }

        public enum EnumDirection
        {
            Und,
            Lt,
            Rt,
            Up,
            Dn
        }
        private int FindDnPoint(IntPtr hdc, int X)
        {
            //string text = string.Empty;
            RawColorList.Clear();
            int Width = SCREENX / 2 + 1, Height = SCREENY / 2 + 1;
            Bitmap myBitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);

            int q = Height-1, findCounter = 0, point = 0;
            bool isFind = false;

            for (int Y = SCREENY - 1; Y >0; Y--)
            {
                uint pixel = GetPixel(hdc, X, Y);
                RawColor rawColor = new RawColor((byte)(pixel & 0x000000FF), (byte)((pixel & 0x0000FF00) >> 8), (byte)((pixel & 0x00FF0000) >> 16));
                myBitmap.SetPixel( Width / 4 - 1, q, Color.FromArgb(250, rawColor.R, rawColor.G, rawColor.B));
                myBitmap.SetPixel( Width / 4, q, Color.FromArgb(250, rawColor.R, rawColor.G, rawColor.B));
                myBitmap.SetPixel( Width / 4 + 1, q, Color.FromArgb(250, rawColor.R, rawColor.G, rawColor.B));
                RawColorList.Add(new RawColor(rawColor.R, rawColor.G, rawColor.B));
                q--;
                if (q <0)
                {
                    q = Height - 1;
                    Width += 100;
                }

                if (rawColor.Equals(WindowColor)) isFind = true;
                else isFind = false;

                if (isFind) findCounter++;
                else findCounter = 0;

                if (findCounter > 2)
                {
                    point = Y - 1;
                    break;
                }
            }

            //RawColorList.Select(x => text += x.ToString() + "\n").ToArray();
            //RTB.Text = text;
            picBox.Image = myBitmap;
            return point;
        }

        private int FindUpPoint(IntPtr hdc, int X)
        {
            //string text = string.Empty;
            RawColorList.Clear();
            int Width = SCREENX / 2 + 1, Height = SCREENY / 2 + 1;
            //Bitmap myBitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);

            int  q = 0, findCounter = 0, point = 0;
            bool isFind = false;

            for (int Y = 0; Y < Height; Y++)
            {
                uint pixel = GetPixel(hdc, X, Y);
                RawColor rawColor = new RawColor((byte)(pixel & 0x000000FF), (byte)((pixel & 0x0000FF00) >> 8), (byte)((pixel & 0x00FF0000) >> 16));

                //myBitmap.SetPixel( Width / 4 - 1, q, Color.FromArgb(250, rawColor.R, rawColor.G, rawColor.B));
                //myBitmap.SetPixel( Width / 4, q, Color.FromArgb(250, rawColor.R, rawColor.G, rawColor.B));
                //myBitmap.SetPixel( Width / 4 + 1, q, Color.FromArgb(250, rawColor.R, rawColor.G, rawColor.B));
                RawColorList.Add(new RawColor(rawColor.R, rawColor.G, rawColor.B));
                q++;
                if (q > Height-1)
                {
                    q =0;
                    Width += 100;
                }

                if (rawColor.Equals(WindowColor)) isFind = true;
                else isFind = false;

                if (isFind) findCounter++;
                else findCounter = 0;

                if (findCounter > 2)
                {
                    point = Y - 1;
                    break;
                }
            }

            //RawColorList.Select(x => text += x.ToString() + "\n").ToArray();
            //RTB.Text = text;
            //picBox.Image = myBitmap;
            return point;
        }

        private int FindRightPoint(IntPtr hdc)
        {
            //string text = string.Empty;
            RawColorList.Clear();
            int Width = SCREENX / 2 + 1, Height = SCREENY / 2 + 1;
            Bitmap myBitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            
            int Y = SCREENY/2, q = Width-1, findCounter = 0, point = 0;
            bool isFind = false;

            for (int X = Width; X >0; X--)
            {
                uint pixel = GetPixel(hdc, X, Y);
                RawColor rawColor = new RawColor((byte)(pixel & 0x000000FF), (byte)((pixel & 0x0000FF00) >> 8), (byte)((pixel & 0x00FF0000) >> 16));

                myBitmap.SetPixel(q, Height / 2 - 1, Color.FromArgb(250, rawColor.R, rawColor.G, rawColor.B));
                myBitmap.SetPixel(q, Height / 2, Color.FromArgb(250, rawColor.R, rawColor.G, rawColor.B));
                myBitmap.SetPixel(q, Height / 2 + 1, Color.FromArgb(250, rawColor.R, rawColor.G, rawColor.B));
                RawColorList.Add(new RawColor(rawColor.R, rawColor.G, rawColor.B));
                q--;
                if (q < 1)
                {
                    q = Width-1;
                    Height += 100;
                }

                if (rawColor.Equals(WindowColor)) isFind = true;
                else isFind = false;

                if (isFind) findCounter++;
                else findCounter = 0;

                if (findCounter > 2)
                {
                    point = X + 1;
                    break;
                }
            }

            //RawColorList.Select(x => text += x.ToString() + "\n").ToArray();
            //RTB.Text = text;
            picBox.Image = myBitmap;
            return point;
        }

        private int FindLeftPoint(IntPtr hdc)
        {
            string text = string.Empty;
            RawColorList.Clear();
            int Width = SCREENX / 2 + 1, Height = SCREENY / 2 + 1;
            Bitmap myBitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);

            int Y = SCREENY / 2, q = 0, findCounter = 0, point = 0;
            bool isFind = false;

            for (int X = 50; X < SCREENX / 4; X++)
            {
                uint pixel = GetPixel(hdc, X, Y);
                RawColor rawColor = new RawColor((byte)(pixel & 0x000000FF), (byte)((pixel & 0x0000FF00) >> 8), (byte)((pixel & 0x00FF0000) >> 16));

                myBitmap.SetPixel(q, Height / 2 - 1, Color.FromArgb(250, rawColor.R, rawColor.G, rawColor.B));
                myBitmap.SetPixel(q, Height / 2, Color.FromArgb(250, rawColor.R, rawColor.G, rawColor.B));
                myBitmap.SetPixel(q, Height / 2 + 1, Color.FromArgb(250, rawColor.R, rawColor.G, rawColor.B));
                RawColorList.Add(new RawColor(rawColor.R, rawColor.G, rawColor.B));
                q++;

                if (rawColor.Equals(WindowColor)) isFind = true;
                else isFind = false;

                if (isFind) findCounter++;
                else findCounter = 0;

                if (findCounter > 2)
                {
                    point = X - 1;
                    break;
                }
            }

            //RawColorList.Select(x => text += x.ToString() + "\n").ToArray();
            //RTB.Text = text;
            picBox.Image = myBitmap;
           
            return point;
        }
        public struct RawColor
        {
            public readonly byte R, G, B;
            public RawColor(byte r, byte g, byte b)
            {
                //(R, G, B) = (r, g, b);
                R = r;
                G = g;
                B = b;
            }
            public RawColor( byte gray)
            {
                R = gray;
                G = gray;
                B = gray;
            }
            public static RawColor Random(Random rand)
            {
                byte r = (byte)rand.Next(256);
                byte g = (byte)rand.Next(256);
                byte b = (byte)rand.Next(256);
                return new RawColor(r, g, b);
            }
            public override bool Equals(object obj)
            {
                // Check for null and type compatibility
                if (obj == null || GetType() != obj.GetType())
                {
                    return false;
                }

                // Compare property values
                RawColor other = (RawColor)obj;
                return R == other.R && G == other.G && B == other.B;
            }
            public override string ToString()=> R.ToString()+" "+ G.ToString() + " "+ B.ToString() + " ";
        }
    }
}