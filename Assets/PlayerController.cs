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
    public Transform bulletDirection;
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

            Transform bulletpoint = GetComponentInChildren<Transform>();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HitEnemy();
        }
    }

    private void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal") * playerSpeed;
        float inputz = Input.GetAxis("Vertical") * playerSpeed;
        transform.position += new Vector3(inputX, 0f, inputz);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.rotation = transform.rotation * Quaternion.Euler(0, mouseX * playerRotationSpeed, 0);
        Camera cam = GetComponentInChildren<Camera>();
        cam.transform.localRotation = Quaternion.Euler(-mouseY, 0, 0) * cam.transform.localRotation;


    }

    public void HitEnemy()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(bulletDirection.position, bulletDirection.forward, out hitInfo, 1000f))
        {
            GameObject hitEnemy = hitInfo.collider.gameObject;
            if (hitEnemy.tag == "Cannibal")
            {
                Destroy(hitEnemy);

            }
        }
    }
}