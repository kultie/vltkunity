
namespace game.resource.map
{
    public class Location
    {
        private settings.MapList.MapInfo mapInfo;

        public Location(settings.MapList.MapInfo _mapInfo)
        {
            this.mapInfo = _mapInfo;
        }

        public map.Position Middle()
        {
            return new()
            {
                top = (this.mapInfo.worFile.rect.top + this.mapInfo.worFile.rect.bottom) * map.Static.nodeMapDimension / 2,
                left = (this.mapInfo.worFile.rect.left + this.mapInfo.worFile.rect.right) * map.Static.nodeMapDimension / 2
            };
        }
    }
}
