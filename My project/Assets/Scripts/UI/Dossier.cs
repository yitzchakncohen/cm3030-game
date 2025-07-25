using UnityEngine;

public class Dossier : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject dossier;

    private void Awake()
    {
        dossier.SetActive(false);
        inputManager.OnShowNotesInput += InputManager_OnShowNotesInput;
    }

    private void OnDestroy()
    {
        inputManager.OnShowNotesInput -= InputManager_OnShowNotesInput;
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
