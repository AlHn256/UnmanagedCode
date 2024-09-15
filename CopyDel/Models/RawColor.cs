using System;
using System.Runtime.CompilerServices;

namespace CopyDel.Models
{
    public readonly struct RawColor
    {
        public readonly byte R, G, B;
        public RawColor(byte r, byte g, byte b)
        {
            //(R, G, B) = (r, g, b);
            R = r;
            G = g;
            B = b;
        }
        public RawColor(byte gray)
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
        public override string ToString() => R.ToString() + " " + G.ToString() + " " + B.ToString() + " ";
    }
}
