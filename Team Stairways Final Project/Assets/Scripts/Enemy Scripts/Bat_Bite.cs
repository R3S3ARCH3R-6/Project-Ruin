using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Bat_Bite : MonoBehaviour
{
    AudioSource chomp;
    float biteTime = 2.0f;
    float chewTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        chomp = GetComponent<AudioSource>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            chomp.Play();
            //PlayerStats.playerHealth--;
            PlayerStats.AddHealth(-1);
        }
    }

    /*private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            chomp.Play();
            PlayerStats.playerHealth--;
        }
    }*/
}
