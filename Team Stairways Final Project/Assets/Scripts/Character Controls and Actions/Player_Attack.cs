using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public GameObject defaultItem;
    private GameObject[] itemList = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        itemList[0] = defaultItem;
        Instantiate(defaultItem);
        for(int i = 1; i < itemList.Length; i++)
        {
            itemList[i] = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
