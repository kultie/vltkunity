// pch.cpp: source file corresponding to the pre-compiled header

#include "pch.h"
#include <stdio.h>

#include "XPackFile.h"
#include "KPakFile.h"

char count;
XPackFile* packages[10];

KPakFile pakFile;
KMapHelper jxSPMap;
int LoadPackages(char* root)
{
	char ini[MAX_PATH],path[MAX_PATH],file[MAX_PATH], str[32], key[3];

	int length = sizeof(str) / sizeof(char);
	memset(str, 0, sizeof(str));

	sprintf_s(ini, "%s/package.ini", root);

	count = 0;

	if (GetPrivateProfileString("Package", "Path", "", str, length, ini) > 0)
	{
		char idx = 0;
		sprintf_s(path, "%s/%s", root, str);

		while (true)
		{
			memset(str, 0, sizeof(str));
			sprintf_s(key, "%d", idx);

			if (GetPrivateProfileString("Package", key, "", str, length, ini) > 0)
			{
				sprintf_s(file, "%s/%s", path, str);

				XPackFile* xf = new XPackFile();
				if (xf->LoadPackage(count, file))
				{
					packages[count] = xf;
					++count;
				}
				else
				{
					delete xf;
				}
				++idx;
			}
			else
			{
				break;
			}
		}
		pakFile.Init();
	}
	return count;
}
unsigned int FileNameToId(char* ptr, int size)
{
	unsigned int id = 0;
	int index = 0;

	while (index < size)
	{
		if (*ptr >= 'A' && *ptr <= 'Z')
			id = (id + (++index) * (*ptr + 'a' - 'A')) % 0x8000000b * 0xffffffef;
		else
			id = (id + (++index) * (*ptr)) % 0x8000000b * 0xffffffef;
		ptr++;
	}

	return (id ^ 0x12345678);
}

bool FindElemPackage(unsigned int uId, XPackElemFileRef& ElemRef)
{
	for (char index = 0; index < count; index++)
	{
		if (packages[index]->XFindElemFile(uId, ElemRef))
			return true;
	}
	return false;
}
bool FindElemFile(char* root, XPackElemFileRef& ElemRef)
{
	unsigned int uId = FileNameToId(root, strlen(root));
	return FindElemPackage(uId, ElemRef);
}

int ExtractElem(char* root,char* buf)
{
	XPackElemFileRef ElemRef;
	if (FindElemFile(root, ElemRef))
	{
		char* s = (char*)packages[ElemRef.nPackIndex]->ExtractElem(ElemRef.nElemIndex);
		memcpy(buf, s, ElemRef.nSize);

		return ElemRef.nSize;
	}
	return 0;
}
bool ExtractFile(char* root)
{
	XPackElemFileRef ElemRef;
	if (FindElemFile(root, ElemRef))
	{
		void* buf = packages[ElemRef.nPackIndex]->ExtractElem(ElemRef.nElemIndex);
		if (buf)
		{
			pakFile.Load(buf, ElemRef.nSize);
			return pakFile.LoadData(&jxSPMap);
		}
	}
	return false;
}

bool LoadObstacle(UINT* buf)
{
	return pakFile.LoadObstacle(&jxSPMap,buf);
}

int ReadTraps() 
{
	return pakFile.ReadTraps(&jxSPMap);
}
bool LoadTrap(void* buf)
{
	KSPTrap* sTrapCell = (KSPTrap*)buf;
	return pakFile.LoadTrap(sTrapCell);
}

int ReadNpcs()
{
	return pakFile.ReadNpcs(&jxSPMap);
}
typedef struct 
{
	int nTemplateID;
	int nPositionX;
	int nPositionY;
	unsigned char Kind;
	unsigned char Camp;
	unsigned char Level;

	unsigned int Scripts;
} KSPNpcExt;
bool LoadNpc(void* buf)
{
	KSPNpc sNpcCell;
	if (!pakFile.LoadNpc(&sNpcCell))
		return false;

	KSPNpcExt* sNpcData = (KSPNpcExt*)buf;
	sNpcData->nTemplateID = sNpcCell.nTemplateID;
	sNpcData->nPositionX = sNpcCell.nPositionX;
	sNpcData->nPositionY = sNpcCell.nPositionY;
	sNpcData->Kind = sNpcCell.shKind < 0 ? 3 : sNpcCell.shKind;
	sNpcData->Camp = sNpcCell.cCamp;
	sNpcData->Level = sNpcCell.nLevel <= 0 ? 1 : sNpcCell.nLevel;
	
	sNpcData->Scripts = FileNameToId(sNpcCell.szScript,strlen(sNpcCell.szScript));
	return true;
}

int ReadObjs()
{
	return pakFile.ReadTraps(&jxSPMap);
}
typedef struct
{
	int nTemplateID;
	int nPositionX;
	int nPositionY;
	unsigned char Dir;

	unsigned int Scripts;
} KSPObjExt;
bool LoadObj(void* buf)
{
	KSPObj sObjCell;
	if (!pakFile.LoadObj(&sObjCell))
		return false;

	KSPObjExt* sObjData = (KSPObjExt*)buf;
	sObjData->nTemplateID = sObjCell.nTemplateID;
	sObjData->nPositionX = sObjCell.Pos.x;
	sObjData->nPositionY = sObjCell.Pos.y;
	sObjData->Dir = sObjCell.nDir;

	sObjData->Scripts = FileNameToId(sObjCell.szScript, strlen(sObjCell.szScript));
	return true;
}