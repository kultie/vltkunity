using FluentNHibernate.Mapping;
using JX.Database.Model;

namespace JX.Database.Map
{
	public class CharacterMap : ClassMap<CharacterModel>
	{
		public CharacterMap()
		{
			Id(x => x.Id).Column("id");
			Map(x => x.Account_Id).Column("account_id");

            Map(x => x.szName).Column("szName");
            Map(x => x.ifiveprop).Column("ifiveprop");
            Map(x => x.bSex).Column("bSex");
            Map(x => x.mateName).Column("mateName");
            Map(x => x.iavatar).Column("iavatar");
            Map(x => x.iteam).Column("iteam");
            Map(x => x.nSect).Column("nSect");
            Map(x => x.nFirstSect).Column("nFirstSect");
            Map(x => x.ijoincount).Column("ijoincount");
            Map(x => x.irank).Column("irank");
            Map(x => x.isectrole).Column("isectrole");
            Map(x => x.iPltitle).Column("iPltitle");
            Map(x => x.irankrole).Column("irankrole");
            Map(x => x.cUseRevive).Column("cUseRevive");
            Map(x => x.irevivalid).Column("irevivalid");
            Map(x => x.irevivalx).Column("irevivalx");
            Map(x => x.irevivaly).Column("irevivaly");
            Map(x => x.ientergameid).Column("ientergameid");
            Map(x => x.ientergamex).Column("ientergamex");
            Map(x => x.ientergamey).Column("ientergamey");

            Map(x => x.iluck).Column("iluck");
            Map(x => x.isavemoney).Column("isavemoney");
            Map(x => x.iservermon).Column("iservermon");
            Map(x => x.iclientx).Column("iclientx");
            Map(x => x.ifightlevel).Column("ifightlevel");
            Map(x => x.ifightexp).Column("ifightexp");
            Map(x => x.ileadlevel).Column("ileadlevel");
            Map(x => x.ileadexp).Column("ileadexp");
            Map(x => x.ileftfight).Column("ileftfight");
            Map(x => x.ileftprop).Column("ileftprop");
            Map(x => x.ipower).Column("ipower");
            Map(x => x.iagility).Column("iagility");
            Map(x => x.iouter).Column("iouter");
            Map(x => x.iinside).Column("iinside");

            Map(x => x.imaxlife).Column("imaxlife");
            Map(x => x.imaxstamina).Column("imaxstamina");
            Map(x => x.imaxinner).Column("imaxinner");
            Map(x => x.imaxnuqi).Column("imaxnuqi");
            Map(x => x.icurlife).Column("icurlife");
            Map(x => x.icurstamina).Column("icurstamina");
            Map(x => x.icurinner).Column("icurinner");
            Map(x => x.cFightMode).Column("cFightMode");
            Map(x => x.cPkStatus).Column("cPkStatus");
            Map(x => x.ipkvalue).Column("ipkvalue");

            Map(x => x.ireputevalue).Column("ireputevalue");
            Map(x => x.ifuyuanvalue).Column("ifuyuanvalue");
            Map(x => x.irebornvalue).Column("irebornvalue");
            Map(x => x.iexitemrole).Column("iexitemrole");
            Map(x => x.iexboxrole).Column("iexboxrole");

            Map(x => x.isWaiGua).Column("isWaiGua");
            Map(x => x.isSerLock).Column("isSerLock");
            Map(x => x.ipassrole).Column("ipassrole");
            Map(x => x.iRoleParm1).Column("iRoleParm1");
            Map(x => x.iRoleParm2).Column("iRoleParm2");
            Map(x => x.iRoleParm3).Column("iRoleParm3");
            Map(x => x.iRoleParm4).Column("iRoleParm4");
            Map(x => x.iRoleParm5).Column("iRoleParm5");
            Map(x => x.iRoleParm6).Column("iRoleParm6");
            Map(x => x.iRoleParm7).Column("iRoleParm7");
            Map(x => x.iRoleParm8).Column("iRoleParm8");
            Map(x => x.iRoleParm9).Column("iRoleParm9");
            Map(x => x.iRoleParm10).Column("iRoleParm10");
            Map(x => x.dwTongID).Column("dwTongID");
            Map(x => x.isvip).Column("isvip");

            Table("characters");
		}
	}
}
