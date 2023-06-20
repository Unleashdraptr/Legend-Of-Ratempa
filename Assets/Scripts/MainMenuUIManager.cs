using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    public enum SlotState {NONE, CREATE, SAVE, LOAD, DELETE};
    public SlotState CurrentState;
    public DataManager data;


    private TextMeshProUGUI[] ButtonText;
    private Button[] Button;
    public GameObject SaveSlotsParent;

    public GameObject SaveSelection;
    public GameObject SaveSpecifics;
    public string GameWorldName;
    private int SaveCreationNum;
    public Button CreateSaveButton;
    void Start()
    {
        CreateSaveButton.interactable = false;
        CurrentState = SlotState.NONE;
        Button = SaveSlotsParent.transform.GetComponentsInChildren<Button>();
        ButtonText = SaveSlotsParent.transform.GetComponentsInChildren<TextMeshProUGUI>();
    }


    //Button controls
    public void CreateSlots()
    {
        CurrentState = SlotState.CREATE;
        UpdateUI();
    }
    public void SaveSlots()
    {
        CurrentState = SlotState.SAVE;
        UpdateUI();
    }
    public void LoadSlots()
    {
        CurrentState = SlotState.LOAD;
        UpdateUI();
    }
    public void DeleteSlots()
    {
        CurrentState = SlotState.DELETE;
        UpdateUI();
    }
    public void SlotInteract(int SlotNum)
    {
        switch(CurrentState)
        {
            case SlotState.NONE:
                return;
            case SlotState.CREATE:
                ChangeMenu(SlotNum);
                return;
            case SlotState.SAVE:
                if(SlotNum > 4)
                {
                    data.SaveGame(DataManager.DataSlot);
                    return;
                }
                data.SaveGame(SlotNum);
                return;
            case SlotState.LOAD:
                data.LoadGame(SlotNum);
                return;
            case SlotState.DELETE:
                data.DeleteGame(SlotNum);
                UpdateUI();
                return;
        }
    }   
    public void UpdateUI()
    {
        for (int i = 0; i < SaveSlotsParent.transform.childCount; i++)
        {
            if (CurrentState != SlotState.CREATE)
            {
                if (data.CheckSave(i + 1) != null)
                {
                    Button[i].interactable = true;
                    ButtonText[i].text = data.CheckSave(i + 1);
                }
                else
                {
                    Button[i].interactable = false;
                    ButtonText[i].text = "Empty";
                }
            }
            else
            {
                if (data.CheckSave(i + 1) == null)
                {
                    Button[i].interactable = true;
                    ButtonText[i].text = "Empty";
                }
                else
                {
                    Button[i].interactable = false;
                    ButtonText[i].text = "Taken";
                }
            }
        }
    }    
    public void ChangeMenu(int SlotNum)
    {
        SaveCreationNum = SlotNum;
        SaveSelection.SetActive(false);
        SaveSpecifics.SetActive(true);
    }
    public void SaveWorldName(TextMeshProUGUI WorldName)
    {
        GameWorldName = WorldName.text;
        CreateSaveButton.interactable = true;
    }
    public void CreateGameSave()
    {
        data.CreateGame(SaveCreationNum, GameWorldName);
        SceneManager.LoadScene(0);
    }
}
