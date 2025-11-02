using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.ShareLibrary.Utils
{
    public class TabFile: JX_Table
    {
        public TabFile(string root): base(root) {  }
        public int  GetInteger(int _columnIndex, int _rowIndex, int _default)
        {
           return this.GetInt(_columnIndex - 1, _rowIndex - 1, _default);
        }
    }
}
