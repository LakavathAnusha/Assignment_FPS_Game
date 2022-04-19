using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float playerSpeed;
    
    public float playerRotationSpeed;
    //public Transform bulletLaunch;
    //public GameObject steveModelPrefabs;
    Rigidbody rb;
    public Animator animator;
    CapsuleCollider colliders;
    Quaternion camRotation;
    public Camera cam;
    Quaternion playerRotation;

    public AudioSource audioSource;
    //SpawnManager spawnManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        colliders = GetComponent<CapsuleCollider>();
        audioSource = GetComponent<AudioSource>();

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
        audioSource.Play();
        float inputz = Input.GetAxis("Vertical") * playerSpeed;
        audioSource.Play();

        transform.position += new Vector3(inputX, 0f, inputz);
        
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        audioSource.Play();
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