using UnmanagedCode.Enum;
using System.Collections.Generic;
using System.Drawing;

namespace UnmanagedCode.Models
{
    public class Scaner
    {
        public int LeftPoint { get; set; } = -1;
        public int RightPoint { get; set; } = -1;
        public int Up { get; set; } = -1;
        public int Dn { get; set; } = -1;
        public static Bitmap Screen { get; set; }
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

        public Scaner()
        {

        }
        public Scaner(Window window)
        {
            LeftPoint = window.LP;
            RightPoint = window.RP;
            Up = window.Up;
            Dn = window.Dn;
            if (Screen == null) GetScrin();
            if (Screen == null) return;
            if (Check())RezultBitMap = CropImage();
        }        
        public Scaner(int leftPoint, int rightPoint, int up, int dn)
        {
            LeftPoint = leftPoint;
            RightPoint = rightPoint;
            Up = up;
            Dn = dn;
            if (Screen == null) GetScrin();
        }
        public Scaner(Bitmap bitmap, int leftPoint, int rightPoint, int up, int dn)
        {
            BitMap = bitmap;
            LeftPoint = leftPoint;
            RightPoint = rightPoint;
            Up = up;
            Dn = dn;
            if (Screen == null) GetScrin();
        }
        public Scaner(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                SetErr("Err bitmap == null!!!");
                return;
            }
            BitMap = bitmap;
            LeftPoint = 0;
            RightPoint = bitmap.Width - 1;
            Up = 0;
            Dn = bitmap.Height - 1;
            if (Screen == null) GetScrin();
        }

        public Bitmap GetScrin()
        {
            ScreenCapturer screenCapturer = new ScreenCapturer();
            Screen = screenCapturer.Capture(CaptureMode.Screen);
            return Screen;
        }
        public void ChangeArea(int leftPoint, int rightPoint, int up, int dn)
        {
            LeftPoint = leftPoint;
            RightPoint = rightPoint;
            Up = up;
            Dn = dn;
        }
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
        public bool Check()
        {
            if(LeftPoint == RightPoint) return false;
            if(Up == Dn) return false;

            if (LeftPoint > RightPoint) (LeftPoint, RightPoint) = (RightPoint, LeftPoint);
            if (Up > Dn) (Up , Dn) = ( Dn, Up);
 
            if (LeftPoint >= 0 && RightPoint > 0 && Up >= 0 && Dn > 0 && Up < Dn && LeftPoint < RightPoint) return true;
            else return false;
        }
        public List<RawColor> GetLine(EnumDirection Direction)
        {
            if (!Check() || BitMap == null)
            {
                if (BitMap == null) SetErr("Err BitMap == null!!!");
                if (!Check()) SetErr("Err Scaner.!Check !!!");
                return new List<RawColor>();
            }
            List<RawColor> RawList = new List<RawColor>();
            if (Direction == EnumDirection.Dn)
            {
                int X = GetX();
                for(int i =0; i < BitMap.Height; i++ ) 
                {
                   Color  col =  BitMap.GetPixel(X, i);
                   RawList.Add(new RawColor(col.R, col.G, col.B));
                }

              
            }
            
            return RawList;
        }

        public List<RawColor> GetLine(int fx, int fy, int tx, int ty)
        {
            List<RawColor> RawList = new List<RawColor>();


            if (true)
            {
                Rectangle section = new Rectangle(new Point(0, 0), new Size(Screen.Width / 3, Screen.Height / 3));
                RezultBitMap = CropImage(Screen, section);
            }
            else
            {
                //IntPtr desktopPtr = GetDesktopWindow();
                //IntPtr hdc = GetDC(desktopPtr);
                //if (fx == tx)
                //{
                //    for (int Y = fy; Y < ty; Y++)
                //    {
                //        uint pixel = GetPixel(hdc, fx, Y);
                //        RawList.Add(new RawColor((byte)(pixel & 0x000000FF), (byte)((pixel & 0x0000FF00) >> 8), (byte)((pixel & 0x00FF0000) >> 16)));
                //    }
                //}
                //else
                //{

                //}
            }

            return RawList;
        }

        public Bitmap CropImage() => 
            CropImage(Screen, new Rectangle (new Point (LeftPoint, Up), new Size(RightPoint - LeftPoint, Dn - Up)));
        
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
