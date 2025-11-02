using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.ShareLibrary.Utils
{
    public struct JX_KMagicAttrib
    {
        public int nAttribType;
        public int[] nValue;

        public void Init()
        {
            nAttribType = 0;
            nValue = new int[3];

            Array.Clear(nValue, 0, 3);
        }
    }
}
