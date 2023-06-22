using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics.Tracing;

//This is where the Dialogue options are used
public class DialogueManager : MonoBehaviour
{
    int DialogueIterNum;
    GameObject dialogueEditor;
    bool EndofText;
    bool NotInDialogue = true;

    public bool IsVillager = false;

    public GameObject DialogueUI;
    public GameObject PlayerTextBox;
    public GameObject NPCTextBox;
    public Transform TextStorage;
    public GameObject StartingText;
    public GameObject NextButtonObject;
    public GameObject EndButtonObject;
    public GameObject GameMenuUI;

    public void Start()
    {
        if(StartingText != null)
        {
            StartDialogue(StartingText);
        }
    }
    public void NextButton()
    {
        if (!EndofText)
        {
            DialogueIterNum += 1;
            UpdateDialogue();
        }
    }
    public void EndButton()
    {
        for (int i = 0; i < TextStorage.childCount; i++)
        {
            Destroy(TextStorage.GetChild(i).gameObject);
        }
        GameManager.Pause = false;
        DialogueUI.SetActive(false);
        NotInDialogue = true;
        if(IsVillager)
        {
            Destroy(dialogueEditor);
            transform.GetComponent<GameManager>().CheckVillagers();
        }
        GameMenuUI.SetActive(true);
    }
    public void StartDialogue(GameObject dia)
    {
        if (NotInDialogue)
        {
            GameMenuUI.SetActive(false);
            NextButtonObject.SetActive(true);
            EndButtonObject.SetActive(false);
            GameManager.Pause = true;
            DialogueUI.SetActive(true);
            //Gives it the dialogue it is going to use
            dialogueEditor = dia;
            DialogueIterNum = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            UpdateDialogue();
            NotInDialogue = false;
            EndofText = false;
        }
    }
    public void UpdateDialogue()
    {
        Dialogue diag = dialogueEditor.GetComponent<DialogueEditor>().dialogue[DialogueIterNum];

        StartCoroutine(MoveDialogueBoxes());
        GameObject Text;
        if (diag.IsPlayerText)
        {
            Text = Instantiate(PlayerTextBox, TextStorage);
        }
        else
            Text = Instantiate(NPCTextBox, TextStorage);
        Text.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = diag.dialogue;
        Text.transform.GetChild(0).GetChild(0).GetComponent<RawImage>().texture = diag.PlayerLookLike;
        if (diag.IsEnd)
        {
            EndofText = true;
            NextButtonObject.SetActive(false);
            EndButtonObject.SetActive(true);
}
    }
    IEnumerator MoveDialogueBoxes()
    {
        for (int i = 0; i < TextStorage.childCount; i++)
        {
            Animator TextBox = TextStorage.GetChild(i).GetComponent<Animator>();
            TextBox.SetInteger("TextLevel", TextStorage.childCount - i);
        }
        yield return new WaitForSeconds(1f);
        if (TextStorage.childCount > 4)
        {
            Destroy(TextStorage.GetChild(0).gameObject);
        }
    }
}
