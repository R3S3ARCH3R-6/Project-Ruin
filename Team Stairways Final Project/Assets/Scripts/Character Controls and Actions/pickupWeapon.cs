using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pickupWeapon : MonoBehaviour
{
    public TextMeshProUGUI interactText;            //"Press E to interact"
    public bool isfaded;                            //faded state
    public float fadeTime = 0.4f;                   //time it takes to fade in/out
    public static bool gunGone = false;
    public static bool meleeGone = false;


    private void Start()
    {
        gunGone = false;
        meleeGone = false;
        interactText.gameObject.GetComponent<CanvasGroup>().alpha = 0;  //make sure the text is not there on start
    }

    private void Update()
    {
        if (gunGone)
        {
            print("gun is gone and imma FADE");
            fade();
            gunGone = false;
        }
        if (meleeGone)
        {
            print("sword is gone and imma FADE");
            fade();
            meleeGone = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Melee"))
        {
            fade();
        }
        if (other.CompareTag("Ranged"))
        {
            fade();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Melee"))
        {
            fade();
        }
        if (other.CompareTag("Ranged"))
        {
            fade();
        }
    }

    /// <summary>
    /// This function Gets the canvas group component to control the alpha & make it fade in/out with the coroutine.
    /// </summary>
    public void fade()
    {
        var canvGroup = interactText.gameObject.GetComponent<CanvasGroup>();
        StartCoroutine(FadeIn(canvGroup, canvGroup.alpha, isfaded ? 0 : 1));

        isfaded = !isfaded;
    }

    public IEnumerator FadeIn(CanvasGroup canvGroup, float start, float end)
    {
        float counter = 0f;

        while (counter < fadeTime)
        {
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / fadeTime);
            yield return null;
        }
    }

    public static void gunDestroyed()
    {
        gunGone = true;
    }

    public static void MeleeDestroyed()
    {
        meleeGone = true;
    }
}
