using System;
namespace JX.Database.Model
{
    public class CharacterModel
    {
        public virtual uint Id { get; set; }
        public virtual uint Account_Id { get; set; }
        public virtual string szName { get; set; }
        public virtual byte ifiveprop { get; set; }
        public virtual bool bSex { get; set; }
        public virtual string mateName { get; set; }
        public virtual byte iavatar { get; set; }
        public virtual byte iteam { get; set; }
        public virtual short nSect { get; set; }
        public virtual short nFirstSect { get; set; }
        public virtual byte ijoincount { get; set; }

        public virtual ushort irank { get; set; }
        public virtual byte isectrole { get; set; }
        public virtual byte iPltitle { get; set; }
        public virtual byte irankrole { get; set; }

        public virtual bool cUseRevive { get; set; }
        public virtual ushort irevivalid { get; set; }
        public virtual int irevivalx { get; set; }
        public virtual int irevivaly { get; set; }
        public virtual ushort ientergameid { get; set; }
        public virtual int ientergamex { get; set; }
        public virtual int ientergamey { get; set; }

        public virtual byte iluck { get; set; }
        public virtual int isavemoney { get; set; }
        public virtual int iservermon { get; set; }
        public virtual int iclientx { get; set; }

        public virtual byte ifightlevel { get; set; }
        public virtual int ifightexp { get; set; }
        public virtual byte ileadlevel { get; set; }
        public virtual int ileadexp { get; set; }

        public virtual ushort ileftfight { get; set; }
        public virtual ushort ileftprop { get; set; }
        public virtual ushort ipower { get; set; }
        public virtual ushort iagility { get; set; }
        public virtual ushort iouter { get; set; }
        public virtual ushort iinside { get; set; }

        public virtual int imaxlife { get; set; }
        public virtual int imaxstamina { get; set; }
        public virtual int imaxinner { get; set; }
        public virtual int imaxnuqi { get; set; }
        public virtual int icurlife { get; set; }
        public virtual int icurstamina { get; set; }
        public virtual int icurinner { get; set; }

        public virtual bool cFightMode { get; set; }
        public virtual byte cPkStatus { get; set; }
        public virtual byte ipkvalue { get; set; }

        public virtual int ireputevalue { get; set; }
        public virtual int ifuyuanvalue { get; set; }

        public virtual byte irebornvalue { get; set; }
        public virtual byte iexitemrole { get; set; }
        public virtual byte iexboxrole { get; set; }
        public virtual byte isWaiGua { get; set; }
        public virtual byte isSerLock { get; set; }
        public virtual int ipassrole { get; set; }
        public virtual int iRoleParm1 { get; set; }
        public virtual int iRoleParm2 { get; set; }
        public virtual int iRoleParm3 { get; set; }
        public virtual int iRoleParm4 { get; set; }
        public virtual int iRoleParm5 { get; set; }
        public virtual int iRoleParm6 { get; set; }
        public virtual uint iRoleParm7 { get; set; }
        public virtual uint iRoleParm8 { get; set; }
        public virtual int iRoleParm9 { get; set; }
        public virtual int iRoleParm10 { get; set; }
        public virtual uint dwTongID { get; set; }
        public virtual byte isvip { get; set; }
    }
}
