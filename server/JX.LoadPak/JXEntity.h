#pragma once

#define	XPACKFILE_SIGNATURE_FLAG		0x4b434150	//'PACK'
#define	REGION_GRID_WIDTH	16
#define	REGION_GRID_HEIGHT	32

enum XPACK_METHOD
{
	TYPE_NONE = 0x00000000,
	TYPE_UCL = 0x10000000,
	TYPE_BZIP2 = 0x20000000,
	TYPE_FRAGMENT = 0x30000000,
	TYPE_FRAGMENTA = 0x40000000,
	TYPE_FRAME = 0x10000000,
	TYPE_METHOD_FILTER = 0xF0000000,
	TYPE_FILTER = 0xF0000000,
	TYPE_FILTER_SIZE = 0x07FFFFFF,
	TYPE_UCL_OLD = 0x01000000,
	TYPE_BZIP2_OLD = 0x02000000,
	TYPE_METHOD_FILTER_OLD = 0x0f000000,
	TYPE_FILTER_OLD = 0xff000000,
	TYPE_FRAGMENT_OLD = 0x03000000,
	TYPE_FRAGMENTA_OLD = 0x04000000,
};

struct XPackFileHeader
{
	unsigned char cSignature[4];
	unsigned int uCount;
	unsigned int uIndexTableOffset;
	unsigned int uDataOffset;
	unsigned int uCrc32;
	unsigned int  uPakTime;
	unsigned char cReserved[8];
};
struct XPackIndexInfo
{
	unsigned int	uId;
	unsigned int	uOffset;
	int			lSize;
	int			lCompressSizeFlag;
};
struct XPackElemFileRef
{
	unsigned int uId;
	int nPackIndex;
	int nElemIndex;
	//int nCacheIndex;
	unsigned int nOffset;
	int nSize;
};
struct KCombinFileSection
{
	unsigned int uOffset;
	unsigned int uLength;
};
struct KObjFileHead
{
	unsigned int uNumObj;
	unsigned int uReserved[2];
};
enum SCENE_FILE_INDEX
{
	REGION_OBSTACLE_FILE_INDEX = 0,//"OBSTACLE.DAT"
	REGION_TRAP_FILE_INDEX,			//"Trap.dat"
	REGION_NPC_FILE_INDEX,			//"Npc_S.dat" or "Npc_C.dat"
	REGION_OBJ_FILE_INDEX,			//"Obj_S.dat" or "Obj_C.dat"
	REGION_GROUND_LAYER_FILE_INDEX,	//"Ground.dat"
	REGION_BUILDIN_OBJ_FILE_INDEX,	//"BuildinObj.Dat"

	REGION_ELEM_FILE_COUNT
};

struct KSPTrap
{
	unsigned char	cX;
	unsigned char	cY;
	unsigned char	cNumCell;
	unsigned char	cReserved;
	unsigned int	uTrapId;
};
#define	MAX_RESOURCE_FILE_NAME_LEN	128
struct KSPNpc
{
	int					nTemplateID;
	int					nPositionX;
	int					nPositionY;
	bool				bSpecialNpc;
	char				cReserved[3];
	char				szName[32];
	short				nLevel;
	short				nCurFrame;
	short				nHeadImageNo;
	short				shKind;
	unsigned char		cCamp;
	unsigned char		cSeries;
	unsigned short		nScriptNameLen;
	char				szScript[MAX_RESOURCE_FILE_NAME_LEN];
	char                nNDropFile[MAX_RESOURCE_FILE_NAME_LEN];
	char                nGDropFile[MAX_RESOURCE_FILE_NAME_LEN];
	//KTriDimensionCoord	Pos;
};
struct KTriDimensionCoord
{
	int x, y, z;
};
struct KSPObj
{
	int					nTemplateID;
	short				nState;
	unsigned short		nBioIndex;
	KTriDimensionCoord	Pos;
	char				nDir;
	bool				bSkipPaint;
	unsigned short		nScriptNameLen;
	char				szScript[MAX_RESOURCE_FILE_NAME_LEN];
};