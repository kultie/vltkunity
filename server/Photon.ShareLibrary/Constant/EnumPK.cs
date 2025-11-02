using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.ShareLibrary.Constant
{
    public enum EnumPK : byte
    {
        ENMITY_STATE_CLOSE = 0,
        ENMITY_STATE_TIME,
        ENMITY_STATE_PKING,
        ENMITY_STATE_NUM,
    };
    public enum DEATH_MODE : byte
    {
        DEATH_MODE_NPC_KILL = 0,
        DEATH_MODE_PLAYER_NO_PUNISH,
        DEATH_MODE_PLAYER_PUNISH,
        DEATH_MODE_PKBATTLE_PUNISH,
        DEATH_MODE_EXP_PUNISH,
        DEATH_MODE_MONEY_PUNISH,
        DEATH_MODE_EQUIP_PUNISH,
        DEATH_MODE_JINBI_PUNISH,
        DEATH_MODE_NUM,
    };
}
