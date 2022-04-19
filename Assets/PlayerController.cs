using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float playerSpeed;
    public float playerJumpForce;
    public float playerRotationSpeed;
    //public Transform bulletLaunch;
    //public GameObject steveModelPrefabs;
    Rigidbody rb;
    public Animator animator;
    CapsuleCollider colliders;
    Quaternion camRotation;
    public Camera cam;
    Quaternion playerRotation;
    //float minX = -90f;
    //float maxX = 90f;

    //SpawnManager spawnManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        colliders = GetComponent<CapsuleCollider>();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetBool("IsShoot", !animator.GetBool("IsShoot"));
        }
    }
    private void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal") * playerSpeed;
        float inputz = Input.GetAxis("Vertical") * playerSpeed;

        transform.position += new Vector3(inputX, 0f, inputz);

        /* float mouseX = Input.GetAxis("Mouse X") * playerRotationSpeed;
         float mouseY = Input.GetAxis("Mouse Y") * playerRotationSpeed;
         //Debug.Log(mouseY);
         playerRotation = Quaternion.Euler(0f, mouseX, 0f);
         camRotation = Quaternion.Euler(-mouseY, 0f, 0f);
         camRotation = ClampRotationPlayer(camRotation);
         // this.transform.localRotation = playerRotation;
         transform.localRotation = playerRotation * transform.localRotation;
         cam.transform.localRotation = camRotation * cam.transform.localRotation;*/
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        transform.rotation = transform.rotation * Quaternion.Euler(0, mouseX *playerRotationSpeed, 0);
        Camera cam = GetComponentInChildren<Camera>();
        cam.transform.localRotation = Quaternion.Euler(-mouseY, 0, 0) * cam.transform.localRotation;

    }

    internal void TakeHit(int damageAmount)
    {
        throw new NotImplementedException();
    }

    /* public Quaternion ClampRotationPlayer(Quaternion n)
     {

         n.w = 1f;
         n.x /= n.w;
         n.y /= n.w;
         n.z /= n.w;
         float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(n.x);

         angleX = Mathf.Clamp(angleX, minX, maxX);
         n.x = Mathf.Tan(Mathf.Deg2Rad * angleX * 0.5f);
         return n;
     }

     internal void TakeHit(int damageAmount)
     {
         throw new NotImplementedException();
     }*/
}