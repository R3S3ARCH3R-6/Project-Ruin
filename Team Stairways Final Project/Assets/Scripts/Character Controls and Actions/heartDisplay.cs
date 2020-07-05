using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heartDisplay : MonoBehaviour
{
    public Image[] hearts;      //array of heart sprites
    public Sprite fullHeart;    //sprite for full heart
    public Sprite emptyHeart;   //sprite for empty heart
    public int numberOfHearts;  //number of hearts being displayed


    // Start is called before the first frame update
    void Start()
    {
        numberOfHearts = PlayerStats.playerHealth;
    }

    // Update is called once per frame
    void Update() { 

        if (PlayerStats.playerHealth > numberOfHearts)
        {
            numberOfHearts = PlayerStats.playerHealth;
        }

        for (int i = 0; i<hearts.Length; i++)   //go through heart array
        {
            if(i < PlayerStats.playerHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numberOfHearts)
            {
                hearts[i].enabled = true;
            } else
            {
                hearts[i].enabled = false;
            }
        }

    }
}
