
using System;

namespace game.resource.settings.npcres
{
    public class ServeMove
    {
        public class Result
        {
            public int nRet;
            public int nDir;
            public resource.map.Position position;

            public Result(int nRet, int nDir, resource.map.Position position)
            {
                this.nRet = nRet;
                this.nDir = nDir;
                this.position = position;
            }

            public Result(int nRet)
            {
                this.nRet = nRet;
            }

            public bool Successful()
            {
                return this.nRet == 1;
            }
        }

        private static int findOnSideState = 0;

        private static bool CheckBarrier(resource.Map map, int sourceX, int sourceY, resource.map.Position pos, int dir, int speed)
        {
            pos.left = resource.settings.skill.Static.g_DirCos(dir, 64) * speed;
            pos.top = resource.settings.skill.Static.g_DirSin(dir, 64) * speed;

            pos.left = sourceX + (pos.left >> 10);
            pos.top = sourceY + (pos.top >> 10);

            return map.GetBarrier(pos) == 0;
        }

        public static ServeMove.Result GetDir(resource.Map map, resource.map.Position cur, UnityEngine.Vector2 move, float speedToFind)
        {
            resource.map.Position des = new map.Position();
            int nWantDir = resource.settings.skill.Static.g_GetDirIndex(cur.left, cur.top, cur.left + (int)(move.x * 1000), cur.top - (int)(move.y * 1000));

            des.left = cur.left + (int)move.x;
            des.top = cur.top - (int)move.y;

            if(map.GetBarrier(des) == 0)
            {   // tìm hướng theo con trỏ

                ServeMove.findOnSideState = 0;
                return new ServeMove.Result(1, resource.settings.skill.Static.Dir64To8(nWantDir) + 1, des);
            }

            // va chạm với chướng ngại vật

            int speed = speedToFind > 10 ? 10 : (int)speedToFind;
            int nTempDir8, nTempDir64;
            nTempDir8 = resource.settings.skill.Static.Dir64To8(nWantDir);

            for (int index = 1; index < 4; index++)
            {
                if (ServeMove.findOnSideState == 0 || ServeMove.findOnSideState == 1)
                {   // tìm hướng bên phải con trỏ

                    nTempDir64 = resource.settings.skill.Static.Dir8To64((nTempDir8 + index) & 0x07);

                    if (ServeMove.CheckBarrier(map, cur.left, cur.top, des, nTempDir64, speed))
                    {
                        ServeMove.findOnSideState = 1;
                        return new ServeMove.Result(1, resource.settings.skill.Static.Dir64To8(nTempDir64) + 1, des);
                    }
                }

                if (ServeMove.findOnSideState == 0 || ServeMove.findOnSideState == 2)
                {   // tìm hướng bên trái con trỏ

                    nTempDir64 = resource.settings.skill.Static.Dir8To64((nTempDir8 - index) & 0x07);

                    if (ServeMove.CheckBarrier(map, cur.left, cur.top, des, nTempDir64, speed))
                    {
                        ServeMove.findOnSideState = 2;
                        return new ServeMove.Result(1, resource.settings.skill.Static.Dir64To8(nTempDir64) + 1, des);
                    }
                }
            }

            nTempDir64 = resource.settings.skill.Static.Dir8To64((nTempDir8 + 4) & 0x07);

            if (ServeMove.CheckBarrier(map, cur.left, cur.top, des, nTempDir64, speed))
            {   // tìm hướng sau lưng con trỏ
                // TODO: nếu không tìm ra hướng, có thể giảm khoảng cách tìm kiếm lại ở đoạn này

                ServeMove.findOnSideState = 0;
                return new ServeMove.Result(1, resource.settings.skill.Static.Dir64To8(nTempDir64) + 1, des);
            }

            return new ServeMove.Result(0);
        }
    }
}
