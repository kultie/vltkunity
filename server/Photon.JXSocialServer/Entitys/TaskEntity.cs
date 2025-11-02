using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.JXSocialServer.Entitys
{
    public class TaskEntity
    {
        public byte Id;
        public bool Execute;
        public string TaskName;
        public string TaskFile;

        public byte TaskInterval; // don vi phut

        public byte DStart, MStart, hStart, mStart; // hh:mm DD:MM
        public byte DEnd, MEnd, hEnd, mEnd; // hh:mm DD:MM

        public TaskEntity()
        {
            Execute = false;

            TaskInterval = 0;

            DStart = MStart = hStart = mStart = 0;
            DEnd = MEnd = hEnd = mEnd = 0;
        }
    }
}
