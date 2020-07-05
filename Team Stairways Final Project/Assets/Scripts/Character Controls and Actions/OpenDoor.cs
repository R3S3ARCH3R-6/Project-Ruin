using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject doorToOpen;
    bool playerHere = false;

    public GameObject nextRoom;

    // Start is called before the first frame update
    void Start()
    {
        playerHere = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHere)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                gameObject.SetActive(false);
                nextRoom.SetActive(true);
                doorToOpen.GetComponent<Animator>().enabled = true;
                interactUI.buttonDestroyed();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHere = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHere = false;
        }
    }
}
