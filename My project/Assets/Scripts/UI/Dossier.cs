using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dossier : MonoBehaviour
{
    public event Action OnOpen;
    public event Action OnClose;
    public event Action<SuspectScriptableObject> OnAccuseSuspect;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Overlay overlay;

    [SerializeField] private Button nextSuspectButton;
    [SerializeField] private Button accuseSuspectButton;

    [SerializeField] private GameObject dossier;
    [SerializeField] private Transform clueHolder;
    [SerializeField] private ClueCell cluePrefab;
    [SerializeField] private SuspectCell suspectCell;
    [SerializeField] private TMP_Text pageNumber;
    [SerializeField] private TMP_Text clueCounter;
    private List<SuspectScriptableObject> suspects;
    private int currentSuspect = 0;

    private void Awake()
    {
        dossier.SetActive(false);
        inputManager.OnShowNotesInput += InputManager_OnShowNotesInput;
        nextSuspectButton.onClick.AddListener(OnNextSuspectClick);
        accuseSuspectButton.onClick.AddListener(OnAccuseSuspectClick);
    }

    private void OnDestroy()
    {
        inputManager.OnShowNotesInput -= InputManager_OnShowNotesInput;
        nextSuspectButton.onClick.RemoveAllListeners();
        accuseSuspectButton.onClick.RemoveAllListeners();
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
        overlay.ShowDossierPip();
        clueCounter.text = $"{clueHolder.childCount}/6 Clues Found";
    }

    private void OnNextSuspectClick()
    {
        currentSuspect = (currentSuspect + 1) % suspects.Count;
        suspectCell.Init(suspects[currentSuspect]);
        pageNumber.text = $"{currentSuspect+1}/{suspects.Count}";
    }

    private void OnAccuseSuspectClick()
    {
        OnAccuseSuspect?.Invoke(suspects[currentSuspect]);
    }

    private void InputManager_OnShowNotesInput()
    {
        ToggleDossier();
    }

    private void ToggleDossier()
    {
        dossier.SetActive(!dossier.activeSelf);
        if (dossier.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            overlay.HideCrosshair();
            OnOpen?.Invoke();
            overlay.HideDossierPip();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            overlay.ShowCrosshair();
            OnClose?.Invoke();
        }
    }
}
