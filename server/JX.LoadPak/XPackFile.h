#pragma once

#include <windows.h>
#include <cstdlib>

#include "JXEntity.h"
#include "ucl/ucl.h"

class XPackFile 
{
	XPackIndexInfo* m_pIndexList;

	HANDLE m_hFile;
	DWORD m_uFileSize;
	int m_nSelfIndex, m_nElemFileCount;

	bool DirectRead(void* pBuffer, unsigned int uOffset, unsigned int uSize)
	{
		if (pBuffer == NULL || m_hFile == INVALID_HANDLE_VALUE)
			return false;

		DWORD dwReaded;
		if (uOffset + uSize <= m_uFileSize && ::SetFilePointer(m_hFile, uOffset, 0, FILE_BEGIN) == uOffset) //设置当前的读写位置
		{
			if (ReadFile(m_hFile, pBuffer, uSize, &dwReaded, NULL))
			{
				if (dwReaded == uSize)
					return true;
			}
		}
		return false;
	}

	bool ExtractRead(void* pBuffer, unsigned int uExtractSize,
		long lCompressType, unsigned int uOffset, unsigned int uSize)
	{
		bool bResult = false;

		if (lCompressType == TYPE_NONE)
		{
			if (uExtractSize == uSize)
				bResult = DirectRead(pBuffer, uOffset, uSize);
		}
		else
		{
			void* pReadBuffer = malloc(uSize);
			if (pReadBuffer)
			{
				if ((lCompressType == TYPE_UCL || lCompressType == TYPE_UCL_OLD || lCompressType == TYPE_BZIP2_OLD || lCompressType == TYPE_BZIP2) && DirectRead(pReadBuffer, uOffset, uSize))
				{
					unsigned int uDestLength;
					ucl_nrv2b_decompress_8((BYTE*)pReadBuffer, uSize, (BYTE*)pBuffer, &uDestLength, NULL);
					bResult = (uDestLength == uExtractSize);
				}
				else
					if ((lCompressType == TYPE_FRAGMENT || lCompressType == TYPE_FRAGMENT_OLD) && DirectRead(pReadBuffer, uOffset, uSize))
					{
						unsigned int uDestLength;
						ucl_nrv2d_decompress_8((BYTE*)pReadBuffer, uSize, (BYTE*)pBuffer, &uDestLength, NULL);
						bResult = (uDestLength == uExtractSize);
					}
					else
						if ((lCompressType == TYPE_FRAGMENTA || lCompressType == TYPE_FRAGMENTA_OLD) && DirectRead(pReadBuffer, uOffset, uSize))
						{
							unsigned int uDestLength;
							ucl_nrv2e_decompress_8((BYTE*)pReadBuffer, uSize, (BYTE*)pBuffer, &uDestLength, NULL);
							bResult = (uDestLength == uExtractSize);
						}
						else
						{
						}
				free(pReadBuffer);
			}
		}

		return bResult;
	}
public:
	int LoadPackage(int id, char* pszPackFileName)
	{
		m_nSelfIndex = id;

		m_hFile = ::CreateFile(pszPackFileName, GENERIC_READ, FILE_SHARE_READ, NULL, OPEN_EXISTING, 0, NULL);
		if (m_hFile != INVALID_HANDLE_VALUE)
		{
			m_uFileSize = ::GetFileSize(m_hFile, NULL);

			XPackFileHeader	header;
			DWORD dwReaded;
			if (::ReadFile(m_hFile, &header, sizeof(header), &dwReaded, NULL) == TRUE)
			{
				if (dwReaded != sizeof(header) ||
					*(int*)(&header.cSignature) != XPACKFILE_SIGNATURE_FLAG ||
					header.uCount == 0 ||
					header.uIndexTableOffset < sizeof(XPackFileHeader) ||
					header.uIndexTableOffset >= m_uFileSize ||
					header.uDataOffset < sizeof(XPackFileHeader) ||
					header.uDataOffset >= m_uFileSize)
				{
					::CloseHandle(m_hFile);
					return 0;
				}
				DWORD dwListSize = sizeof(XPackIndexInfo) * header.uCount;
				m_pIndexList = (XPackIndexInfo*)malloc(dwListSize);
				if (m_pIndexList == NULL ||
					::SetFilePointer(m_hFile, header.uIndexTableOffset, NULL, FILE_BEGIN) != header.uIndexTableOffset)
				{
					::CloseHandle(m_hFile);
					return 0;
				}
				if (::ReadFile(m_hFile, m_pIndexList, dwListSize, &dwReaded, NULL) == TRUE)
				{
					if (dwReaded == dwListSize)
					{
						m_nElemFileCount = header.uCount;
						return header.uCount;
					}
				}
				free(m_pIndexList);
			}
			::CloseHandle(m_hFile);
		}
		return 0;
	};
	int XFindElemFileA(unsigned int ulId)
	{
		int nBegin, nEnd, nMid = 0;
		nBegin = 0;
		nEnd = m_nElemFileCount - 1;
		while (nBegin <= nEnd)
		{
			nMid = (nBegin + nEnd) / 2;
			if (ulId < m_pIndexList[nMid].uId)
				nEnd = nMid - 1;
			else if (ulId > m_pIndexList[nMid].uId)
				nBegin = nMid + 1;
			else
				break;
		}
		return ((nBegin <= nEnd) ? nMid : -1);
	}
	bool XFindElemFile(unsigned int uId, XPackElemFileRef& ElemRef)
	{
		ElemRef.nElemIndex = XFindElemFileA(uId);
		if (ElemRef.nElemIndex >= 0)
		{
			ElemRef.uId = uId;
			ElemRef.nPackIndex = m_nSelfIndex;
			ElemRef.nOffset = m_pIndexList[ElemRef.nElemIndex].uOffset;
			ElemRef.nSize = m_pIndexList[ElemRef.nElemIndex].lSize;
			return true;
		}
		return false;
	}
	void* ExtractElem(int nElemIndex)
	{
		void* pDataBuffer = malloc(m_pIndexList[nElemIndex].lSize);
		if (pDataBuffer)
		{
			long lCompressType = m_pIndexList[nElemIndex].lCompressSizeFlag & TYPE_FILTER_OLD;
			unsigned int uSize = m_pIndexList[nElemIndex].lCompressSizeFlag & (~TYPE_FILTER_OLD);

			if (ExtractRead(pDataBuffer,
				m_pIndexList[nElemIndex].lSize,
				lCompressType,
				m_pIndexList[nElemIndex].uOffset,
				uSize) == false)
			{
				free(pDataBuffer);
				pDataBuffer = NULL;
			}
		}
		return pDataBuffer;
	}
};