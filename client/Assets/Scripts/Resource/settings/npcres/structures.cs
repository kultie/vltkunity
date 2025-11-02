
namespace game.resource.settings.npcres
{
    public class Structures
    {
        public struct PartSprInfo
        {
            public string sprFullPath;
            public ushort frameCount;
            public int directionCount;
            public int intervalRatio;
        }

        public class PartAnimation
        {
            public string sprPath;
            public ushort frameBegin;
            public ushort frameEnd;
            public int framePerDirection;
            public int framePerSeconds;
            public int layerOrder;

            public ushort GetNowFrameIndex(float _delta = 0)
            {
                float delta = _delta != 0 ? _delta : UnityEngine.Time.timeSinceLevelLoad;

                ushort indexOnCount = (ushort)((delta * this.framePerSeconds) % this.framePerDirection);
                return indexOnCount += this.frameBegin;
            }
        }
    }
}
