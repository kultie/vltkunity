using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.ShareLibrary.Entities
{
    public class PlayerSkill
    {
        public ushort id;
        public byte level;
        public uint exp;
    }
    public class PlayerTask
    {
        public int id;
        public int value;
    }
    public class PlayerFriend
    {
        public uint id;
        public string name;
    }
}
