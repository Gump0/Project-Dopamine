using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioClip soundeffect;
    public AudioSource audioSource;
 

    public void playClip()
    {
        audioSource.clip = soundeffect;
        audioSource.PlayOneShot(soundeffect);
    }
}