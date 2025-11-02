using System.Collections.Generic;

namespace Photon.ShareLibrary.Handlers
{
    public enum MessageType
    {
        Null = 0,
        Response,
        Event
    }
    public interface iMessageHandler
    {
        MessageType Type { get; }
        OperationCode Code { get; }

        void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters); 
    }
}