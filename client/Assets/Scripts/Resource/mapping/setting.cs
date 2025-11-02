
namespace game.resource.mapping
{
    public struct Settings
    {
        public const string magicDescIni = "\\settings\\magicdesc.ini";
        public const string magicDescTable = "\\settings\\magicdesc.table.txt";

        public struct Music
        {
            public const string musicset = "\\settings\\music\\musicset.txt";

            public enum HeaderIndexer
            {
                mapId,
                musicFile1,
                volume1,
                startTime1,
                endTime1
            }
        }
    }
}
