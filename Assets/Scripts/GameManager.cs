using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IDataController
{
    public static bool Pause;
    public GameObject PauseMenu;
    public TextMeshProUGUI VillagerCountUI;
    public GameObject GameMenu;
    public DataManager data;

    public int VillagerNum;
    public Transform VillagerStorage;

    public bool LevelComplete;
    public GameObject LevelCompleteMenu;

    int CurrentLevel;

    public void LoadData(GameData data)
    {
        CurrentLevel = data.CurrentLevel;
    }

    public void SaveData(ref GameData data)
    {
        data.CurrentLevel = SceneManager.GetActiveScene().buildIndex-1;
    }
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
        VillagerNum--;
        if (VillagerNum == 0)
        {
            LevelComplete = true;
            Pause = true;
            GameMenu.SetActive(false);
            LevelCompleteMenu.SetActive(true);
            data.SaveGame(DataManager.DataSlot);
        }
        else
            UpdateUI();
    }
    public void ExitLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void NextLevel(int LevelNum)
    {
        SceneManager.LoadScene(2 + LevelNum);
    }
    public void PauseGame()
    {
        Pause = !Pause;
    }
    public void UpdateUI()
    {
        VillagerCountUI.text = "Villagers Left to find: " + VillagerNum;
    }
}
