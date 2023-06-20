using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool Pause;
    public GameObject PauseMenu;
    public TextMeshProUGUI VillagerCountUI;
    public GameObject GameMenu;
    public DataManager data;


    public GameObject SettingsMenu;

    public int VillagerNum;
    public Transform VillagerStorage;

    public bool LevelComplete;
    public GameObject LevelCompleteMenu;

    // Start is called before the first frame update
    void Start()
    {
        VillagerNum = VillagerStorage.childCount;
        LevelComplete = false;
        LevelCompleteMenu.SetActive(false);
        Pause = false;
        UpdateUI();
    }

    public void CheckVillagers()
    {
        if (VillagerNum == 0 && VillagerStorage.childCount == 0)
        {
            LevelComplete = true;
            Pause = true;
            LevelCompleteMenu.SetActive(true);
        }
        UpdateUI();
    }
    public void SaveGame()
    {
        data.SaveGame(DataManager.DataSlot);
    }
    public void QuitToMenu()
    {

    }
    public void ExitLevel()
    {

    }
    public void PauseGame()
    {
        Pause = !Pause;
    }
    void UpdateUI()
    {
        VillagerCountUI.text = "Villagers Left to find: " + VillagerNum;
    }
}
