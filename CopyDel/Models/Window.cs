using System.Collections.Generic;
using System.Linq;

namespace UnmanagedCode.Models
{
    public class Window
    {

        public readonly RawColor MainWindowColor = new RawColor(31);
        public readonly RawColor FieldColor1 = new RawColor(229,224,224);
        public readonly RawColor FieldColor2 = new RawColor(230, 224, 224);
        public int LP { get; set; } = -1;
        public int RP { get; set; } = -1;
        public int Up { get; set; } = -1;
        public int Dn { get; set; } = -1;

        public int FirstCellLP { get; set; } = -1;
        public int FirstCellRP{ get; set; } = -1;
        public int FirstCellUp { get; set; } = -1;
        public int FirstCellDn { get; set; } = -1;

        public int SecondCellLP { get; set; } = -1;
        public int SecondCellRP { get; set; } = -1;
        public int SecondCellUp { get; set; } = -1;
        public int SecondCellDn { get; set; } = -1;

        public int ThirdCellLP { get; set; } = -1;
        public int ThirdCellRP { get; set; } = -1;
        public int ThirdCellUp { get; set; } = -1;
        public int ThirdCellDn { get; set; } = -1;

        public int FourthCellLP { get; set; } = -1;
        public int FourthCellRP { get; set; } = -1;
        public int FourthCellUp { get; set; } = -1;
        public int FourthCellDn { get; set; } = -1;

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

        public bool IsErr { get; set; } = false;
        public string ErrText { get; set; } = string.Empty;
        public List<string> ErrList { get; set; } = new List<string>();
        private bool SetErr(string err)
        {
            ErrText = err;
            return false;
        }

        public bool FirstLineAnalys(List<RawColor> rawColor)
        {
            bool firstCellisFind = false, isFind = false, randisFind = false;
            int findCounter = 0, randCounter = 0, findRand = -1, CellN = 0 ;
            RawColor fieldColor = FindeFieldColor(rawColor);
            for (int i = 0; i < rawColor.Count; i++)
            {
                if (rawColor[i].Equals(fieldColor))
                {
                    if (FirstCellUp == -1)firstCellisFind = true;
                    else firstCellisFind = false;

                    if (CellN == 1 && FirstCellDn != -1 && SecondCellUp == -1)
                    {
                        SecondCellUp = Up + i;
                        CellN++;
                    }
                    if (CellN == 2 && SecondCellUp != -1 && ThirdCellUp == -1 && SecondCellDn != -1)
                    {
                        ThirdCellUp = Up + i;
                        CellN++;
                    }
                    if (CellN == 3 && ThirdCellUp != -1 && FourthCellUp == -1 && ThirdCellDn != -1)
                    {
                        FourthCellUp = Up + i;
                        CellN++;
                    }
                    if (CellN == 4 && LastCellUp == -1 && FourthCellUp != -1 && LastCellDn == -1)
                    {
                        LastCellUp = Up + i;
                        CellN++;
                    }
                }
                else if (LastCellDn == -1 && CellN > 0 && CellN < 6)
                {
                    if (CellN == 1 && FirstCellDn == -1) FirstCellDn = Up + i - 1;
                    if (CellN == 2 && SecondCellDn == -1) SecondCellDn = Up + i - 1;
                    if (CellN == 3 && ThirdCellDn == -1) ThirdCellDn = Up + i - 1;
                    if (CellN == 4 && FourthCellDn == -1)
                    {
                        FourthCellDn = Up + i - 1;
                    }
                    if (CellN == 5 && LastCellDn == -1)
                    {
                        LastCellDn = Up + i - 1;
                    }
                }

                if (firstCellisFind || isFind)findCounter++;
                else findCounter = 0;

                if (findCounter > 2)
                {
                    if (FirstCellUp > 0)
                    {
                        //LastCellDn = Up + i - 1;
                    }
                    else
                    {
                        FirstCellUp = Up + i - 1;
                        CellN = 1;
                    }
                }
                if (FirstCellUp > 0 && rawColor[i].Equals(MainWindowColor)) isFind = true;
                else isFind = false;

                if (LastCellDn > 0 && rawColor[i].Equals(MainWindowColor) && findRand == -1) findRand = 0;
                else if (findRand == 0 && !rawColor[i].Equals(MainWindowColor) && randCounter != 3) randCounter++;
                else if (findRand == 0 && randCounter == 3)
                {
                    MiddleRandomCellUp = Up + i - 1;
                    FirstRandomCellUp = MiddleRandomCellUp;
                    LastRandomCellUp = MiddleRandomCellUp;
                    findRand = 1;
                }
                else if (MiddleRandomCellUp > 0 && findRand == 1 && rawColor[i].Equals(MainWindowColor))
                {
                    MiddleRandomCellDn = Up + i - 2; findRand = 2;
                    FirstRandomCellDn = MiddleRandomCellDn;
                    LastRandomCellDn = MiddleRandomCellDn;
                }
            }

            if (LastCellDn > 0 && FirstCellUp > 0 && MiddleRandomCellUp > 0 && MiddleRandomCellDn > 0) return true;
            return FirstCheck();
        }

        private bool FirstCheck()
        {
            string text = string.Empty;
            List<int> ints = new List<int>()
            { 
                FirstCellUp, FirstCellDn, SecondCellLP , SecondCellDn,
                ThirdCellUp, ThirdCellDn, FourthCellUp, FourthCellDn,
                LastCellUp, LastCellDn, FirstRandomCellUp, FirstRandomCellDn,
                MiddleRandomCellUp, MiddleRandomCellDn, LastRandomCellUp, LastRandomCellDn
            };

            if (FirstCellUp == -1) text += "FirstCellUp\n";
            if (FirstCellDn == -1) text += "FirstCellDn\n";
            if (SecondCellUp == -1) text += "SecondCellUp\n";
            if (SecondCellDn == -1) text += "SecondCellDn\n";
            if (ThirdCellUp == -1) text += "ThirdCellUp\n";
            if (ThirdCellDn == -1) text += "ThirdCellDn\n";
            if (FourthCellUp == -1) text += "FourthCellUp\n";
            if (FourthCellDn == -1) text += "FourthCellDn\n";
            if (LastCellUp == -1) text += "LastCellUp\n";
            if (LastCellDn == -1) text += "LastCellDn\n";
            if (FirstRandomCellUp == -1) text += "FirstRandomCellUp\n";
            if (FirstRandomCellDn == -1) text += "FirstRandomCellDn\n";
            if (MiddleRandomCellUp == -1) text += "MiddleRandomCellUp\n";
            if (MiddleRandomCellDn == -1) text += "MiddleRandomCellDn\n";
            if (LastRandomCellUp == -1) text += "LastRandomCellUp\n";
            if (LastRandomCellDn == -1) text += "LastRandomCellDn\n";
            bool rezult = ints.Any(x => x == -1);
            if (rezult)
            {
                text += " непределено!!!";
                SetErr(text);
            }
            return rezult;
        }

        public bool SecondLineAnalys(List<RawColor> rawColor)
        {
            return true;
        }

        private RawColor FindeFieldColor(List<RawColor> rawColor)
        {
            return rawColor.Any(x => x.Equals(FieldColor1)) ? FieldColor1 : FieldColor2;
        }
    }
}
