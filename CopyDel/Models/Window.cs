using System;
using System.Collections.Generic;
using System.Drawing;

namespace UnmanagedCode.Models
{
    public class Window
    {
        public readonly RawColor MainWindowColor = new RawColor(31);
        public readonly RawColor FieldColor3 = new RawColor(230,224,224);
        public readonly RawColor FieldColor = new RawColor(229,224,224);
        public int LP { get; set; } = -1;
        public int RP { get; set; } = -1;
        public int Up { get; set; } = -1;
        public int Dn { get; set; } = -1;

        public int FirstCellLP { get; set; } = -1;
        public int FirstCellRP{ get; set; } = -1;
        public int FirstCellUp { get; set; } = -1;
        public int FirstCellDn { get; set; } = -1;

        public int LastCellLP { get; set; } = -1;
        public int LastCellRP{ get; set; } = -1;
        public int LastCellUp { get; set; } = -1;
        public int LastCellDn { get; set; } = -1;

        public int FirstRandomCellLP { get; set; } = -1;
        public int FirstRandomCellRP { get; set; } = -1;
        public int FirstRandomCellUp { get; set; } = -1;
        public int FirstRandomCellDn { get; set; } = -1;

        public int MiddleRandomCellLP { get; set; } = -1;
        public int MiddleRandomCellRP { get; set; } = -1;
        public int MiddleRandomCellUp { get; set; } = -1;
        public int MiddleRandomCellDn { get; set; } = -1;

        public int LastRandomCellLP { get; set; } = -1;
        public int LastRandomCellRP { get; set; } = -1;
        public int LastRandomCellUp { get; set; } = -1;
        public int LastRandomCellDn { get; set; } = -1;


        public bool FirstLineAnalys(List<RawColor> rawColor)
        {
            bool firstCellisFind = false, isFind = false, randisFind = false;
            int findCounter = 0, randCounter = 0, findRand = -1;
            for (int i = 0; i < rawColor.Count; i++)
            {
                if (rawColor[i].Equals(FieldColor)&& FirstCellUp==-1)
                {
                    firstCellisFind = true;
                }
                else firstCellisFind = false;

                if (firstCellisFind || isFind) findCounter++;
                else findCounter = 0;

                if (findCounter > 2)
                {
                    if (FirstCellUp > 0) LastCellDn = Up + i - 1;
                    else FirstCellUp = Up + i - 1;
                }

                if (FirstCellUp > 0 && rawColor[i].Equals(MainWindowColor)) isFind = true;
                else isFind = false;

                if (LastCellDn > 0 && rawColor[i].Equals(MainWindowColor) && findRand == -1)
                {
                    findRand = 0;
                }
                else if (findRand == 0 && !rawColor[i].Equals(MainWindowColor) && randCounter != 3)
                {
                    randCounter++;
                }
                else if (findRand == 0 && randCounter == 3) { MiddleRandomCellUp = Up + i - 1; findRand = 1; }
                else if (MiddleRandomCellUp > 0 && findRand == 1 && rawColor[i].Equals(MainWindowColor)) 
                { 
                    MiddleRandomCellDn = Up + i - 2; findRand = 2;
                    FirstRandomCellDn = MiddleRandomCellDn;
                    LastRandomCellDn = MiddleRandomCellDn;
                }
            }

            if (LastCellDn > 0 && FirstCellUp > 0 && MiddleRandomCellUp > 0 && MiddleRandomCellDn > 0) return true;
            return false;
        }
    }
}
