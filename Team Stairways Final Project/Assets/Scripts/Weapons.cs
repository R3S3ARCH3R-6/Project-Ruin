using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Website: https://www.youtube.com/watch?v=2WnAOV7nHW0
/// </summary>

public class Weapons {
    public enum WeaponType {
        Sword,
        Gun,
        Wand,
        RobotArms
    }

    public WeaponType equipedWeapon;
    public int amount = 1; //not really needed here? (used to be safe to follow the video)
}
