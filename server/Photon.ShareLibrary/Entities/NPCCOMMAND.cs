using Photon.ShareLibrary.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.ShareLibrary.Entities
{
    public struct NPCCOMMAND
    {
        public NPCCMD CmdKind;
        public int Param_X;
        public int Param_Y;
        public int Param_Z;
    }
    public struct PLAYER_REVIVAL_POS_DATA
    {
        public ushort m_nSubWorldID;
        public ushort m_ReviveID;
        public int m_nMpsX;
        public int m_nMpsY;
    }
}
