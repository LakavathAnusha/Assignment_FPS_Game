using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    public List<AudioClip> audioClips; 
    void Start()
    {
        audioSource = GetComponent<AudioSource>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShotFire()
    {
        audioSource.PlayOneShot(audioClips[0]);
    }

    public void PlayerJump()
    {
        audioSource.PlayOneShot(audioClips[1]);
    }

    public void PlayerLand()
    {
        audioSource.PlayOneShot(audioClips[2]);
    }

    public void PlayerWalkOne()
    {
        audioSource.PlayOneShot(audioClips[3]);
    }
    public void PlayerWalkTwo()
    {
        audioSource.PlayOneShot(audioClips[4]);
    }
    public void PlayerWalkThree()
    {
        audioSource.PlayOneShot(audioClips[5]);
    }
    public void PlayerWalkFour()
    {
        audioSource.PlayOneShot(audioClips[6]);
    }


}
