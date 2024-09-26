using UnmanagedCode.Enum;
using UnmanagedCode.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;
//https://habr.com/ru/articles/316012/
namespace UnmanagedCode
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

        public TestClick() => InitializeComponent();

        private int SCREENX = -1;
        private int SCREENY = -1;
        private void GetRezolution()
        {
            SCREENX = GetSystemMetrics(0);
            SCREENY = GetSystemMetrics(1);
        }

        int N = 15000;

        private void TestBtn1_Click(object sender, EventArgs e)
        {
            N = 15000;
            int D = 4500;
            for (int i = 0; i < 5; i++)
            {
                Moov().Wait();
                N -= D;
            }
        }

        async Task<bool> Moov()
        {
            // Получаем разрешение экрана
            GetRezolution();
            int xfr = (wind.LP + wind.RP) / 2;
            int yfr = (wind.MiddleRandomCellDn + wind.MiddleRandomCellUp) / 2;
            int xto = (wind.LP + wind.RP) / 2;
            int yto = (wind.FirstCellUp + wind.LastCellDn) / 2;

            double dx = 65536 / (double)SCREENX;
            double dy = 65536 / (double)SCREENY;

            // и использование - клик левой примерно в центре экрана
            // (подробнее о координатах, передаваемых в mouse_event см. в MSDN): 
            const int x = 32000;
            const int y = 32000;

            int Xfr = (int)(xfr * dx), Xto = Xfr;
            int Yfr = (int)(yfr * dy), Yto = (int)(yto * dy) - N;

            Cursor.Position = new Point(Xfr, Yfr);
            mouse_event(MouseFlags.Absolute | MouseFlags.Move, Xfr, Yfr, 0, UIntPtr.Zero);
            mouse_event(MouseFlags.Absolute | MouseFlags.LeftDown, Xfr, Yfr, 0, UIntPtr.Zero);
            Thread.Sleep(200);

            for (int i = Yfr; i > Yto; i -= 1000)
            {
                mouse_event(MouseFlags.Absolute | MouseFlags.Move, Xfr, i, 0, UIntPtr.Zero);
                Thread.Sleep(20);
            }

            mouse_event(MouseFlags.Absolute | MouseFlags.Move, Xto, Yto, 0, UIntPtr.Zero);
            mouse_event(MouseFlags.Absolute | MouseFlags.LeftUp, Xto, Yto, 0, UIntPtr.Zero);
            Thread.Sleep(500);
            //mouse_event(MouseFlags.Absolute | MouseFlags.RightUp, x, y, 0, UIntPtr.Zero);
            return true;
        }
        private List<RawColor> RawColorList = new List<RawColor>();

        public RawColor WindowColor = new RawColor(31);
        Window wind = new Window();
        private void TestBtn2_Click(object sender, EventArgs e)
        {

            if (ChkBox.Checked)
            {
                wind.Dn = 1001;
                wind.LP = 84;
                wind.RP = 706;
                wind.Up = 256;
            }
            else wind = FindWindow();

            Scaner scaner = new Scaner(wind);
            Scaner scaner2 = new Scaner(scaner.RezultBitMap);
            var rawColor = scaner2.GetLine(EnumDirection.Dn);

            if (rawColor.Count == 0)
            {
                if (scaner2.RezultBitMap != null)
                {
                    // picBox.Image = scaner.RezultBitMap;
                    var img = scaner2.RezultBitMap;
                    picBox.BackColor = Color.White;
                    Bitmap myBitmap2 = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb);
                    for (int i = 0; i < img.Height - 1; i++)
                    {
                        Color color = img.GetPixel(img.Height / 2, i);
                        myBitmap2.SetPixel(picBox.Width / 2 - 1, i, Color.FromArgb(color.A, color.R, color.G, color.B));
                        myBitmap2.SetPixel(picBox.Width / 2, i, Color.FromArgb(250, color.R, color.G, color.B));
                        myBitmap2.SetPixel(picBox.Width / 2 + 1, i, Color.FromArgb(250, color.R, color.G, color.B));
                    }
                    picBox.Image = myBitmap2;
                }
            }

            if (!wind.FirstLineAnalys(rawColor))
            {
                picBox.Image = scaner.RezultBitMap;
                return;
            }

            Bitmap myBitmap = new Bitmap(picBox.Width, picBox.Height, PixelFormat.Format32bppArgb);
            int to = rawColor.Count > picBox.Height ? picBox.Height - 1 : rawColor.Count;
            for (int i = 0; i < to; i++)
            {
                myBitmap.SetPixel(picBox.Width / 2 - 1, i, Color.FromArgb(250, rawColor[i].R, rawColor[i].G, rawColor[i].B));
                myBitmap.SetPixel(picBox.Width / 2, i, Color.FromArgb(250, rawColor[i].R, rawColor[i].G, rawColor[i].B));
                myBitmap.SetPixel(picBox.Width / 2 + 1, i, Color.FromArgb(250, rawColor[i].R, rawColor[i].G, rawColor[i].B));
            }
            picBox.Image = myBitmap;

            //picBox.Image = null;
            //picBox.BackColor = Color.Black;
            //picBox.Image = scaner.RezultBitMap;
        }

        private Window FindWindow()
        {
            GetRezolution();
            picBox.Image = null;
            picBox.BackColor = Color.Black;
            //RTB.Text = null;
            //string text = string.Empty;
            IntPtr desktopPtr = GetDesktopWindow();
            IntPtr hdc = GetDC(desktopPtr);

            Window window = new Window();
            window.LP = FindLeftPoint(hdc);
            window.RP = FindRightPoint(hdc);

            int X = (window.LP + window.RP) / 2;
            if (X > 0)
            {
                window.Up = FindUpPoint(hdc, X);
                window.Dn = FindDnPoint(hdc, X);
            }
            //var check = window.Check();

            ReleaseDC(desktopPtr, hdc);
            List<int> ints = new List<int>();
            ints.Add(window.LP);
            ints.Add(window.RP);
            ints.Add(window.Up);
            ints.Add(window.Dn);
            return window;
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
                    myBitmap.SetPixel(picBox.Width / 2 - 1, i, Color.FromArgb(250, rawColor[i].R, rawColor[i].G, rawColor[i].B));
                    myBitmap.SetPixel(picBox.Width / 2, i, Color.FromArgb(250, rawColor[i].R, rawColor[i].G, rawColor[i].B));
                    myBitmap.SetPixel(picBox.Width / 2 + 1, i, Color.FromArgb(250, rawColor[i].R, rawColor[i].G, rawColor[i].B));
                }
                picBox.Image = myBitmap;
            }
            else
            {
                // picBox.Image = scaner.RezultBitMap;
                var img = scaner.RezultBitMap;
                picBox.BackColor = Color.White;
                Bitmap myBitmap = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb);
                for (int i = 0; i < img.Height - 1; i++)
                {
                    Color color = img.GetPixel(img.Height / 2, i);
                    myBitmap.SetPixel(picBox.Width / 2 - 1, i, Color.FromArgb(color.A, color.R, color.G, color.B));
                    myBitmap.SetPixel(picBox.Width / 2, i, Color.FromArgb(250, color.R, color.G, color.B));
                    myBitmap.SetPixel(picBox.Width / 2 + 1, i, Color.FromArgb(250, color.R, color.G, color.B));
                }
                picBox.Image = myBitmap;
            }
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
            picBox.Image = myBitmap;
           
            return point;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            picBox.Image = null;

            Scaner scaner = new Scaner();
            //Scaner.Screen.Dispose();
            Bitmap source = scaner.GetScrin();
            var area = new Rectangle(new Point(0, 0), new Size(source.Width / 2, source.Height));
            Scaner.Screen = scaner.CropImage(source, area);
            picBox.Image = Scaner.Screen;
        }

        private void DelScrin_Click(object sender, EventArgs e)
        {
            Scaner.Screen.Dispose();
            picBox.Image = null;
        }

        private void GetPixelBtn_Click(object sender, EventArgs e)
        {
            int x = 0,y=0;
            Int32.TryParse(XTxtBox.Text, out x);
            Int32.TryParse(YTxtBox.Text,out y);
            
            Scaner scaner = new Scaner();
            RawColor rawColor =  scaner.GetPixel(x,y);
            LbTxt.Text = rawColor.R+" "+ rawColor.G + " "+ rawColor.B;
        }

        private void TestBtn_Click(object sender, EventArgs e)
        {

        }
    }
}