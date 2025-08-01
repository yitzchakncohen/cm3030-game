using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Dossier : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;

    [SerializeField] private Button nextSuspectButton;

    [SerializeField] private GameObject dossier;
    [SerializeField] private Transform clueHolder;
    [SerializeField] private ClueCell cluePrefab;
    [SerializeField] private SuspectCell suspectCell;
    private List<SuspectScriptableObject> suspects;
    private int currentSuspect = 0;

    private void Awake()
    {
        dossier.SetActive(false);
        inputManager.OnShowNotesInput += InputManager_OnShowNotesInput;
        nextSuspectButton.onClick.AddListener(OnNextSuspect);
    }

    private void OnDestroy()
    {
        inputManager.OnShowNotesInput -= InputManager_OnShowNotesInput;
        nextSuspectButton.onClick.RemoveAllListeners();
    }

    public void Init(List<SuspectScriptableObject> suspectsList)
    {
        suspects = suspectsList;
        suspectCell.Init(suspects[currentSuspect]);
        foreach (Transform child in clueHolder)
        {
            Destroy(child.gameObject);
        }
    }

    public void AddClue(ClueScriptableObject clue)
    {
        ClueCell clueCell = Instantiate(cluePrefab, clueHolder);
        clueCell.Init(clue);
    }

    private void OnNextSuspect()
    {
        currentSuspect = (currentSuspect + 1) % suspects.Count;
        suspectCell.Init(suspects[currentSuspect]);
    }

    private void InputManager_OnShowNotesInput()
    {
        ToggleDossier();
    }

    private void ToggleDossier()
    {
        dossier.SetActive(!dossier.activeSelf);
    }
}
