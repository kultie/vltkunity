namespace Photon.ShareLibrary.Handlers
{
    public enum ParamterCode : byte
    {
        Data = 0,
        Message,

        Account,
        Password,
        Password2,
        UserId,
        CharacterId,
        CharacterName,

        Id,
        Title,
        Name,
        Sex,
        Level,
        Exp,
        Kind,
        Series,
        Cam,
        HPCur,
        HPMax,
        MPCur,
        MPMax,
        SPCur,
        SPMax,
        Fight,

        ////
        Dir,
        MapX,
        MapY,
        MapId,
        ////
        
        SkillId,
        SkillLevel,
        SkillEnChance,

        NpcType,

        ActionId,
        FactionId,
        TongId,

        ItemId,
        IsEquip,
        TaskId,
        TaskValue
    }

    public enum OperationCode : byte
    {
        ServerRegister = 0,
        ServerCharge,
        NotifyPlayer,
        
        ServerTask,
        ServerTong,

        TeamCommand,
        TongCommand,

        Login,
        Register,
        GetCharacters,
        SelectCharacter,
        CreateCharacter,

        OpenBox,
        DoChat,
        DoDie,
        DoSit,
        DoRun,
        DoMove,
        StopMove,
        PickItem,

        // sync event
        NpcSale,
        NpcSkill,
        NpcDamage,
        NpcExp,

        NpcQuery,
        NpcQuest,
        NpcSelect,
        NpcTalk,
        NpcCallback,

        SendWorld,// init
        SyncWorld,// dong bo hieu ung
        WorldJoin,
        WorldLoaded,// client da san sang 

        SendPlayer,// goi full ban dau
        SyncPlayer,// dong bo trang thai
        SyncPlayerDel,// remove khoi khu vuc

        SendNpc,// goi full ban dau
        SyncNpc,// dong bo trang thai
        SyncNpcDel,// remove khoi khu vuc

        SendObj,
        SyncObj,
        SyncObjDel,

        SyncCharAttr,//bo
        SyncCharSkill,
        SyncCharItem,
        SyncCharTask,
        SyncCharFriend,

        // Player 
        AutoEquip,
        AddItem,
        RemoveItem,
        DoTask,
    }
}
