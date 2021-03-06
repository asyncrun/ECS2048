﻿using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;

public struct Player : IComponentData { }
public struct TextUI : IComponentData { public int Index; }
public struct ScoreHolder : IComponentData { public int Value; }
public struct Input : IComponentData { public int Direction; }

public struct MoveDirection
{
    public const int Up = 2;
    public const int Down = -2;
    public const int Left = -1;
    public const int Right = 1;
}

public struct Block : IComponentData
{
    public int Value;
    public int SelfArrayIndex;
    public int2 PosIndex;
    public int2 NextBlockArrayIndex;
    public int2 PrevBlockArrayIndex;
    public bool1 Changed;

    public static Block Null {
        get {
            return new Block() {
                SelfArrayIndex = -1,
                NextBlockArrayIndex = -1,
                PrevBlockArrayIndex = -1,
                Value = -1,
                PosIndex = -1
            };
        }
    }
}
