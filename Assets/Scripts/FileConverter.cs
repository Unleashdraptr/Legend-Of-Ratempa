using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileConverter
{
    //The 2 strings that make up the file name
    private readonly string dataDirPath = "";
    private readonly string dataSaveFolder = "";
    private readonly string dataSaveName = "";
    //Changes it to the current one
    public FileConverter(string dataDirPath, string dataFileName, string dataSaveName)
    {
        this.dataDirPath = dataDirPath;
        this.dataSaveFolder = dataFileName;
        this.dataSaveName = dataSaveName;
    }

    public void CreateFiles()
    {
        GameData data = null;
        for (int i = 1; i < 5; i++)
        {
            string fullPath = Path.Combine(dataDirPath, dataSaveFolder, dataSaveName + i.ToString());
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToConvert = JsonUtility.ToJson(data, true);
            using (FileStream stream = new(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new(stream))
                {
                    writer.Write(dataToConvert);
                }
            }
        }
    }
    //When loading the game it will check if there is a save file, if so, it will then get the data from said file.
    //If not, just create a new game
    public GameData Load(int FilePointer)
    {
        string fullPath = Path.Combine(dataDirPath, dataSaveFolder, dataSaveName + FilePointer.ToString());
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToConvert = "";
                using (FileStream stream = new(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new(stream))
                    {
                        dataToConvert = reader.ReadToEnd();
                    }
                }
                loadedData = JsonUtility.FromJson<GameData>(dataToConvert);
            }
            catch (Exception e)
            {
                Debug.LogError("Couldnt laod file" + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }
    //Once pressing the save button or quitting it will either create or find the file and then convert the relivent data into the correct file format and stores it
    public void Save(GameData data, int FilePointer)
    {
        string fullPath = Path.Combine(dataDirPath, dataSaveFolder, dataSaveName + FilePointer.ToString());
        string dataToConvert = JsonUtility.ToJson(data, true);
        using (FileStream stream = new(fullPath, FileMode.Create))
        {
            using (StreamWriter writer = new(stream))
            {
                writer.Write(dataToConvert);
            }
        }
    }
    public void Delete(int FilePointer)
    {
        string fullPath = Path.Combine(dataDirPath, dataSaveFolder, dataSaveName + FilePointer.ToString());
        GameData data = null;
        string dataToConvert = JsonUtility.ToJson(data, true);
        using (FileStream stream = new(fullPath, FileMode.Create))
        {
            using (StreamWriter writer = new(stream))
            {
                writer.Write(dataToConvert);
            }
        }
    }

    public void SaveNames(SaveNames saveNames)
    {
        string fullPath = Path.Combine(dataDirPath, dataSaveFolder, dataSaveName);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
        string dataToConvert = JsonUtility.ToJson(saveNames, true);
        using (FileStream stream = new(fullPath, FileMode.Create))
        {
            using (StreamWriter writer = new(stream))
            {
                writer.Write(dataToConvert);
            }
        }
    }
    public SaveNames LoadSaveNames()
    {
        string fullPath = Path.Combine(dataDirPath, dataSaveFolder, dataSaveName);
        SaveNames loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToConvert = "";
                using (FileStream stream = new(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new(stream))
                    {
                        dataToConvert = reader.ReadToEnd();
                    }
                }
                loadedData = JsonUtility.FromJson<SaveNames>(dataToConvert);
            }
            catch (Exception e)
            {
                Debug.LogError("Couldnt laod file" + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }
}