using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Text.RegularExpressions;

public class PasswordInputField : MonoBehaviour
{
    [SerializeField] private TMP_InputField passwordPrompt;

    public void GrabFromInputField() {
        string input = passwordPrompt.text;
        input = Regex.Replace(input, @"\s+", String.Empty);

        if (!String.IsNullOrEmpty(input)) {
            DisplayReactiontoPassword();
        } else {
            Debug.LogWarning("PASSWORD INPUT CANNOT BE EMPTY");
        }
    }

    public void DisplayReactiontoPassword() {
        Debug.Log("Welcome!");

        SceneManager.LoadScene("Desktop");
    } 
}