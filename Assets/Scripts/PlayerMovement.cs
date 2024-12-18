using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float mouseSensivity;
    [SerializeField] private float jumpPower;
    public Camera fpsCam;

    public LayerMask ground;
    Rigidbody rb;
    Vector3 direction;
    CapsuleCollider capsuleCol;

    float mouseX, mouseY;
    float rotX, rotY;
    void Start()
    {
        capsuleCol = gameObject.GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }
        
    void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        
        direction = new Vector3(hor, 0, ver);

        mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensivity;
        mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensivity;

        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, -90, 90);

        rotY += mouseX;
        transform.localRotation = Quaternion.Euler(0, rotY, 0);

        if (Input.GetButtonDown("Jump"))
        {
            if (OnGround()) 
            {
                Jump();
            }
            
        }
    }
    private void LateUpdate()
    {
        fpsCam.transform.localRotation = Quaternion.Euler(rotX,0,0);
    }

    bool OnGround()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, Vector3.down, out hit,capsuleCol.bounds.size.y/2 , ground))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void FixedUpdate()
    {
        
        rb.MovePosition(transform.position + transform.TransformDirection(direction* Time.fixedDeltaTime * moveSpeed));
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpPower);
    }
}
