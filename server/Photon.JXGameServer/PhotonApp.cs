using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using JX.Database;
using log4net.Config;
using Photon.JXGameServer.Common;
using Photon.JXGameServer.Items;
using Photon.JXGameServer.Modulers;
using Photon.JXGameServer.Skills;
using Photon.SocketServer;

using LogManager = ExitGames.Logging.LogManager;

namespace Photon.JXGameServer
{
    public class PhotonApp : ApplicationBase
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern uint GetPrivateProfileInt(string Section, string Key, int nDefault, string lpFileName);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public static readonly ILogger log = LogManager.GetCurrentClassLogger();
        public static byte MoneySale;
        public static byte ExpSale;
        public static byte LuckyRate;
        public static ushort MapAutoGoldNpcRank;

        static PhotonApp()
        {
        }
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            if (log.IsDebugEnabled)
            {
                log.DebugFormat("Received init request: conId={0}, endPoint={1}:{2}", initRequest.ConnectionId, initRequest.LocalIP, initRequest.LocalPort);
            }
            
            if (log.IsDebugEnabled)
            {
                log.DebugFormat("Create ClientPeer");
            }

            return new ClientPeer(initRequest);
        }
        protected override void Setup()
        {
            this.SetupLog4net();

            log.Info("Initializing ...");

            var path = Path.Combine(this.ApplicationPath, "settings/gamesetting.ini");
            MoneySale = (byte)GetPrivateProfileInt("SYSTEM", "MoneySale", 1, path);
            ExpSale = (byte)GetPrivateProfileInt("SYSTEM", "ExpSale", 1, path);
            LuckyRate = (byte)GetPrivateProfileInt("SYSTEM", "LuckyRate", 10, path);
            MapAutoGoldNpcRank = (ushort)GetPrivateProfileInt("SYSTEM", "MapAutoGoldNpcRank", 10, path);

            new KNpcAttribModify(log);

            new PlayerModule();
            new SkillModule();
            new ScriptModule();
            new SceneModule();

            new ItemModule();

            log.Info("ServicePointManager.DefaultConnectionLimit=" + ServicePointManager.DefaultConnectionLimit);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            
            TypeSerializer.RegisterTypes();

            try
            {
                KNpcAttribModify.Me.LoadConfig(this.ApplicationPath);

                PlayerModule.Me.LoadConfig(this.ApplicationPath);
                SkillModule.Me.LoadConfig(this.ApplicationPath);
                ScriptModule.Me.LoadConfig(this.ApplicationPath);
                SceneModule.Me.LoadConfig(this.ApplicationPath);

                ItemModule.Me.LoadConfig(this.ApplicationPath);

                // Setup Database connection from config
                new DatabaseRepository(Path.Combine(this.ApplicationPath, "bin"));
            }
            catch (Exception ex)
            { 
                log.Error(ex);
            }

            // Common setup
            Global.SetRoot(this.ApplicationPath);

            this.Initialize();
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            log.ErrorFormat("Unhandled exception. IsTerminating={0}, exception:{1}",
                unhandledExceptionEventArgs.IsTerminating, unhandledExceptionEventArgs.ExceptionObject);
        }

        protected virtual void SetupLog4net()
        {
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationRootPath, "log");
            log4net.GlobalContext.Properties["Photon:ApplicationLogFile"] = $"{this.ApplicationName}.log";
            var configFileInfo = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));
            if (configFileInfo.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
                XmlConfigurator.ConfigureAndWatch(configFileInfo);
            }
        }

        public static ServerPeer LoginPeer { get; private set; }
        public static ServerPeer SocialPeer { get; private set; }

        [DllImport("kernel32.dll")]
        public static extern UInt64 GetTickCount64();

        static int m_nGameLoop = 0, m_nGameDay = 0;
        static float GAME_FPS = 18;
        static int m_nTime;

        public static void ThreadProc()
        {
            ScriptModule.Me.InitWorld();

            m_nTime = Environment.TickCount;
            while (true)
            {
                if (m_nGameLoop >= 1512000)
                {
                    m_nGameLoop = 0;
                    m_nGameDay++;

                    m_nTime = Environment.TickCount;
                }
                var nMes = 1000 * m_nGameLoop;
                var m_nTicks = (Environment.TickCount - m_nTime) * GAME_FPS;
                if (nMes <= m_nTicks)
                {
                    LoginPeer.Update();
                    SocialPeer.Update();

                    PlayerModule.Me.HeartBeat();
                    SceneModule.Me.HeartBeat();

                    if (m_nGameLoop >= 1080 && (m_nGameLoop % 1080) == 0)
                    {
                        log.InfoFormat("GameLoop={0} Time={1}d {2}:{3} Player {4}",m_nGameLoop,m_nGameDay,m_nGameLoop/1080/60,m_nGameLoop/1080%60,PlayerModule.Me.Count);
                    }
                    m_nGameLoop++;
                }
                else
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(1));
                }
            }
        }
        protected virtual void Initialize()
        {
            StringBuilder str = new StringBuilder();

            var path = Path.Combine(this.ApplicationPath, "servercfg.ini");
            var ServerId = (byte)GetPrivateProfileInt("Server", "ServerId", 0, path);

            GetPrivateProfileString("Server", "ServerIP", string.Empty, str, 256, path);
            var ServerIP = str.ToString();

            GetPrivateProfileString("Server", "ServerLogin", string.Empty, str, 256, path);

            LoginPeer = new ServerPeer(ServerId, ServerIP);
            LoginPeer.Connect(str.ToString(), "JXLoginServer");

            GetPrivateProfileString("Server", "ServerSocial", string.Empty, str, 256, path);

            SocialPeer = new ServerPeer(ServerId, ServerIP);
            SocialPeer.Connect(str.ToString(), "JXSocialServer");

            Thread t = new Thread(new ThreadStart(ThreadProc));
            t.Start();
        }
 
        protected override void TearDown()
        {
        }
    }
}
