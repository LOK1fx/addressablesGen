using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LocalPlayer
{
    public static Player Player { get; private set; }
    public static Vector3 Position
    {
        get
        {
            return Player.transform.position;
        }
    }

    public static void Initialize(Player player)
    {
        Player = player;
    }
}