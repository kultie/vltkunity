using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.ShareLibrary.Handlers
{
    public enum ErrorCode : short
    {
        Ok = 0,
        RegisterPassword,
        RegisterAccount,
        
        LoginAlready,
        LoginWrong,

        CharacterExist,
        CharacterMany,

        PlayerNotExist,
        PlayerInTeam,
        PlayerNotTeam,
        PlayerNotCaption,
        TeamIsFull,

        InternalServerError,
        OperationInvalid,
        UnAuthorized
    }
}
