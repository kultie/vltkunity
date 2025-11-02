using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.ShareLibrary.Entities
{
    public struct KStateNode
    {
        public int m_SkillID;
        public byte m_Level;
        public int m_LeftTime;
        public List<KMagicAttrib> m_State;

        public void Init()
        {
            m_SkillID = 0;
            m_Level = 0;
            m_LeftTime = 0;
            m_State = new List<KMagicAttrib>();
        }
    }
}
