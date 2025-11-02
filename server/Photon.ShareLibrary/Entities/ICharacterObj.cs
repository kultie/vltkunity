using Photon.ShareLibrary.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.ShareLibrary.Entities
{
    public interface ICharacterObj
    {
        int Id { get; }
        string Name { get; set; }
        NPCKIND Kind { get; }
        NPCSERIES Series { get; set; }
        NPCCAMP CurrentCamp { get; set; }
        bool FightMode { get; set; }

        EnumPK GetNormalPKState();
        EnumPK GetEnmityPKState();
        int GetEnmityPKAim();
        int GetExercisePKAim();

        string MasterName { get; set; }
        ICharacterObj MasterObj { get; set; }
    }
}
