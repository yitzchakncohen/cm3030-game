using System;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private ClueScanner clueScanner;
    [SerializeField] private Dossier dossier;

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
}
