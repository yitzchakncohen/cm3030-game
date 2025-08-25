using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private ClueScanner clueScanner;
    [SerializeField] private Dossier dossier;

    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject suspectSelectMenu;
    [SerializeField] private GameObject clueHolder;
    [SerializeField] private GameObject clueText;
    [SerializeField] private GameObject suspectHolder;
    [SerializeField] private GameObject suspectButton;
    [SerializeField] private GameObject gameControllerObject;
    [SerializeField] private GameObject scoringObject;
    private GameController gameController;

    [SerializeField] private GameObject eventSystem;
    [SerializeField] private GameObject endGameMenu;

    private List<String> results;

    private void Start()
    {
        clueScanner.OnClueScanned += ClueScanner_OnClueScanned;
        gameController = gameControllerObject.GetComponent<GameController>();
        gameController.OnAccuseSuspect += GameController_OnAccuseSuspect;
    }

    private void OnDestroy()
    {
        clueScanner.OnClueScanned -= ClueScanner_OnClueScanned;
        gameController.OnAccuseSuspect -= GameController_OnAccuseSuspect;
    }

    public void InitializeDossier(List<SuspectScriptableObject> suspects)
    {
        dossier.Init(suspects);
    }

    private void ClueScanner_OnClueScanned(Clue clue)
    {
        dossier.AddClue(clue.ClueScriptableObject);
        GameObject clueObject = Instantiate(clueText, clueHolder.transform);
        clueObject.name = clue.gameObject.transform.parent.name;
        clueObject.GetComponent<TextMeshProUGUI>().text = clue.gameObject.transform.parent.name;

    }


    public void GameStarted()
    {

        startMenu.GetComponent<Animator>().SetTrigger("GameStarted");

    }

    public void GameEnded()
    {
        Debug.Log("uicontroller game ended");
        suspectSelectMenu.SetActive(true);
        foreach (SuspectScriptableObject suspect in gameController.allSuspects)
        {
            GameObject newButton = Instantiate(suspectButton, suspectHolder.transform);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = suspect.suspectName;
            newButton.name = suspect.suspectName;

            // learnt how to do this from Unity documentation: https://docs.unity3d.com/2018.3/Documentation/ScriptReference/UI.Button-onClick.html
            newButton.GetComponent<Button>().onClick.AddListener(() => SuspectSelected(suspect));
        }

        suspectHolder.transform.GetChild(1).gameObject.GetComponent<Button>().Select();
    }
    
    private void GameController_OnAccuseSuspect(SuspectScriptableObject suspect)
    {
        SuspectSelected(suspect);
    }

    private void SuspectSelected(SuspectScriptableObject suspect)
    {
        results = gameController.OnSuspectSelect(suspect.suspectName);
        for(var i = 0; i < results.Count;i++)
        {
            Debug.Log(results[i]);
        }
        endGameMenu.SetActive(true);

        endGameMenu.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = results[0];
        endGameMenu.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = results[1];

        
        for (int i = 0; i < scoringObject.transform.childCount; i++)
        {
            scoringObject.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = results[i + 2];
        }

    }
    
    
}
