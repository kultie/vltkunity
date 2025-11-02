
namespace game.resource.settings
{
	public class Skill : skill.CastSkill
	{
		public static void Initialize()
		{
			new settings.skill.Initialize();
		}

		////////////////////////////////////////////////////////////////////////////////
		
		public Skill(int skillId, int skillLevel, resource.Map map)
		{
			this.self = this;
            this.map = map;
            this.skillSetting = settings.skill.SkillSetting.Get(skillId, skillLevel);
            this.missileSetting = settings.skill.MissileSetting.Get(this.skillSetting.m_nChildSkillId);
        }
    }
}
