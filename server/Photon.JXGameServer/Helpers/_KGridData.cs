using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.ShareLibrary.Utils;

namespace Photon.JXGameServer.Helpers
{
    public class _KGridData
    {
        uint[] m_adwDataFlag;
        Dictionary<int,uint> m_mapData;

        public _KGridData() 
        {
            m_adwDataFlag = new uint[JXHelper.m_nRegionWidth];
            Array.Clear(m_adwDataFlag, 0, m_adwDataFlag.Length);

            m_mapData = new Dictionary<int,uint>();
        }
        public bool HasData(int nX,int nY)
		{
            if (nX < 0 || nX >= JXHelper.m_nRegionWidth)
                return false;
            if (nY < 0 || nY >= JXHelper.m_nRegionHeight)
                return false;

            var t = (uint)(1 << nY);
            return (m_adwDataFlag[nX] & t) != 0;
		}
        public uint GetData(int nX,int nY)
		{
			if (nX<0 || nX>= JXHelper.m_nRegionWidth) 
				return 0;
			if (nY<0 || nY>= JXHelper.m_nRegionHeight)
                return 0;

            var t = (uint)(1 << nY);
            return ((m_adwDataFlag[nX] & t) != 0) ? m_mapData[JXHelper.MakeLong(nX,nY)] : 0;
		}
        public void SetData(int nX,int nY,uint nData)
        {
            if (nX < 0 || nX >= JXHelper.m_nRegionWidth)
                return;
            if (nY < 0 || nY >= JXHelper.m_nRegionHeight)
                return;

            var t = (uint)(1 << nY);
            var dwId = JXHelper.MakeLong(nX, nY);

            if (nData > 0)
            {
                m_adwDataFlag[nX] |= t;
                if (!m_mapData.ContainsKey(dwId))
                {
                    m_mapData.Add(dwId, nData);
                }
            }
            else
            {
                m_adwDataFlag[nX] &= ~t;
                m_mapData.Remove(dwId);
            }
        }
        public void IncData(int nX,int nY)
        {
            if (nX < 0 || nX >= JXHelper.m_nRegionWidth)
                return;
            if (nY < 0 || nY >= JXHelper.m_nRegionHeight)
                return;

            var t = (uint)(1 << nY);
            int dwId = JXHelper.MakeLong(nX, nY);

            if ((m_adwDataFlag[nX] & t) != 0)
            {
                m_mapData[dwId]++;
            }
            else
            {
                m_adwDataFlag[nX] |= t;
                m_mapData[dwId] = 1;
            }
        }
        public void DecData(int nX,int  nY)
        {
            if (nX < 0 || nX >= JXHelper.m_nRegionWidth)
                return;
            if (nY < 0 || nY >= JXHelper.m_nRegionHeight)
                return;

            var t = (uint)(1 << nY);
            int dwId = JXHelper.MakeLong(nX, nY);

            if ((m_adwDataFlag[nX] & t) != 0)
            {
                if (--m_mapData[dwId] == 0)
                {
                    m_adwDataFlag[nX] &= ~t;
                    m_mapData.Remove(dwId);
                }
            }
        }
    }
}
