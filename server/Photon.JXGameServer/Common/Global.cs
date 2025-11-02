using Photon.ShareLibrary.Utils;
using System.IO;

namespace Photon.JXGameServer.Common
{
    public class Global
    {
        private static string _root;
        public static string Root { get { return _root; } }
        public static void SetRoot(string path) { _root = path; }

        public static TabFile WayPointTabFile;
        public static TabFile StationTabFile;
        public static TabFile Docpublic ;
        public static TabFile StationPriceTabFile;
        public static TabFile WayPointPriceTabFile;
        public static TabFile DockPriceTabFile;

        public static void LoadFiles()
        {
            WayPointTabFile = new TabFile(Path.Combine(_root, GlobalConfig.WORLD_WAYPOINT_TABFILE));
            StationTabFile = new TabFile(Path.Combine(_root, GlobalConfig.WORLD_STATION_TABFILE));
            Docpublic = new TabFile(Path.Combine(_root, GlobalConfig.WORLD_STATIONPRICE_TABFILE));
            StationPriceTabFile = new TabFile(Path.Combine(_root, GlobalConfig.WORLD_WAYPOINTPRICE_TABFILE));
            WayPointPriceTabFile = new TabFile(Path.Combine(_root, GlobalConfig.WORLD_DOCK_TABFILE));
            DockPriceTabFile = new TabFile(Path.Combine(_root, GlobalConfig.WORLD_DOCKPRICE_TABFILE));
            /*Debug.WriteLine("WayPointTabFile count: " + (WayPointTabFile.RowCount - 1));
            Debug.WriteLine("StationTabFile count: " + (StationTabFile.RowCount - 1));
            Debug.WriteLine("Docpublic count: " + (StationPriceTabFile.RowCount - 1));
            Debug.WriteLine("StationPriceTabFile count: " + (StationPriceTabFile.RowCount - 1));
            Debug.WriteLine("WayPointPriceTabFile count: " + (WayPointPriceTabFile.RowCount - 1));
            Debug.WriteLine("DockPriceTabFile count: " + (DockPriceTabFile.RowCount - 1));*/
        }
    }

    public class GlobalConfig
    {
        public static string WORLD_WAYPOINT_TABFILE =  "settings\\waypoint.txt";
        public static string WORLD_STATION_TABFILE = "settings\\station.txt";
        public static string WORLD_STATIONPRICE_TABFILE = "settings\\stationprice.txt";
        public static string WORLD_WAYPOINTPRICE_TABFILE = "settings\\waypointprice.txt";
        public static string WORLD_DOCK_TABFILE = "settings\\wharf.txt";
        public static string WORLD_DOCKPRICE_TABFILE = "settings\\wharfprice.txt";
    }
}
