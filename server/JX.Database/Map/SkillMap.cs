using FluentNHibernate.Mapping;
using JX.Database.Model;

namespace JX.Database.Map
{
    public class SkillMap : ClassMap<SkillModel>
    {
        public SkillMap()
        {
            Id(x => x.Id).Column("id");

            Map(x => x.Character_Id).Column("character_id");

            Map(x => x.Skill_Id).Column("skill_Id");

            Map(x => x.Skill_Level).Column("skill_Level");

            Map(x => x.Skill_Exp).Column("skill_Exp");

            Table("skills");
        }
    }
}
