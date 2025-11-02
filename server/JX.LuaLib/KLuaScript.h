#include "LuaLib.h"

typedef struct {
	const char* name;
	lua_CFunction func;
}TLua_Func;

extern "C" 
{
	LUALIB_API Lua_State* KLuaScript_Init(int);
	LUALIB_API void KLuaScript_Close(Lua_State*);

	LUALIB_API void KLuaScript_Register(Lua_State*, int, TLua_Func*);
	LUALIB_API bool KLuaScript_Load(Lua_State*, wchar_t*, bool);

	LUALIB_API bool KLuaScript_Call(Lua_State*, char*);
	LUALIB_API bool KLuaScript_CallMe(Lua_State*, char*, int);
	LUALIB_API bool KLuaScript_CallFunctionS(Lua_State* m_LuaState, char* cFuncName, int nResults, char* cFormat, va_list vlist);
}