// pch.h: This is a precompiled header file.
// Files listed below are compiled only once, improving build performance for future builds.
// This also affects IntelliSense performance, including code completion and many code browsing features.
// However, files listed here are ALL re-compiled if any one of them is updated between builds.
// Do not add files here that you will be updating frequently as this negates the performance advantage.

#ifndef PCH_H
#define PCH_H

// add headers that you want to pre-compile here
#include "framework.h"

extern "C"
{
	extern __declspec(dllexport) unsigned int FileNameToId(char*,int);
	extern __declspec(dllexport) int LoadPackages(char*);

	extern __declspec(dllexport) int ExtractElem(char*,char*);
	extern __declspec(dllexport) bool ExtractFile(char*);

	extern __declspec(dllexport) bool LoadObstacle(UINT*);

	extern __declspec(dllexport) int ReadTraps();
	extern __declspec(dllexport) bool LoadTrap(void*);

	extern __declspec(dllexport) int ReadNpcs();
	extern __declspec(dllexport) bool LoadNpc(void*);

	extern __declspec(dllexport) int ReadObjs();
	extern __declspec(dllexport) bool LoadObj(void*);
}

#endif //PCH_H
