using System;

namespace CopyDel.Enum
{//для удобства использования создаем перечисление с необходимыми флагами (константами), которые определяют действия мыши: 
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
}
