using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.ShareLibrary.Constant
{
    public enum Obstacle_Type: byte
    {
        Obstacle_Empty = 0,
        Obstacle_Full,
        Obstacle_LT,
        Obstacle_RT,
        Obstacle_LB,
        Obstacle_RB,
        Obstacle_Type_Num,
    };

    public enum Obstacle_Kind: byte
    {
        Obstacle_NULL = 0,
        Obstacle_Normal,
        Obstacle_Fly,
        Obstacle_Jump,
        Obstacle_JumpFly,
        Obstacle_Kind_Num,m
    };
}
