using UnityEngine;
using UnityEngine.SceneManagement;

public class PasswordInputField : MonoBehaviour
{
    private string password; 

    public void GrabFromInputField (string input)
    {
        password = input;

        DisplayReactiontoPassword();
    }

    public void DisplayReactiontoPassword()
    {
        Debug.Log("Welcome!");

        SceneManager.LoadScene("Desktop");
    } 
}