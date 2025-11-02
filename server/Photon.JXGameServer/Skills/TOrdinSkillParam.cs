using Photon.JXGameServer.Entitys;
using Photon.ShareLibrary.Constant;

namespace Photon.JXGameServer.Skills
{
    public struct TOrdinSkillParam
    {
        public CharacterObj nLauncher;
        public CharacterObj nTargetId;

        public eSkillLauncherType eLauncherType;

        public int nParent;
        public eSkillLauncherType eParentType;

        public int nParam1;
        public int nParam2;
        public int nWaitTime;
    }
}
