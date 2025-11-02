using Photon.JXSocialServer.Entitys;
using Photon.ShareLibrary.Handlers;
using Photon.ShareLibrary.Utils;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Photon.JXSocialServer.Modulers
{
    public class ServerModule
    {
        public static ServerModule Me;

        Dictionary<byte,ServerEntity> dict = new Dictionary<byte,ServerEntity>();
        public ServerModule()
        {
            Me = this;
        }
        public void ConnectMe(byte id,ClientPeer client,SendParameters sendParameters)
        {
            if (!dict.ContainsKey(id))
            {
                var entity = new ServerEntity
                {
                    client = client,
                    sendParameters = sendParameters
                };
                dict.Add(id, entity);
            }
            dict[id].client = client;
            dict[id].sendParameters = sendParameters;
        }
        public void DoChat(ClientPeer client,Dictionary<byte,object> Parameters)
        {
            var send = new EventData
            {
                Code = (byte)OperationCode.DoChat,
                Parameters = Parameters,
            };
            var it = dict.GetEnumerator();
            while (it.MoveNext())
            {
                if (client != it.Current.Value.client)
                {
                    it.Current.Value.client.SendEvent(send, it.Current.Value.sendParameters);
                }
            }
        }
        public void SendTask(TaskEntity task,string func)
        {
            var send = new EventData
            {
                Code = (byte)OperationCode.ServerTask,
                Parameters = new Dictionary<byte, object>
                {
                    {(byte) ParamterCode.Name, task.TaskName },
                    {(byte) ParamterCode.Data, task.TaskFile },
                    {(byte) ParamterCode.Message, func }
                },
            };
            var it = dict.GetEnumerator();
            while (it.MoveNext())
            {
                it.Current.Value.client.SendEvent(send, it.Current.Value.sendParameters);
            }
        }
    }
}
