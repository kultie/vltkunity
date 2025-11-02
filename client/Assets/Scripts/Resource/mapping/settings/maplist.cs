
namespace game.resource.mapping.settings
{
    struct MapList
    {
        public const string resourceFolder = "\\maps\\";
        public const string filePath = "\\settings\\maplist.ini";

        public struct Section
        {
            public const string list = "List";
        }

        public struct Key
        {
            public struct Suffix
            {
                public const string name = "_name";
            }
        }

        public struct WorFile
        {
            public const string extension = ".wor";

            public struct Section
            {
                public const string main = "MAIN";
            }

            public struct Key
            {
                public const string rect = "rect";

                public struct Rect
                {
                    public const int left = 0;
                    public const int top = 1;
                    public const int right = 2;
                    public const int bottom = 3;
                }
            }
        }

        public struct MiniMap
        {
            public const string imageSuffix = "24.jpg";
        }

        public struct Region
        {
            public const string clientSuffix = "_Region_C.dat";
        }
    }
}
