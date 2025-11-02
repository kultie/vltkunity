
using System.Runtime.InteropServices;

namespace game.resource.mapServer
{
    public class Element
    {
        public struct NpcData
        {
            public int templateID;
            public int positionX;
            public int positionY;
            public short specialNpc;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] reserved;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] name;

            public short level;
            public short curFrame;
            public short headImageNo;
            public short kind;
            public ushort camp;
            public ushort series;
            public ushort scriptNameLen;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
            public byte[] script;
        }
    }
}
