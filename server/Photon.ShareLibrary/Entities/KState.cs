using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.ShareLibrary.Entities
{
    public struct KState
    {
        public short nMagicAttrib;
        public short[] nValue;
        public short nTime;

        public void Init()
        {
            nValue = new short[3];
            Clear();
        }
        public void Clear()
        {
            Array.Clear(nValue, 0, 3);
            nTime = nMagicAttrib = 0;
        }
    }

    public struct KMagicAttrib
    {
        public short nAttribType;
        public short[] nValue;
        public void Init()
        {
            nValue = new short[3];
            Clear();
        }
        public void Clear()
        {
            Array.Clear(nValue, 0, 3);
            nAttribType = 0;
        }
    }
}
