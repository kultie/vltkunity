using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.ShareLibrary.Constant
{
    public enum NPCCMD : byte
    {
        do_none = 0,
		do_stand,
		do_walk,
		do_run,
		do_jump,
		do_skill,
		do_magic,
		do_attack,
		do_sit,
		do_hurt,
		do_death,
		do_defense,
		do_idle,
		do_specialskill,
		do_special1,
		do_special2,
		do_special3,
		do_special4,
		do_runattack,
		do_manyattack,
		do_jumpattack,
		do_revive,
		do_stall,
		do_movepos,
		do_knockback,
		do_drag,
		do_rushattack,
		do_runattackmany,
		do_num,
    };
    public enum NPCDIR : byte
    {
        DIR_DOWN = 0,
        DIR_LEFTDOWN,
        DIR_LEFT,
        DIR_LEFTUP,
        DIR_UP,
        DIR_RIGHTUP,
        DIR_RIGHT,
        DIR_RIGHTDOWN,
    };
    public enum NPCSTATE : byte
	{
        STATE_FREEZE = 0x01,
        STATE_POISON = 0x02,
        STATE_CONFUSE = 0x04,
        STATE_STUN = 0x08,
        STATE_HIDE = 0x10,
	};
}
