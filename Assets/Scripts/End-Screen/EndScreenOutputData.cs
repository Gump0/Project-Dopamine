// Class that reads save data and outputs
// a final ending response based off JSON data

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScreenOutputData : MonoBehaviour
{
    [Header("MANAGES OUTPUTTED DATA")]
    public TextData[] dialogueData; // stores every possible end message
    public int index;               // indexes array data
    [Header("DISPLAYS DATA")]
    public Image snakeCheck, essayCheck, clickerCheck;
    public Image snakeIcon, essayIcon, clickerIcon;
    public TMP_Text outputBox;

    void Start() {

    }
}
