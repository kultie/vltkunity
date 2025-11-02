using App;
using ExitGames.Logging;
using Photon.ShareLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Photon.JXGameServer.Modulers
{
    public class ConfigurationRepository
    {
        public static ConfigurationRepository Me;

        

        ILogger log;

        public ConfigurationRepository(PhotonApp app, ILogger log)
        {
            Me = this;
            this.log = log;
        }


    }
}

