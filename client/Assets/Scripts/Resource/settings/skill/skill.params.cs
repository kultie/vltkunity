
namespace game.resource.settings.skill
{
    public class Params
    {
        public class Owner
        {
            public settings.npcres.Controller npc;
            public settings.skill.Missile missile;
            public resource.map.Position position;
            public skill.Defination.eSkillLauncherType type;

            public Owner() {}

            public Owner(settings.npcres.Controller npcController)
            {
                this.npc = npcController;
                this.type = skill.Defination.eSkillLauncherType.SKILL_SLT_Npc;
            }

            public Owner(settings.skill.Missile missile)
            {
                this.missile = missile;
                this.type = skill.Defination.eSkillLauncherType.SKILL_SLT_Missle;
            }

            public Owner(resource.map.Position position)
            {
                this.position = position;
                this.type = skill.Defination.eSkillLauncherType.SKILL_SLT_Position;
            }

            public void SetData(settings.npcres.Controller controller)
            {
                this.npc = controller;
                this.type = skill.Defination.eSkillLauncherType.SKILL_SLT_Npc;
            }

            public void SetData(settings.skill.Missile missile)
            {
                this.missile = missile;
                this.type = skill.Defination.eSkillLauncherType.SKILL_SLT_Missle;
            }

            public void SetData(resource.map.Position position)
            {
                this.position = position;
                this.type = skill.Defination.eSkillLauncherType.SKILL_SLT_Position;
            }

            public bool HaveData()
            {
                if(this.npc != null
                    && this.npc.GetAppearance().parent == null)
                {
                    return false;
                }

                return this.npc != null
                    || this.missile != null
                    || this.position != null;
            }

            public resource.map.Position GetMapPosition()
            {
                switch (this.type)
                {
                    case skill.Defination.eSkillLauncherType.SKILL_SLT_Npc:
                        if (this.npc == null)
                        {
                            return resource.map.Position.Zero;
                        }

                        return this.npc.GetMapPosition();

                    case skill.Defination.eSkillLauncherType.SKILL_SLT_Missle:
                        if (this.missile == null)
                        {
                            return resource.map.Position.Zero;
                        }

                        return this.missile.GetMapPosition();

                    case skill.Defination.eSkillLauncherType.SKILL_SLT_Position:
                        if (this.position == null)
                        {
                            return resource.map.Position.Zero;
                        }

                        return this.position;
                }

                return resource.map.Position.Zero;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////

        public class Cast
        {
            public Params.Owner launcher;
            public Params.Owner target;

            public int nParam1;
            public int nParam2;
            public int nWaitTime;

            public Cast()
            {
                this.launcher = new Params.Owner();
                this.target = new Params.Owner();
                this.nParam1 = this.nParam2 = this.nWaitTime = 0;
            }

            public Cast(settings.npcres.Controller launcher, settings.npcres.Controller target)
            {
                this.launcher = new Params.Owner(launcher);
                this.target = new Params.Owner(target);
                this.nParam1 = this.nParam2 = this.nWaitTime = 0;
            }

            public Cast(settings.npcres.Controller launcher, resource.map.Position target)
            {
                this.launcher = new Params.Owner(launcher);
                this.target = new Params.Owner(target);
                this.nParam1 = this.nParam2 = this.nWaitTime = 0;
            }

            public Cast(settings.skill.Missile launcher, settings.npcres.Controller target)
            {
                this.launcher = new Params.Owner(launcher);
                this.target = new Params.Owner(target);
                this.nParam1 = this.nParam2 = this.nWaitTime = 0;
            }

            public Cast(settings.skill.Missile launcher, resource.map.Position target)
            {
                this.launcher = new Params.Owner(launcher);
                this.target = new Params.Owner(target);
                this.nParam1 = this.nParam2 = this.nWaitTime = 0;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////

        public class TOrdinSkillParam
        {
            public Params.Owner launcher;
            public Params.Owner parent;
            public Params.Owner target;

            public int nParam1;
            public int nParam2;
            public int nWaitTime;

            public TOrdinSkillParam()
            {
                this.launcher = this.parent = this.target = null;
                this.nParam1 = this.nParam2 = this.nWaitTime = 0;
            }
        }
    }
}
