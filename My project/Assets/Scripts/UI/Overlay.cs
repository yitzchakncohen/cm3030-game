using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private ClueScanner clueScanner;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Button doneButton;

    [SerializeField] private RectTransform crosshair;
    [SerializeField] private RectTransform scanClue;
    [SerializeField] private RectTransform interact;
    [SerializeField] private RectTransform dossier;
    [SerializeField] private RectTransform controls;

    private void Start()
    {
        doneButton.onClick.AddListener(OnDoneButtonClick);
    }

    private void OnDestroy()
    {
        doneButton.onClick.RemoveAllListeners();
    }

    private void Update()
    {
        CheckClueScanner();
        CheckForPickup();
    }

    private void CheckForPickup()
    {
        if (characterController.TargetPickup != null && characterController.GrabbedPickup == null)
        {
            ShowInteract(characterController.TargetPickup.transform);
        }
        else
        {
            HideInteract();
        }
    }

    private void CheckClueScanner()
    {
        if (clueScanner.TargetClue != null && !clueScanner.IsScanning)
        {
            ShowScanClue(clueScanner.TargetClue.transform);
        }
        else
        {
            HideScanClue();
        }
    }

    public void ShowCrosshair()
    {
        crosshair.gameObject.SetActive(true);
    }

    public void HideCrosshair()
    {
        crosshair.gameObject.SetActive(false);
    }

    public void ShowDossier()
    {
        dossier.gameObject.SetActive(true);
    }

    public void HideDossier()
    {
        dossier.gameObject.SetActive(false);
    }

    public void ShowScanClue(Transform target)
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(target.position);
        scanClue.position = screenPoint;
        if (!scanClue.gameObject.activeSelf)
        {
            scanClue.gameObject.SetActive(true);
        }
    }

    public void HideScanClue()
    {
        if (scanClue.gameObject.activeSelf)
        {
            scanClue.gameObject.SetActive(false);
        }
    }

    public void ShowInteract(Transform target)
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(target.position);
        interact.position = screenPoint;
        if (!interact.gameObject.activeSelf)
        {
            interact.gameObject.SetActive(true);
        }
    }

    public void HideInteract()
    {
        if (interact.gameObject.activeSelf)
        {
            interact.gameObject.SetActive(false);
        }
    }
    
    public void OnControlsButtonClick()
    {
        controls.gameObject.SetActive(true);
    }

    private void OnDoneButtonClick()
    {
        controls.gameObject.SetActive(false);
    }
}
