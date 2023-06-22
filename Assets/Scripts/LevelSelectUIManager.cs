using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectUIManager : MonoBehaviour, IDataController
{
    public DataManager data;
    public GameObject OptionsMenu;
    public GameObject SettingsMenu;
    bool InSettings;

    public Transform LevelStorage;
    private Button[] Levels;
    int CurLevel;

    public void LoadData(GameData data)
    {
        CurLevel = data.CurrentLevel;
    }

    public void SaveData(ref GameData data)
    {
        data.CurrentLevel = CurLevel;
    }
    // Start is called before the first frame update
    void Start()
    {
        Levels = LevelStorage.GetComponentsInChildren<Button>();
        UpdateLevels();
        data.LoadGame(DataManager.DataSlot);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && InSettings == false)
        {
            OptionsMenu.SetActive(!OptionsMenu.activeSelf);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && InSettings)
        {
            SettingsMenu.SetActive(false);
            InSettings = false;
            OptionsMenu.SetActive(true);
        }
    }
    private void UpdateLevels()
    {
        for(int i = 0; i< CurLevel; i++)
        {
            Levels[i].interactable = true;
        }
    }
    public void SaveGame()
    {
        data.SaveGame(DataManager.DataSlot);
    }
    public void SettingMenu()
    {
        OptionsMenu.SetActive(false);
        SettingsMenu.SetActive(true);
        InSettings = true;
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadLevel(int Level)
    {
        SceneManager.LoadScene(1 + Level);
    }
}
