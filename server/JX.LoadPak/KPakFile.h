#pragma once

#include "KMapHelper.h"

class KPakFile
{
	char* buffer;
	int cur, size;

public:
	void Init()
	{
		buffer = NULL;
		cur = size = 0;
	}
	void Load(void* buf, int siz)
	{
		if (buffer)
		{
			free(buffer);
			buffer = NULL;
		}
		buffer = (char*)buf;
		size = siz;
		cur = 0;
	}
	void Seek(int nOffset, unsigned int uMethod)
	{
		if (uMethod == FILE_BEGIN)
			cur = nOffset;
		else if (uMethod == FILE_END)
			cur = size + nOffset;
		else
			cur += nOffset;

		if (cur > size)
			cur = size;
		else
		if (cur < 0)
			cur = 0;
	}

	bool Read(void* buf, int length)
	{
		if (cur + length <= size)
		{
			memcpy(buf, buffer + cur, length);
			cur += length;
			return true;
		}
		return false;
	}
	bool LoadData(KMapHelper* helper)
	{
		if (size < sizeof(UINT) + sizeof(KCombinFileSection) * REGION_ELEM_FILE_COUNT)
			return false;

		UINT	dwMaxElemFile = 0;
		Read(&dwMaxElemFile, sizeof(UINT));

		KCombinFileSection sElemFile[REGION_ELEM_FILE_COUNT];
		memset(&sElemFile, 0, sizeof(sElemFile));

		if (dwMaxElemFile > REGION_ELEM_FILE_COUNT)
		{
			Read(&sElemFile, sizeof(sElemFile));
			Seek(sizeof(KCombinFileSection) * (dwMaxElemFile - REGION_ELEM_FILE_COUNT), FILE_CURRENT);
		}
		else
		{
			Read(&sElemFile, sizeof(sElemFile));
		}
		helper->SaveSize(sizeof(UINT) + sizeof(KCombinFileSection) * dwMaxElemFile, &sElemFile);

		return true;
	}

	bool LoadObstacle(KMapHelper* helper, UINT* buf)
	{
		UINT anObstacle[REGION_GRID_WIDTH][REGION_GRID_HEIGHT];

		if (helper->GetData(REGION_OBSTACLE_FILE_INDEX).uLength != sizeof(anObstacle))
			return false;

		Seek(helper->GetSize() + helper->GetData(REGION_OBSTACLE_FILE_INDEX).uOffset, FILE_BEGIN);

		if (!Read((void*)anObstacle, sizeof(anObstacle)))
			return false;

		for (INT i = 0; i < REGION_GRID_WIDTH; ++i)
		{
			for (INT j = 0; j < REGION_GRID_HEIGHT; ++j)
			{
				*buf = anObstacle[i][j];
				buf++;
			}
		}
		return true;
	}

	int  ReadTraps(KMapHelper* helper)
	{
		if (helper->GetData(REGION_TRAP_FILE_INDEX).uLength < sizeof(KObjFileHead))
			return 0;

		Seek(helper->GetSize() + helper->GetData(REGION_TRAP_FILE_INDEX).uOffset, FILE_BEGIN);

		KObjFileHead sTrapFileHead;
		memset(&sTrapFileHead, 0, sizeof(sTrapFileHead));

		if (!Read(&sTrapFileHead, sizeof(sTrapFileHead)))
			return 0;

		if ((sTrapFileHead.uNumObj * sizeof(KSPTrap) + sizeof(KObjFileHead)) != helper->GetData(REGION_TRAP_FILE_INDEX).uLength)
			return 0;

		return sTrapFileHead.uNumObj;
	}

	bool LoadTrap(KSPTrap* sTrapCell)
	{
		memset(sTrapCell, 0, sizeof(sTrapCell));
		return Read(sTrapCell, sizeof(sTrapCell));
	}
	int  ReadNpcs(KMapHelper* helper)
	{
		if (helper->GetData(REGION_NPC_FILE_INDEX).uLength < sizeof(KObjFileHead))
			return 0;

		Seek(helper->GetSize() + helper->GetData(REGION_NPC_FILE_INDEX).uOffset, FILE_BEGIN);

		KObjFileHead sNpcFileHead;
		memset(&sNpcFileHead, 0, sizeof(sNpcFileHead));

		if (!Read(&sNpcFileHead, sizeof(sNpcFileHead)))
			return 0;

		return sNpcFileHead.uNumObj;
	}
	bool LoadNpc(KSPNpc* sNpcCell)
	{
		memset(sNpcCell, 0, sizeof(KSPNpc));

		if (!Read(sNpcCell, sizeof(KSPNpc) - sizeof(sNpcCell->szScript) - sizeof(sNpcCell->nNDropFile) - sizeof(sNpcCell->nGDropFile)))
			return false;

		if (sNpcCell->nScriptNameLen < sizeof(sNpcCell->szScript))
		{
			if (!Read(sNpcCell->szScript, sNpcCell->nScriptNameLen))
				return false;

			sNpcCell->szScript[sNpcCell->nScriptNameLen] = 0;
		}
		else
		{
			sNpcCell->szScript[0] = 0;
			Seek(sNpcCell->nScriptNameLen, FILE_CURRENT);
		}
		return true;
	}
	int  ReadObjs(KMapHelper* helper)
	{
		if (helper->GetData(REGION_OBJ_FILE_INDEX).uLength < sizeof(KObjFileHead))
			return 0;

		Seek(helper->GetSize() + helper->GetData(REGION_OBJ_FILE_INDEX).uOffset, FILE_BEGIN);

		KObjFileHead sNpcFileHead;
		memset(&sNpcFileHead, 0, sizeof(sNpcFileHead));

		if (!Read(&sNpcFileHead, sizeof(sNpcFileHead)))
			return 0;

		return sNpcFileHead.uNumObj;
	}
	bool LoadObj(KSPObj* sData)
	{
		memset(sData, 0, sizeof(KSPObj));

		if (!Read(sData, sizeof(KSPObj) - sizeof(sData->szScript)))
			return false;

		if (sData->nScriptNameLen < sizeof(sData->szScript))
		{
			if (!Read(sData->szScript, sData->nScriptNameLen))
				return false;

			sData->szScript[sData->nScriptNameLen] = 0;
		}
		else
		{
			sData->szScript[0] = 0;
			Seek(sData->nScriptNameLen, FILE_CURRENT);
		}
		return true;
	}
};