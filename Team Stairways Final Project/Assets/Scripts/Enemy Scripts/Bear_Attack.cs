using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear_Attack : MonoBehaviour
{
    AudioSource chomp;
    float biteTime = 2.0f;
    float chewTime = 2.0f;

    // Start is called before the first frame update
    void Start() {
        chomp = GetComponent<AudioSource>();

    }

    private void OnTriggerEnter(Collider other) {
        /*if(biteTime > Time.time) {
            if (other.tag == "Player") {
                chomp.Play();
                PlayerStats.AddHealth(-1);
            }
        }
        biteTime += chewTime;
        chewTime += biteTime + Time.time;*/

        if (other.tag == "Player") {
            chomp.Play();
            PlayerStats.AddHealth(-1);
        }
    }
}
