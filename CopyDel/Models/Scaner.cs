using CopyDel.Enum;
using System.Collections.Generic;
using System.Drawing;

namespace CopyDel.Models
{
    public class Scaner
    {
        public int LeftPoint { get; set; } = -1;
        public int RightPoint { get; set; } = -1;
        public int Up { get; set; } = -1;
        public int Dn { get; set; } = -1;

        public Bitmap BitMap { get; set; }
        public Bitmap RezultBitMap { get; set; }

        public bool IsErr { get; set; } = false;
        public string ErrText { get; set; } = string.Empty;

        private bool SetErr(string err)
        {
            IsErr = true;
            ErrText = err;
            return false;
        }

        public Scaner(int leftPoint, int rightPoint, int up, int dn)
        {
            LeftPoint = leftPoint;
            RightPoint = rightPoint;
            Up = up;
            Dn = dn;
        }

        //public Scaner()
        //{
        //    LeftPoint = 0;
        //    RightPoint = GetSystemMetrics(0);
        //    Up = 0;
        //    Dn = GetSystemMetrics(1);
        //}
        public Scaner(Bitmap bitmap, int leftPoint, int rightPoint, int up, int dn)
        {
            BitMap = bitmap;
            LeftPoint = leftPoint;
            RightPoint = rightPoint;
            Up = up;
            Dn = dn;

        }

        public Scaner(Bitmap bitmap)
        {
            BitMap = bitmap;
            LeftPoint = 0;
            RightPoint = bitmap.Width - 1;
            Up = 0;
            Dn = bitmap.Height - 1;
        }
        public void ChangeArea(int leftPoint, int rightPoint, int up, int dn)
        {
            LeftPoint = leftPoint;
            RightPoint = rightPoint;
            Up = up;
            Dn = dn;
        }
        //public void ChangeArea()
        //{
        //    LeftPoint = 0;
        //    RightPoint = GetSystemMetrics(0);
        //    Up = 0;
        //    Dn = GetSystemMetrics(1);
        //}

        public bool StartScan()
        {
            List<RawColor> DnPointList = GetLine(EnumDirection.Dn);
            List<RawColor> UpPointList = GetLine(EnumDirection.Up);
            List<RawColor> LtPointList = GetLine(EnumDirection.Lt);
            List<RawColor> RtPointList = GetLine(EnumDirection.Rt);

            return true;
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
            List<RawColor> RawList = new List<RawColor>();
            if (true)
            {
                ScreenCapturer screenCapturer = new ScreenCapturer();
                var sdf = screenCapturer.Capture(CaptureMode.Screen);
                if (sdf != null)
                {
                    Rectangle section = new Rectangle(new Point(0, 0), new Size(sdf.Width / 3, sdf.Height / 3));
                    RezultBitMap = CropImage(sdf, section);
                }
            }
            else
            {
                IntPtr desktopPtr = GetDesktopWindow();
                IntPtr hdc = GetDC(desktopPtr);
                if (fx == tx)
                {
                    for (int Y = fy; Y < ty; Y++)
                    {
                        uint pixel = GetPixel(hdc, fx, Y);
                        RawList.Add(new RawColor((byte)(pixel & 0x000000FF), (byte)((pixel & 0x0000FF00) >> 8), (byte)((pixel & 0x00FF0000) >> 16)));
                    }
                }
                else
                {

                }
            }

            return RawList;
        }

        public Bitmap CropImage(Bitmap source, Rectangle section)
        {
            var bitmap = new Bitmap(section.Width, section.Height);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);
                return bitmap;
            }
        }
    }
}
