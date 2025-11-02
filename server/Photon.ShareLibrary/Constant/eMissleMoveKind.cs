using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.ShareLibrary.Constant
{
    public enum eMisslesForm : byte
    {
        SKILL_MF_Wall = 0,
        SKILL_MF_Line,
        SKILL_MF_Spread,
        SKILL_MF_Circle,
        SKILL_MF_Random,
        SKILL_MF_Zone,
        SKILL_MF_AtTarget,
        SKILL_MF_AtFirer,
        SKILL_MF_COUNT,
    };
    public enum eMissleStatus : byte
    {
        MS_DoWait = 0,
        MS_DoFly,
        MS_DoVanish,
        MS_DoCollision,
    };
    public enum eMissleMoveKind : byte
    {
        MISSLE_MMK_Stand = 0,
        MISSLE_MMK_Line,
        MISSLE_MMK_Random,
        MISSLE_MMK_Circle,
        MISSLE_MMK_Helix,
        MISSLE_MMK_Follow,
        MISSLE_MMK_Motion,
        MISSLE_MMK_Parabola,
        MISSLE_MMK_SingleLine,
        MISSLE_MMK_RollBack = 100,
        MISSLE_MMK_Toss,
    };
    public enum eMissleFollowKind : byte
    {
        MISSLE_MFK_None = 0,
        MISSLE_MFK_NPC,
        MISSLE_MFK_Missle,
    };
    public enum eMisslesGenerateStyle : byte
    {
        SKILL_MGS_NULL = 0,
        SKILL_MGS_SAMETIME,
        SKILL_MGS_ORDER,
        SKILL_MGS_RANDONORDER,
        SKILL_MGS_RANDONSAME,
        SKILL_MGS_CENTEREXTENDLINE,
    };
    public enum eMissleInteruptType : byte
    {
        Interupt_None = 0,
        Interupt_EndNewMissleWhenMove,
        Interupt_EndOldMissleLifeWhenMove,
    };
    public enum eMeleeForm : byte
    {
        Melee_AttackWithBlur = eMisslesForm.SKILL_MF_COUNT,
        Melee_Jump,
        Melee_JumpAndAttack,
        Melee_RunAndAttack,
        Melee_ManyAttack,
        Melee_Move,
    };

}
