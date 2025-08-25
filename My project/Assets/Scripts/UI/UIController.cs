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
    }


    public void GameStarted()
    {

        startMenu.GetComponent<Animator>().SetTrigger("GameStarted");

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
