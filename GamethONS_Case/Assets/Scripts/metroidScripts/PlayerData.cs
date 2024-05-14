using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public bool hasDash;
    public bool hasWallJump;

    public PlayerData(Player player)
    {
        hasDash = player.hasDash;
        hasWallJump = player.hasWallJump;
    }
}
