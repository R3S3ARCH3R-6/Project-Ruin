using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class miniMap : MonoBehaviour
{
    public RawImage map;
    public RawImage minimap;
    //private bool mapActive = false;
    // Start is called before the first frame update
    void Start()
    {
        map.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            map.enabled = true;
            minimap.enabled = false;
            //mapActive = true;
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            map.enabled = false;
            minimap.enabled = true;
        }
    }
}
