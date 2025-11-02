using Photon.ShareLibrary.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Photon.ShareLibrary.Entities
{
    public struct TOrdinSkillParam
    {
        public int nLauncher;
        public eSkillLauncherType eLauncherType;

        public int nParent;
        public eSkillLauncherType eParentType;

        public int nParam1;
        public int nParam2;
        public int nWaitTime;
        public int nTargetId;
    }
}
