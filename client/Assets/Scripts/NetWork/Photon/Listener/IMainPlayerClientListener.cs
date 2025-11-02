using Photon.ShareLibrary.Entities;

namespace game.network.listener
{
    public interface IMainPlayerClientListener
    {
        public void SyncTask();
        public void SyncChracter();

        public void SyncAddSkill(PlayerSkill skill);
        public void SyncUpdateSkill(PlayerSkill skill);

        public void SyncNewItem(ItemData itemData);
        public void SyncUpdateItem(ItemData itemData);
        public void SyncRemoveItem();
    }
}

