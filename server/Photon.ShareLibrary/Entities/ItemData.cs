using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.ShareLibrary.Entities
{
    public class ItemData
    {
        public uint id { get; set; }

        public byte Equipclasscode;
        public ushort Detailtype;
        public byte Particulartype;

        public byte Level;
        public byte Series;

        public byte Stack;
        public byte Enchance;
        public byte Point;

        public short IsWhere;
        public byte RongPoint;

        public byte X;
        public byte Y;
        public byte Local;

        public byte Param1;
        public byte Param2;
        public byte Param3;
        public byte Param4;
        public byte Param5;
        public byte Param6;

        public byte Paramr1;
        public byte Paramr2;
        public byte Paramr3;
        public byte Paramr4;
        public byte Paramr5;
        public byte Paramr6;

        public byte Paramj1;
        public byte Paramj2;
        public byte Paramj3;
        public byte Paramj4;
        public byte Paramj5;
        public byte Paramj6;
        public byte Paramj7;

        public uint RandSeed;
        public byte Lucky;

        public int IdGold;
        public bool IsPlasma;
        public byte irongNum;
        public byte iwengangPin;
        public int ibinfujiazhi;

        public bool IsBang;
        public byte IsKuaiJie;
        public bool IsMagic;

        public byte Paramb1;
        public byte Paramb2;
        public byte Paramb3;
        public byte Paramb4;

        public string WonName;
        public ushort Durability;

        public ushort Year;
        public byte Month;
        public byte Day;
        public byte Hour;
        public byte Min;

        public List<KMagicAttrib> Magics;
    }
}
