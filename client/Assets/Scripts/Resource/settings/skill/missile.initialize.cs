
namespace game.resource.settings.skill.missile
{
    public class Initialization : missile.Data
    {
        public void Initialize()
        {
            this.texture.Initialize();
            this.isActive = true;
            this.isPainting = false;

            this.texture.SetMapPosition(this.m_nRefPY, this.m_nRefPX);
        }
    }
}
