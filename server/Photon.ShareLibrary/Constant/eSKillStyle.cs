using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.ShareLibrary.Constant
{
    public enum eSkillLauncherType : byte
    {
        SKILL_SLT_Npc = 0,
        SKILL_SLT_Obj,
        SKILL_SLT_Missle,
    };
    public enum eSKillStyle : byte
    {
        SKILL_SS_Missles = 0,
        SKILL_SS_Melee,
        SKILL_SS_InitiativeNpcState,
        SKILL_SS_PassivityNpcState,
        SKILL_SS_CreateNpc,
        SKILL_SS_BuildPoison,
        SKILL_SS_AddPoison,
        SKILL_SS_GetObjDirectly,
        SKILL_SS_StrideObstacle,
        SKILL_SS_BodyToObject,
        SKILL_SS_Mining,
        SKILL_SS_RepairWeapon,
        SKILL_SS_Capture,
        SKILL_SS_Thief,
    };
}
