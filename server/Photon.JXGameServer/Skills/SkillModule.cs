using ExitGames.Logging;
using Photon.JXGameServer.Entitys;
using Photon.JXGameServer.Skills;
using Photon.ShareLibrary.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.JXGameServer.Skills
{
    public class SkillModule
    {
        public static SkillModule Me;

        public JX_Table skills;

        Dictionary<int, MissleTemplate> g_Missles = new Dictionary<int, MissleTemplate>();
        Dictionary<int, Dictionary<byte,SkillTemplate>> m_skills;

        public SkillModule()
        {
            Me = this;

            m_skills = new Dictionary<int, Dictionary<byte, SkillTemplate>>();
        }
        public void LoadConfig(string root)
        {
            skills = new JX_Table(Path.Combine(root, "settings/skills.txt"));

            int c = 0;
            var missles = new JX_Table(Path.Combine(root, "settings/missles.txt"));
            for (int i = 0; i < missles.RowCount; i++)
            {
                var id = missles.Get<int>((int)Missle_Index.MissleId, i);
                if (id > 0)
                {
                    ++c;
                    g_Missles.Add(id, new MissleTemplate(missles, i));
                }
            }
            PhotonApp.log.InfoFormat("SkillModule: {0} missles loaded", c);
        }
        public SkillTemplate GetSkillTemplate(int id, byte lvl)
        {
            if (!m_skills.ContainsKey(id))
            {
                m_skills.Add(id, new Dictionary<byte, SkillTemplate>());
            }
            if (!m_skills[id].ContainsKey(lvl))
            {
                m_skills[id].Add(lvl, new SkillTemplate(id, lvl));
            }
            return m_skills[id][lvl];
        }
        public MissleTemplate GetMissleTemplate(int id)
        {
            if (g_Missles.ContainsKey(id))
                return g_Missles[id];
            else
                return null;
        }
    }
}
