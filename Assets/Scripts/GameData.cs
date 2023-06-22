using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int CurrentLevel;
    //Add variables here to store all the data needed.
    //(Cant be too complicated e.g., GameObjects)
}

[System.Serializable]
public class SaveNames
{
    public string[] SaveSlotName = new string[4];
    public float[] HrsPlayed = new float[4];
}
