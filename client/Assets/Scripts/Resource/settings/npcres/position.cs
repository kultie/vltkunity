
namespace game.resource.settings.npcres
{
    public class Position
    {
        private readonly resource.map.Position mapPosition;
        private UnityEngine.Vector3 scenePosition;
        private UnityEngine.Vector3 cameraPosition;
        private int orderInMap;

        private readonly npcres.Identification identify;

        public Position(npcres.Identification identification)
        {
            this.mapPosition = new resource.map.Position();
            this.scenePosition = new UnityEngine.Vector3();
            this.orderInMap = 0;

            this.identify = identification;
        }

        public void SetMapPosition(int _top, int _left)
        {
            this.mapPosition.top = _top;
            this.mapPosition.left = _left;

            this.scenePosition.y = _top / -100f;
            this.scenePosition.x = _left / 100f;

            this.cameraPosition.y = this.scenePosition.y;
            this.cameraPosition.x = this.scenePosition.x;
            this.cameraPosition.z = -10;

            this.orderInMap = (_top * 2) << 1;

            this.identify.SetScenePosition(this.scenePosition);
            this.identify.SetMapPos(string.Empty + _left + ", " + (_top * 2));
        }

        public void SetMapPosition(resource.map.Position _mapPosition) => this.SetMapPosition(_mapPosition.top, _mapPosition.left);

        public resource.map.Position GetMapPosition() => new map.Position(this.mapPosition);

        public UnityEngine.Vector3 GetScenePosition() => this.scenePosition;

        public UnityEngine.Vector3 GetCameraPosition(int z = -10)
        {
            this.cameraPosition.z = z;
            return this.cameraPosition;
        }

        public int GetOrderInMap() => this.orderInMap;
    }
}
