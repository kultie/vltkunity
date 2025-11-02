namespace JX.Database.Model
{
    public class ItemModel
    {
        public virtual ulong Id { get; set; }
        public virtual uint Character_Id { get; set; }

        public virtual byte iequipclasscode { get; set; }
        public virtual ushort idetailtype { get; set; }
        public virtual byte iparticulartype { get; set; }

        public virtual byte ilevel { get; set; }
        public virtual short iseries { get; set; }
        public virtual byte istacknum { get; set; }
        public virtual int ienchance { get; set; }
        public virtual byte ipoint { get; set; }
        public virtual short iswhere { get; set; }
        public virtual byte iRongPoint { get; set; }

        public virtual byte ix { get; set; }
        public virtual byte iy { get; set; }
        public virtual byte ilocal { get; set; }

        public virtual byte iparam1 { get; set; }
        public virtual byte iparam2 { get; set; }
        public virtual byte iparam3 { get; set; }
        public virtual byte iparam4 { get; set; }
        public virtual byte iparam5 { get; set; }
        public virtual byte iparam6 { get; set; }

        public virtual byte iparamr1 { get; set; }
        public virtual byte iparamr2 { get; set; }
        public virtual byte iparamr3 { get; set; }
        public virtual byte iparamr4 { get; set; }
        public virtual byte iparamr5 { get; set; }
        public virtual byte iparamr6 { get; set; }

        public virtual byte iparamj1 { get; set; }
        public virtual byte iparamj2 { get; set; }
        public virtual byte iparamj3 { get; set; }
        public virtual byte iparamj4 { get; set; }
        public virtual byte iparamj5 { get; set; }
        public virtual byte iparamj6 { get; set; }
        public virtual byte iparamj7 { get; set; }

        public virtual uint irandseed { get; set; }
        public virtual byte ilucky { get; set; }

        public virtual int igoldid { get; set; }
        public virtual bool isplatina { get; set; }
        public virtual byte irongNum { get; set; }
        public virtual byte iwengangPin { get; set; }
        public virtual int ibinfujiazhi { get; set; }

        public virtual bool iIsBang { get; set; }
        public virtual byte iIsKuaiJie { get; set; }
        public virtual bool iIsMagic { get; set; }
        public virtual int iSkillType { get; set; }

        public virtual int iparamb1 { get; set; }
        public virtual int iparamb2 { get; set; }
        public virtual int iparamb3 { get; set; }
        public virtual int iparamb4 { get; set; }

        public virtual string iWonName { get; set; }
        public virtual ushort idurability { get; set; }

        public virtual ushort iyear { get; set; }
        public virtual byte imonth { get; set; }
        public virtual byte iday { get; set; }
        public virtual byte ihour { get; set; }
        public virtual byte imin { get; set; }
    }
}
