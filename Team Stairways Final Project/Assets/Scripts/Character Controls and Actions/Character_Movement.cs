using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This is a simple character control script - 4/7/20
/// </summary>
public class Character_Movement : MonoBehaviour
{
    Rigidbody playerRigidbody;
    public float speed = 10;
    Vector3 movement;
    int floorMask;
    private float camRayLength = 100;

    //[SerializeField] private InventoryHelp UI_inventory;
    [SerializeField] private equipmentDisplay UI_Weapons;
    private InventoryHelp inventory;
    //private Weapons equipedWeapons;

    PlayerStats playerStatsScript;
    Animator charAnimations;
    public bool runnning;
    bool gunShot;
    bool swordSlice;

    private void Awake()
    {
        playerStatsScript = GetComponent<PlayerStats>();
        playerRigidbody = GetComponent <Rigidbody>();
        floorMask = LayerMask.GetMask("Floor");
        //charAnimations = GetComponentInChildren<Animator>();
        charAnimations = transform.Find("The Adventurer Blake").GetComponent<Animator>();
        //charAnimations.SetBool("isRunning", false);
        swordSlice = playerStatsScript.swordHit;
        gunShot = playerStatsScript.shoot;
        //inventory code
        inventory = new InventoryHelp();
        UI_Weapons.SetInventory(inventory);
    }

    private void FixedUpdate()
    {
        swordSlice = playerStatsScript.swordHit;
        gunShot = playerStatsScript.shoot;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        newCharacterMovement(h, v);
        followMouse();
        CharAnim(h, v);

    }

    /// <summary>
    /// CharacterMovement provides the character with basic movement controls
    /// </summary>
    /*void CharacterMovement()
    {
        float x = Input.GetAxis("Horizontal");  //access the horizontal keys (left, right, a, d)
        float z = Input.GetAxis("Vertical");    //access the vertical keys (up, down, w, s)

        float rotateSpeed = 2.50f;    //speed of the character's rotation
        float moveSpeed = 5.0f;     //speed of the character's movement
        Vector3 turnVelocity = new Vector3(0f, x * rotateSpeed, 0f);    //the velocity of the rotations/is the Vector3 of the rotation movement

        transform.position += transform.forward * z * moveSpeed * Time.deltaTime;

        transform.Rotate(turnVelocity); //rotates the player left and right
    }*/

    void newCharacterMovement(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
        CharAnim(h, v); 
    }

    void followMouse()
    {
        Ray camray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorhit;
        if (Physics.Raycast(camray, out floorhit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorhit.point - transform.position;
            playerToMouse.y = 0;
            Quaternion newrot = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newrot);
        }

    }

    void CharAnim(float h, float v) {
        runnning = h != 0f || v != 0f;
        charAnimations.SetBool("isRunning",runnning);
        charAnimations.SetBool("Shooting", gunShot);
        charAnimations.SetBool("isHitting", swordSlice);
    }

    /*private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("RoomGreen")) {
            SceneManager.LoadScene("Entry Level G");
        }else if (other.gameObject.CompareTag("RoomRed")) {
            SceneManager.LoadScene("Entry Level R");
        } else if (other.gameObject.CompareTag("RoomBlue")) {
            SceneManager.LoadScene("Entry Level B");
        } else if (other.gameObject.CompareTag("RoomYellow")) {
            SceneManager.LoadScene("Entry Level Y");
        } else if (other.gameObject.CompareTag("RoomBoss")) {
            SceneManager.LoadScene("Boss Room");
        } else if (other.gameObject.CompareTag("RoomBonus")) {
            SceneManager.LoadScene("Bonus Room");
        }
    }*/
}
