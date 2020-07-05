using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spike Trap Tutorial:https://www.youtube.com/watch?v=odJLo-tVAOA
/// Animation Delay: https://answers.unity.com/questions/35855/animation-delay-or-wait.html
/// </summary>

public class Spike_Trap : MonoBehaviour
{
    private float animationTime = 15f;
    private float refreshTime = 5f;
    private Animator trap;
    //Animation trap;

    // Start is called before the first frame update
    void Start()
    {
        trap = GetComponent<Animator>();
        StartCoroutine(SpikeTrapAlt());
    }

    /*private void FixedUpdate() {

        if(Time.time > animationTime) {
            SpikeTrapAlt();
        }
        animationTime += animationTime + Time.deltaTime;

    }*/

    IEnumerator SpikeTrapAlt() {
        //yield return new WaitForSeconds(5);
        while (true) {
            trap.Play("Spike Trap Animation");
            yield return new WaitForSeconds(5f);
        }
        
    }

}
