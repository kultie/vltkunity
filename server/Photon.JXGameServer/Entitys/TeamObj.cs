using Photon.JXGameServer.Modulers;
using Photon.ShareLibrary.Constant;
using Photon.ShareLibrary.Handlers;
using Photon.SocketServer;
using System.Collections.Generic;

namespace Photon.JXGameServer.Entitys
{
    public class TeamObj
    {
        public int Captain;
        public List<int> Members;

        public TeamObj(int i)
        {
            Captain = i;
            Members = new List<int>();
        }
        public bool IsFull() => Members.Count == 7;

        void SendIt(EventData data)
        {
            var obj = PlayerModule.Me.GetPlayerObj(Captain);
            if (obj != null)
                obj.ClientPeer.SendEvent(data, obj.sendParameters);

            foreach (int i in Members)
            {
                obj = PlayerModule.Me.GetPlayerObj(i);
                if (obj != null)
                    obj.ClientPeer.SendEvent(data, obj.sendParameters);
            }
        }
        public void Add(PlayerObj player)
        {
            Members.Add(player.id);

            var add = new EventData
            {
                Code = (byte)OperationCode.TeamCommand,
                Parameters = new Dictionary<byte, object>
                {
                    { (byte)ParamterCode.ActionId, (byte)TeamCommand.TeamAccept },
                    { (byte)ParamterCode.CharacterId, player.id },
                    { (byte)ParamterCode.CharacterName, player.Name },
                },
            };

            SendIt(add);
        }
        public void Charge(PlayerObj player, PlayerObj obj)
        {
            var charge = new EventData
            {
                Code = (byte)OperationCode.TeamCommand,
                Parameters = new Dictionary<byte, object>
                {
                    { (byte)ParamterCode.ActionId, (byte)TeamCommand.TeamCharge },
                    { (byte)ParamterCode.CharacterId, obj.id },
                    { (byte)ParamterCode.CharacterName, obj.Name },
                },
            };

            SendIt(charge);
            
            int idx = Members.IndexOf(obj.id);
            Members[idx] = player.id;
            Captain = obj.id;
        }
        public void Leave(PlayerObj player)
        {
            var leave = new EventData
            {
                Code = (byte)OperationCode.TeamCommand,
                Parameters = new Dictionary<byte, object>
                {
                    { (byte)ParamterCode.ActionId, (byte)TeamCommand.TeamLeave },
                    { (byte)ParamterCode.CharacterId, player.id },
                    { (byte)ParamterCode.CharacterName, player.Name },
                },
            };

            SendIt(leave);

            if (player.id == Captain)
            {
                if (Members.Count == 0)
                {
                    PlayerModule.Me.DelTeamObj(player.TeamId);
                }
                else// nhuong chuc
                {
                    var i = 0;
                    while (i < Members.Count)
                    {
                        var obj = PlayerModule.Me.GetPlayerObj(Members[i]);
                        if (obj != null)
                        {
                            Charge(player, obj);
                            return;
                        }
                        i++;
                    }
                    //bug
                }
            }
            else// roi di
            {
                Members.Remove(player.id);
            }
        }
    }
}
