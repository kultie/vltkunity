using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using JX.Database;
using log4net.Config;
using Photon.JXLoginServer.Modulers;
using Photon.ShareLibrary.Utils;
using Photon.SocketServer;

namespace Photon.JXLoginServer
{
    public class PhotonApp : ApplicationBase
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        public static readonly ILogger log = LogManager.GetCurrentClassLogger();

        public static LongCounter counter;
        static List<ClientPeer> clientPeers = new List<ClientPeer>();
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
            var peer = new ClientPeer(initRequest);
            clientPeers.Add(peer);
            return peer;
        }
        protected override void Setup()
        {
            this.SetupLog4net();

            log.Info("Initializing ...");

            new AccountModule();
            new ServerModule();

            log.Info("ServicePointManager.DefaultConnectionLimit=" + ServicePointManager.DefaultConnectionLimit);

            StringBuilder sb = new StringBuilder();
            if (GetPrivateProfileString("Server", "serial", string.Empty, sb, 256, Path.Combine(PhotonApp.Instance.ApplicationPath, "settings/servercfg.ini")) > 0)
                counter = new LongCounter(Int64.Parse(sb.ToString()));
            else
                counter = new LongCounter(0);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;

            TypeSerializer.RegisterTypes();

            try
            {
                new DatabaseRepository(Path.Combine(this.ApplicationPath, "bin"));
            }
            catch (Exception ex)
            { 
                log.Error(ex);
            }

            Initialize();
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

        public static void ThreadProc()
        {
            var clean = new List<ClientPeer>();
            while (true)
            {
                lock (clientPeers)
                {
                    clientPeers.ForEach(peer =>
                    {
                        if (!peer.isServer)
                        {
                            var span = DateTime.Now.Subtract(peer.timePeer);
                            if (span.TotalSeconds > 60 * 5)// 5p
                            {
                                clean.Add(peer);
                            }
                        }
                    });

                    foreach (var peer in clean)
                    {
                        peer.Disconnect();
                        clientPeers.Remove(peer);
                    }
                    clean.Clear();
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }

        public static void Initialize()
        {
            Thread t = new Thread(new ThreadStart(ThreadProc));
            t.Start();
        }

        protected override void TearDown()
        {
            WritePrivateProfileString("Server", "Serial", counter.Origin.ToString(), Path.Combine(PhotonApp.Instance.ApplicationPath, "settings/servercfg.ini"));
        }
    }
}