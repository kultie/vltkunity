
namespace game.scene.world
{
    class Camera
    {
        private readonly UnityEngine.Camera mainCamera;

        public Camera(UnityEngine.Camera mainCamera)
        {
            this.mainCamera = mainCamera;
        }

        public void SetMapPosition(resource.map.Position _position)
        {
            this.mainCamera.transform.position = _position.GetCameraPosition();
        }

        public void SetPosition(UnityEngine.Vector3 _position)
        {
            this.mainCamera.transform.position = _position;
        }

        public void SetOrthographicSize(float _size)
        {
            this.mainCamera.orthographicSize = _size;
        }
    }
}
