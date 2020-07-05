using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Website: https://www.youtube.com/watch?v=2WnAOV7nHW0
/// </summary>

//this is a simple class, so no monobehavior elements
public class InventoryHelp {    
    private List<Weapons> weapons;

    public InventoryHelp() {
        weapons = new List<Weapons>();
        //below are the items that will be preloaded into our inventory
        AddWeapon(new Weapons { equipedWeapon = Weapons.WeaponType.Gun, amount = 1});
        AddWeapon(new Weapons { equipedWeapon = Weapons.WeaponType.Sword, amount = 1});
        AddWeapon(new Weapons { equipedWeapon = Weapons.WeaponType.Wand, amount = 1});
        AddWeapon(new Weapons { equipedWeapon = Weapons.WeaponType.RobotArms, amount = 1});
    }

    public void AddWeapon(Weapons _weapon) {
        weapons.Add(_weapon);
    }

    public List<Weapons> GetWeaponsList() {
        return weapons;
    }
}
