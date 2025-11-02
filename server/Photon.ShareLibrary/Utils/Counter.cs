using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Photon.ShareLibrary.Utils
{
    public class IntCounter
    {
        Int32 index;
        public IntCounter(Int32 value) 
        {
            index = value;
        }
        public Int32 Origin
        {
            get { return index; }
        }
        public Int32 Next
        { 
            get 
            {
                Interlocked.Increment(ref index);
                return index; 
            } 
        }
    }
    public class LongCounter
    {
        Int64 index;
        public LongCounter(Int64 value)
        {
            index = value;
        }
        public Int64 Origin
        {
            get { return index; }
        }
        public Int64 Next
        {
            get
            {
                Interlocked.Increment(ref index);
                return index;
            }
        }
    }
}
