using FluentNHibernate.Mapping;
using JX.Database.Model;

namespace JX.Database.Map
{
    public class ItemMap : ClassMap<ItemModel>
    {
        public ItemMap()
        {
            Id(x => x.Id).Column("id");
            Map(x => x.Character_Id).Column("character_id");

            Map(x => x.iequipclasscode).Column("iequipclasscode");
            Map(x => x.idetailtype).Column("idetailtype");
            Map(x => x.iparticulartype).Column("iparticulartype");

            Map(x => x.ilevel).Column("ilevel");
            Map(x => x.iseries).Column("iseries");

            Map(x => x.istacknum).Column("istacknum");
            Map(x => x.ienchance).Column("ienchance");
            Map(x => x.ipoint).Column("ipoint");
            Map(x => x.iswhere).Column("iswhere");
            Map(x => x.iRongPoint).Column("iRongpiont");

            Map(x => x.ix).Column("ix");
            Map(x => x.iy).Column("iy");
            Map(x => x.ilocal).Column("ilocal");

            Map(x => x.iparam1).Column("iparam1");
            Map(x => x.iparam2).Column("iparam2");
            Map(x => x.iparam3).Column("iparam3");
            Map(x => x.iparam4).Column("iparam4");
            Map(x => x.iparam5).Column("iparam5");
            Map(x => x.iparam6).Column("iparam6");

            Map(x => x.iparamr1).Column("iparamr1");
            Map(x => x.iparamr2).Column("iparamr2");
            Map(x => x.iparamr3).Column("iparamr3");
            Map(x => x.iparamr4).Column("iparamr4");
            Map(x => x.iparamr5).Column("iparamr5");
            Map(x => x.iparamr6).Column("iparamr6");

            Map(x => x.iparamj1).Column("iparamj1");
            Map(x => x.iparamj2).Column("iparamj2");
            Map(x => x.iparamj3).Column("iparamj3");
            Map(x => x.iparamj4).Column("iparamj4");
            Map(x => x.iparamj5).Column("iparamj5");
            Map(x => x.iparamj6).Column("iparamj6");
            Map(x => x.iparamj7).Column("iparamj7");

            Map(x => x.irandseed).Column("irandseed");
            Map(x => x.ilucky).Column("ilucky");

            Map(x => x.igoldid).Column("igoldid");
            Map(x => x.isplatina).Column("isplatina");
            Map(x => x.irongNum).Column("irongNum");
            Map(x => x.iwengangPin).Column("iwengangPin");
            Map(x => x.ibinfujiazhi).Column("ibinfujiazhi");

            Map(x => x.iIsBang).Column("iIsBang");
            Map(x => x.iIsKuaiJie).Column("iIsKuaiJie");
            Map(x => x.iIsMagic).Column("iIsMagic");
            Map(x => x.iSkillType).Column("iSkillType");

            Map(x => x.iparamb1).Column("iparamb1");
            Map(x => x.iparamb2).Column("iparamb2");
            Map(x => x.iparamb3).Column("iparamb3");
            Map(x => x.iparamb4).Column("iparamb4");

            Map(x => x.iWonName).Column("iWonName");
            Map(x => x.idurability).Column("idurability");

            Map(x => x.iyear).Column("iyear");
            Map(x => x.imonth).Column("imonth");
            Map(x => x.iday).Column("iday");
            Map(x => x.ihour).Column("ihour");
            Map(x => x.imin).Column("imin");

            Table("items");
        }
    }
}
