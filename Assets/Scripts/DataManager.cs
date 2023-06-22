using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    //Gives the player the option
    public bool EnableAutoSave;
    //How frequently to save the game
    public int AutoSaveFrequency;
    //The countdown to the next save
    public float AutoSaveTimer;

    //What Save the game is currently on
    public static int DataSlot;
    //Where the files are stored and what they are named
    [SerializeField] private string SaveFileStorageName;
    [SerializeField] private int SaveFileNumPrefix;
    //This is for the OtherSaveData files
    [SerializeField] private string NameSaveFileName;
    //The Data and SaveNames stored for each run time save currently being played through
    private GameData gameData;
    private SaveNames SaveNames;
    //How it then saves the storage and how to convert it
    public static DataManager Instance { get; private set; }
    private FileConverter SaveNameStorage;
    private FileConverter dataHandler;
    private List<IDataController> dataHandlerobjects;

    //Checks if there is another one of the managers and removes it if so
    public void Awake()
    {
        Instance = this;
    }
    //Gets all the objects it will need to write data to at the beginning and potential creates the files if it needs to
    private void Start()
    {
        SaveNameStorage = new FileConverter(Application.persistentDataPath, "", NameSaveFileName);
        dataHandler = new FileConverter(Application.persistentDataPath, SaveFileStorageName, SaveFileNumPrefix.ToString());
        dataHandlerobjects = FindAllDataPoints();
        string fullPath = Path.Combine(Application.persistentDataPath, SaveFileStorageName, 0.ToString() + 1.ToString());
        if (!File.Exists(fullPath))
        {
            dataHandler.CreateFiles();
        }
        TryLoadSaveNames();
        LoadGame(DataSlot);
    }
    //This is if there is autosave enabled by the player
    private void Update()
    {
        if (EnableAutoSave)
        {
            AutoSaveTimer -= 1 * Time.deltaTime;
            if (AutoSaveTimer <= 0)
            {
                SaveGame(DataSlot);
                AutoSaveTimer = AutoSaveFrequency;
            }
        }
    }
    //Used by other scripts to ensure other data isnt overwritten
    public string CheckSave(int SaveNum)
    {
        if (SaveNames.SaveSlotName[SaveNum - 1] != "")
        {
            return SaveNames.SaveSlotName[SaveNum - 1];
        }
        return null;
    }


    //Game Data managment
    //Reset the game file if needed and will Update the saveNames (This creates the game)
    public void CreateGame(int FileNum, string NewSaveName)
    {
        DataSlot = FileNum;
        gameData = new GameData
        {
            CurrentLevel = 1
        };
        SaveNames.HrsPlayed[DataSlot - 1] = 0;
        SaveNames.SaveSlotName[DataSlot - 1] = NewSaveName;
        SaveNameStorage.SaveNames(SaveNames);
        SaveGame(FileNum);
    }
    //Gets all the relevent data from scripts and will store it within an external file
    public void SaveGame(int FileNum)
    {
        foreach (IDataController dataHandlerObj in dataHandlerobjects)
        {
            dataHandlerObj.SaveData(ref gameData);
        }
        dataHandler.Save(gameData, FileNum);
    }
    //Gets the data from the file and applies it to the relevent data
    public void LoadGame(int FileNum)
    {
        DataSlot = FileNum;
        gameData = dataHandler.Load(FileNum);
        if (gameData == null)
        {
            return;
        }
        foreach (IDataController dataHandlerObj in dataHandlerobjects)
        {
            dataHandlerObj.LoadData(gameData);
        }
    }
    //Resets the game file and removes the name from the list (Essentially deleting it)
    public void DeleteGame(int FileNum)
    {
        dataHandler.Delete(FileNum);
        SaveNames.SaveSlotName[FileNum-1] = "";
        SaveNameStorage.SaveNames(SaveNames);
    }


    //Used to check if its the first time the game has loaded and will create the neccesary files needed
    public void TryLoadSaveNames()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, NameSaveFileName);
        if (!File.Exists(fullPath))
        {
            SaveNames = new SaveNames();
            SaveNameStorage.SaveNames(SaveNames);
        }
        else
            SaveNames = SaveNameStorage.LoadSaveNames();
    }


    //Gathers all the DataPoints in other scripts
    private List<IDataController> FindAllDataPoints()
    {
        IEnumerable<IDataController> dataHandlers = FindObjectsOfType<MonoBehaviour>().OfType<IDataController>();
        return new List<IDataController>(dataHandlers);
    }
}