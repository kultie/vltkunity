using Photon.ShareLibrary.Constant;

namespace game.network.listener
{
    public interface INpcClientListener
    {
        public void ChangeWorld();
        public void DelNpc(int id);
        public NpcClick FindNpc(int id);
        public void UpdateNpc(game.resource.settings.npcres.Controller npcController, int top, int left);
        public NpcClick SpwanNpc(int id);

        public void DelObj(int id);
        public void ActiveObj(int id, short data);
        public game.resource.settings.objres.Controller SpwanObj(int id, byte dir, ObjKind kind, int npcType, int mapX, int mapY);

        public void NpcQuest(int id, string name, string data);
        public void NpcSale(int id, string data);
        public void NpcTalk(int id, string name, string data);
    }
}

