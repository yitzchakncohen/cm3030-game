using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private ClueScanner clueScanner;
    [SerializeField] private Dossier dossier;

    [SerializeField] private Canvas startMenuCanvas;

    private void Start()
    {
        clueScanner.OnClueScanned += ClueScanner_OnClueScanned;
    }

    private void OnDestroy()
    {
        clueScanner.OnClueScanned -= ClueScanner_OnClueScanned;
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

        startMenuCanvas.GetComponent<Animator>().SetTrigger("GameStarted");
        
    }
}
