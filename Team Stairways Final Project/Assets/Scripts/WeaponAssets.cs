using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAssets : MonoBehaviour
{

    public static WeaponAssets Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

}
