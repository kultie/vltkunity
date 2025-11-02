#pragma once

#include "JXEntity.h"

class KMapHelper
{
	int dwHeadSize;
	KCombinFileSection sElemFile[REGION_ELEM_FILE_COUNT];

public:
	void SaveSize(int size, void* buf)
	{
		dwHeadSize = size;
		memcpy(&sElemFile, buf, sizeof(sElemFile));
	}

	int GetSize() { return dwHeadSize; }
	KCombinFileSection GetData(int idx) { return sElemFile[idx]; }
};