using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip entering_course;
    public AudioClip entering_the_crown_node;
    public AudioSource mapBackground;
    

    public void  PlayEnteringSound()
    {
        audioSource.PlayOneShot(entering_course);
    }


    public void PlayEnteringCrownNodeSound()
    {
        audioSource.PlayOneShot(entering_the_crown_node);
    }
   
}
