using System;
using System.Collections.Generic;

namespace Photon.ShareLibrary.Handlers
{
    public class MessageHandlers : iMessageHandler
    {
        public virtual MessageType Type => MessageType.Null;

        public virtual OperationCode Code => 0;

        public virtual void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            throw new NotImplementedException();
        }
    }
}
