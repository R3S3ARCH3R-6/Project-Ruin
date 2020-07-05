using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCharController : MonoBehaviour
{

    Rigidbody playerRigidbody;
    public float speed = 10;
    Vector3 movement;
    int floorMask;
    private float camRayLength = 100;


    private void Awake() {
        playerRigidbody = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Floor");
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        //CharacterMovement();    //enables the character to have simple motions
    }

    private void FixedUpdate() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        newCharacterMovement(h, v);
        followMouse();

    }

    /// <summary>
    /// CharacterMovement provides the character with basic movement controls (these are the DOOM controls)
    /// </summary>
    void CharacterMovement() {
        float x = Input.GetAxis("Horizontal");  //access the horizontal keys (left, right, a, d)
        float z = Input.GetAxis("Vertical");    //access the vertical keys (up, down, w, s)

        float rotateSpeed = 2.50f;    //speed of the character's rotation
        float moveSpeed = 5.0f;     //speed of the character's movement
        Vector3 turnVelocity = new Vector3(0f, x * rotateSpeed, 0f);    //the velocity of the rotations/is the Vector3 of the rotation movement

        transform.position += transform.forward * z * moveSpeed * Time.deltaTime;

        transform.Rotate(turnVelocity); //rotates the player left and right

    }

    /// <summary>
    /// These are the Survival shooter controls (need to be changed)
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    void newCharacterMovement(float h, float v) {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void followMouse() {
        /*Ray camray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorhit;
        if (Physics.Raycast(camray, out floorhit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorhit.point - transform.position;
            playerToMouse.y = 0;
            Quaternion newrot = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newrot);
        }*/

        Vector3 camPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 playerToMouse = camPos - transform.position;
        playerToMouse.y = 0;
        Quaternion newrot = Quaternion.LookRotation(playerToMouse);
        playerRigidbody.MoveRotation(newrot);


    }
}
