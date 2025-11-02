using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.ShareLibrary.Entities
{
    public class CharacterLogin
    {
        public uint Id;
        public string Name;

        public byte Avatar;
        public byte Series;
        public bool Sex;

        public byte Faction;
        public byte Level;
        public ushort Rank;
    }
    public class CharacterResponse
    {
        public uint id;
        public List<CharacterLogin> chars;
    }
    public class CharacterPosition
    {
        public string Account;
        public ushort MapId;
    }
    public class CharacterData
    {
        public byte Avatar;
        public string Name;

        public byte Fiveprop;
        public bool Sex;

        public ushort LeftProp;
        public ushort LeftFight;

        public ushort Power;
        public ushort Agility;
        public ushort Outer;
        public ushort Inside;

        public byte Luck;

        public int FightExp;
        public byte FightLevel;
        public int LeadExp;
        public byte LeadLevel;

        public byte Sect;
        public byte FirstSect;
        public byte JoinCount;

        public int MaxLife;
        public int CurLife;
        public int MaxInner;
        public int CurInner;
        public int MaxStamina;
        public int CurStamina;

        public int Money;
        public int SaveMoney;

        public byte CurNpcTitle;
        public byte SectRole;
        public byte Rankrole;

        public byte Exboxrole;
        public byte Exitemrole;

        public bool FightMode;
        public byte Camp;

        public byte PkStatus;
        public byte Pkvalue;
        public int Reputevalue;
        public int Fuyuanvalue;
        public byte Rebornvalue;
        public uint TongID;

        public int RoleParm1;
        public int RoleParm2;
        public int RoleParm3;
        public int RoleParm4;
        public int RoleParm5;

        public ushort MapId;
        public int MapX;
        public int MapY;
    }
}
