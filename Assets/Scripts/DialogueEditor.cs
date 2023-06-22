using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a class used to store data about the dialogue currently displayed
[System.Serializable]
public class Dialogue
{
    public string dialogue;
    public bool IsPlayerText;
    public bool IsEnd;
    public Texture PlayerLookLike;
}

//This is where i can then store the dialogue and change the options
public class DialogueEditor : MonoBehaviour
{
    public Dialogue[] dialogue;
}


