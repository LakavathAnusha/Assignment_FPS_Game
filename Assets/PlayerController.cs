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
    float ammo=0f;
    float maxAmmo=25f;
   public  float health=10f;
    public float MaxHealth=20f;
    public new AudioClip audio;
 
    //SpawnManager spawnManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        colliders = GetComponent<CapsuleCollider>();
        audio = GetComponent<AudioClip>();
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
            ammo--;
        }
    }

    private void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal") * playerSpeed;
        float inputz = Input.GetAxis("Vertical") * playerSpeed;
        transform.position += new Vector3(inputX, 0f, inputz);
        audioSource.Play();

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
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Ammo"&&ammo<maxAmmo)
        {
            Debug.Log("ammo is collected");
            ammo = Mathf.Clamp(ammo + 25, 0, maxAmmo);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Medical" && health < MaxHealth)
        {
            Debug.Log("health is collected");
            ammo = Mathf.Clamp(ammo + 10, 0, maxAmmo);
            Destroy(other.gameObject);
        }

    }
}