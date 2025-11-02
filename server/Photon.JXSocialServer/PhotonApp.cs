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
using Photon.JXSocialServer.Entitys;
using Photon.JXSocialServer.Modulers;
using Photon.ShareLibrary.Utils;
using Photon.SocketServer;

namespace Photon.JXSocialServer
{
    public class PhotonApp : ApplicationBase
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern uint GetPrivateProfileInt(string Section, string Key, int nDefault, string lpFileName);

        public static readonly ILogger log = LogManager.GetCurrentClassLogger();

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

            new ServerModule();
            new TongModule();

            log.Info("ServicePointManager.DefaultConnectionLimit=" + ServicePointManager.DefaultConnectionLimit);

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
        static List<TaskEntity> tasks = new List<TaskEntity>();
        public static void ThreadProc()
        {
            DateTime t1, t2, t;
            while (true)
            {
                var n = DateTime.Now;
                t = new DateTime(n.Year, n.Month, n.Day, n.Hour, n.Minute, 0);

                for (var i = 0; i <tasks.Count; i++)
                {
                    if (tasks[i].DStart != 0 || tasks[i].DEnd != 0)
                    {
                        t1 = new DateTime(n.Year, tasks[i].MStart, tasks[i].DStart, tasks[i].hStart, tasks[i].mStart, 0);
                        t2 = new DateTime(n.Year, tasks[i].MEnd, tasks[i].DEnd, tasks[i].hEnd, tasks[i].mEnd, 0);

                        if (t < t1 || t2 < t)//out time
                        {
                            if (tasks[i].Execute)
                            {
                                tasks[i].Execute = false;
                                ServerModule.Me.SendTask(tasks[i], "taskclose");
                            }
                        }
                        else
                        {
                            if (!tasks[i].Execute)
                            {
                                tasks[i].Execute = true;
                                ServerModule.Me.SendTask(tasks[i], "taskopen");
                            }
                        }
                        continue;
                    }

                    t1 = new DateTime(n.Year, n.Month, n.Day, tasks[i].hStart, tasks[i].mStart, 0);
                    t2 = new DateTime(n.Year, n.Month, n.Day, tasks[i].hEnd, tasks[i].mEnd, 0);

                    if (t < t1 || t2 < t)//out time
                    {
                        if (tasks[i].Execute)
                        {
                            tasks[i].Execute = false;
                            ServerModule.Me.SendTask(tasks[i], "taskclose");
                        }
                    }
                    else
                    {
                        if (!tasks[i].Execute)
                        {
                            tasks[i].Execute = true;
                            ServerModule.Me.SendTask(tasks[i], "taskopen");
                        }
                        else
                        if (tasks[i].TaskInterval > 0)
                        {
                            if ((t - t1).TotalMinutes % tasks[i].TaskInterval == 0)
                            {
                                ServerModule.Me.SendTask(tasks[i], "taskloop");
                            }
                        }
                    }
                }

                Thread.Sleep(TimeSpan.FromMinutes(1));
            }
        }
        static void SplitTime(string text,ref byte h,ref byte m,ref byte D,ref byte M)
        {
            text = text.Trim();
            if (!string.IsNullOrEmpty(text))
            {
                var parts = text.Split(' ');

                var hours = parts[0].Split(':');
                h = byte.Parse(hours[0]);
                m = byte.Parse(hours[1]);

                if (parts.Length > 1)
                {
                    hours = parts[1].Split(':');
                    D = byte.Parse(hours[0]);
                    M = byte.Parse(hours[1]);
                }
            }
        }
        public static void Initialize()
        {
            var sb =new StringBuilder();

            var path = Path.Combine(PhotonApp.Instance.ApplicationPath, "settings/maintask.ini");
            var n = GetPrivateProfileInt("Count", "Total", 0, path);

            for (byte i = 1; i <= n; i++)
            {
                var section = $"Task_{i}";
                if (GetPrivateProfileInt(section, "IsOpen", 0, path) != 0)
                {
                    var ent = new TaskEntity();
                    ent.Id = i;

                    if (GetPrivateProfileString(section, "TaskName", string.Empty, sb, 256, path) > 0)
                        ent.TaskName = sb.ToString();
                    else
                        ent.TaskName = section;

                    if (GetPrivateProfileString(section, "TaskFile", string.Empty, sb, 256, path) == 0)
                        continue;

                    ent.TaskFile = $"/script/task/{sb.ToString()}";
                    ent.TaskInterval = (byte)GetPrivateProfileInt(section, "TaskInterval", 0, path);

                    sb.Clear();
                    if (GetPrivateProfileString(section, "TaskStart", string.Empty, sb, 256, path) == 0)
                        continue;

                    SplitTime(sb.ToString(), ref ent.hStart,ref ent.mStart,ref ent.DStart, ref ent.MStart);

                    sb.Clear();
                    if (GetPrivateProfileString(section, "TaskEnd", string.Empty, sb, 256, path) == 0)
                        continue;

                    SplitTime(sb.ToString(), ref ent.hEnd, ref ent.mEnd, ref ent.DEnd, ref ent.MEnd);

                    tasks.Add(ent);
                }
            }

            Thread t = new Thread(new ThreadStart(ThreadProc));
            t.Start();
        }
        protected override void TearDown()
        {
        }
    }
}