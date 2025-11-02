
using System.Runtime.InteropServices;

namespace game.resource.map
{
    public class Element
    {
        public struct TextureType
        {
            public const int unidentified = 0;
            public const int groundNode = 1;
            public const int groundObject = 2;
            public const int buildingUnder = 3;
            public const int buildingAbove = 4;
            public const int tree = 5;
        };

        public struct Texture
        {
            public int type;

            public map.Position.Sequential.Origin originPosition;
            public map.Position.Sequential.Node nodeAssetPosition;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 150)]
            public byte[] texturePathBuffer;

            public ushort textureFrame;
            public int order;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct ObstacleGridElement
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public int[] element;
        }

        public struct Obstacle
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public Element.ObstacleGridElement[] grid;

            public map.Position.Sequential.Node nodeAssetPosition;
        }

        public Element.Texture[] texture;
        public Element.Obstacle[] obstacle;
    }
}
