using UnityEngine;

public class ShutdownButton : MonoBehaviour
{
    public Animator panelAnimator;

    public void PlayExitStuff()
    {
        panelAnimator.Play("Login Screen Fade Out");

        AudioSource audioSource = GetComponent<AudioSource>();

        audioSource.Play();

        Invoke("QuitTheGame", 2);
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }
}