using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.ShareLibrary.Constant
{
    public enum TeamCommand : byte
    {
        TeamCreate = 0,
        TeamLeave,
        TeamInvite,
        TeamAccept,
        TeamJoin,
        TeamCharge,
    }
    public enum TongCommand : byte
    {
        Create = 0,
        Delete,
        Query
    }
}
