using Photon.JXLoginServer.Entitys;
using System.Collections.Generic;
using System.Linq;

namespace Photon.JXLoginServer.Modulers
{
    public class ServerModule
    {
        public static ServerModule Me;

        Dictionary<byte,ServerEntity> servers = new Dictionary<byte,ServerEntity>();
        public ServerModule()
        {
            Me = this;
        }

        public void ConnectMe(ClientPeer client, byte id, string ip)
        {
            if (servers.ContainsKey(id))
            {
                servers[id].ip = ip;
                servers[id].client = client;
            }
            else
            {
                var entity = new ServerEntity 
                { 
                    id = id,
                    ip = ip,
                    client = client,
                };
                entity.LoadMap();
                servers.Add(id,entity);
            }
        }
        byte[] villages = new byte[] 
        {
            20,//Giang Tan thon 
            53,//Ba Lang huyen
            99,//Vinh Lac tran 
            100,//Chu Tien tran
            101,//Dao Huong thon
            121,//Long Mon tran
            153,//Thach Co tran
            174,//Long Tuyen thon
        };
        public ServerEntity FindServer(int e)
        {
            return servers.Values.FirstOrDefault(x => x.maps.Contains(e));
        }
        public int FindMap()
        {
            int map = -1;
            var min = 999999;

            foreach (var e in villages)
            {
                var entity = servers.Values.FirstOrDefault(x => x.maps.Contains(e));
                if (entity != null) // co' map
                {
                    var count = AccountModule.Me.players.Select(x => (x.server & 127) == entity.id).Count();
                    if (count < min)
                    {
                        map = e;
                        min = count;
                    }
                }
            }
            return map;
        }
    }
}
