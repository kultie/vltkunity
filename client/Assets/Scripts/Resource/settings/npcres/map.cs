
namespace game.resource.settings.npcres
{
    public class Map
    {
        public int npcIndex;

        public map.Position.Grid gridPosition;
        public int gridVertical;
        public int gridHorizontal;
        public ushort gridElementIndex;

        public Map()
        {
            this.Reset();
        }

        public void Reset()
        {
            this.npcIndex = -1;

            this.gridPosition = new map.Position.Grid();
            this.gridVertical = -1;
            this.gridHorizontal = -1;
            this.gridElementIndex = ushort.MaxValue;
        }
    }
}
