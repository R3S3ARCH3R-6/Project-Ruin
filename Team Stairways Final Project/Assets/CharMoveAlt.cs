using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMoveAlt : MonoBehaviour
{
    public float speed = 10;
    public float mouseSensitivity = 100f;
    //public Transform player;    //might be private if possible

    Vector3 movement;
    float xRotation = 0f;   //probably do not need
    float zRotation = 0f;

    private void Awake() {
        
        
    }

    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate() {
        CharacterMovement();
        MouseMovement();
    }

    /// <summary>
    /// CharacterMovement provides the character with basic movement controls
    /// </summary>
    void CharacterMovement() {
        float x = Input.GetAxis("Horizontal");  //access the horizontal keys (left, right, a, d)
        float z = Input.GetAxis("Vertical");    //access the vertical keys (up, down, w, s)

        float rotateSpeed = 2.50f;    //speed of the character's rotation
        float moveSpeed = 5.0f;     //speed of the character's movement
        Vector3 turnVelocity = new Vector3(0f, x * rotateSpeed, 0f);    //the velocity of the rotations/is the Vector3 of the rotation movement
        
        transform.position += (transform.forward * z).normalized * moveSpeed * Time.deltaTime;
        transform.position += (transform.right * x).normalized * moveSpeed * Time.deltaTime;
    }

    void MouseMovement() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;    //more for the "z" location here

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        zRotation -= mouseX;
        zRotation = Mathf.Clamp(zRotation, -360, 360);

        transform.localRotation = Quaternion.Euler(0f, zRotation, 0f);
        //transform.Rotate(Vector3.up * xRotation);
    }
}
