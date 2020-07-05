using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//this is the UI_Inventory (the displayed inventory found in the video link below)
public class equipmentDisplay : MonoBehaviour
{
    public Image[] equipment;
    //public Sprite equip1;       //ranged pic
    //public Sprite equip2;       //melee pic
    //public Sprite equip3;       //robot arms pic
    //public Sprite equip4;       //wand pic

    public static int currentEquip;


    //for the inventory code below: https://www.youtube.com/watch?v=2WnAOV7nHW0&t=330s
    private InventoryHelp inventoryHelp;

    public void SetInventory(InventoryHelp inventory) {
        this.inventoryHelp = inventory;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 1; i <= equipment.Length; i++)
        {
            if (i == (currentEquip))
            {
                equipment[i-1].enabled = true;
            }
            else
            {
                equipment[i-1].enabled = false;
            }
        }
    }

    //Call this when the player changes equipment
    public static void ChangeCurrentEquip(int val)
    {
        if ((val <= 2) && (val >= 1))
        {
            currentEquip = val;
        }
    }

    //not sure if we need this
    /*private void RefreshInventory() {
        foreach(Weapons weaponIn in inventoryHelp.GetWeaponsList()) {
            
        }
    }*/
}
