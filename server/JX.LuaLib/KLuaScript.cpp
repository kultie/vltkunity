#include <string.h>
#include "KLuaScript.h"

Lua_State* KLuaScript_Init(int size)
{
	Lua_State* m_LuaState = lua_open(size);
	if (m_LuaState == NULL)
		return NULL;
	
	Lua_OpenBaseLib(m_LuaState);
	Lua_OpenIOLib(m_LuaState);
	Lua_OpenStrLib(m_LuaState);
	Lua_OpenMathLib(m_LuaState);
	Lua_OpenDBLib(m_LuaState);

	return m_LuaState;
}
void KLuaScript_Close(Lua_State* L)
{
	lua_close(L);
}

void KLuaScript_Register(Lua_State* L, int n, TLua_Func* Funcs)
{
	for (int i = 0; i < n; i++)
		Lua_Register(L, Funcs[i].name, Funcs[i].func);
}

#include <fstream>
#include <stdlib.h>
#include <stdio.h>
bool KLuaScript_Load(Lua_State* L, wchar_t* path, bool include)
{
	FILE* f = _wfopen(path, L"r");
	if (f == NULL) return false;

	fseek(f, 0L, SEEK_END);
	
	int size = ftell(f);

	char* pBuffer = (char*)malloc(size + 1);
	if (pBuffer == NULL)
	{
		fclose(f);
		return false;
	}
	memset(pBuffer, 0, size + 1);

	fseek(f, 0L, SEEK_SET);
	fread(pBuffer, size, 1, f);
	fclose(f);

	bool result = false;
	if (include)
	{
		if (Lua_ExecuteBuffer(L, pBuffer, size + 1, NULL) == 0)
		{
			result = true;
		}
	}
	else
	{
		if (Lua_CompileBuffer(L, pBuffer, size + 1, NULL) == 0)
		{
			if (Lua_Execute(L) == 0)
			{
				result = true;
			}
		}
	}

	free(pBuffer);
	return result;
}
#include <stdarg.h>
bool KLuaScript_CallFunctionS(Lua_State* L, char* cFuncName, int nResults, char* cFormat, va_list vlist)
{
	Lua_GetGlobal(L, cFuncName);

	double nNumber;
	int nArgnum = 0;

	if (cFormat)
	{
		int n = strlen(cFormat), i= 0;
		while (i < n && cFormat[i] != '\0')
		{
			switch (cFormat[i])
			{
				case 'n':
				{
					nNumber = va_arg(vlist, double);
					Lua_PushNumber(L, nNumber);
					nArgnum++;
				}
				break;

				case 'd':
				{
					nNumber = (double)va_arg(vlist, int);
					Lua_PushNumber(L, nNumber);
					nArgnum++;
				}
				break;

				case 's':
				{
					char* cString = va_arg(vlist, char*);
					Lua_PushString(L, cString);
					nArgnum++;
				}
				break;

				case 'N'://NULL
				{
					Lua_PushNil(L);
					nArgnum++;
				}
				break;

				case 'f':
				{
					Lua_CFunction CFunc = va_arg(vlist, Lua_CFunction);
					Lua_PushCFunction(L, CFunc);
					nArgnum++;
				}
				break;

				case 'v':
				{
					nNumber = va_arg(vlist, int);
					Lua_PushValue(L, (int)nNumber);
					nArgnum++;
				}
				break;

				case 't':
				{
				}
				break;

				case 'p':
				{
					void* pPoint = va_arg(vlist, void*);
					Lua_PushUserTag(L, pPoint, true);
					nArgnum++;
				}
				break;
			}
			i++;
		}
	}
	return Lua_Call(L, nArgnum, nResults) == 0;
}
bool KLuaScript_CallFunction(Lua_State* L, char* cFuncName, int nResults, char* cFormat, ...)
{
	if (!cFuncName)
		return false;

	va_list vlist;
	va_start(vlist, cFormat);
	bool bResult = KLuaScript_CallFunctionS(L, cFuncName, nResults, cFormat, vlist);
	va_end(vlist);

	return bResult;
}
bool KLuaScript_Call(Lua_State* L, char* cFuncName)
{
	return KLuaScript_CallFunctionS(L, cFuncName, 0, NULL, NULL);
}
bool KLuaScript_CallMe(Lua_State* L, char* cFuncName, int id)
{
	return KLuaScript_CallFunction(L, cFuncName, 0, "d", id);
}
