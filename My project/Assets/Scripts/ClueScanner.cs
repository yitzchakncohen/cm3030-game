using System;
using UnityEngine;

public class ClueScanner : MonoBehaviour
{
    public event Action<Clue> OnClueScanned;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private LayerMask pickupsLayer;
    [SerializeField] private GameObject magnifyingGlass;
    [SerializeField] private float clueScanTime = 2f;
    [SerializeField] private float scanDistance = 2f;
    private Clue targetClue = null;
    private float timer = 0f;
    private bool isScanning = false;

    private void OnEnable()
    {
        inputManager.OnScanInputDown += InputManager_OnScanInputDown;
        inputManager.OnScanInputUp += InputManager_OnScanInputUp;
        magnifyingGlass.SetActive(false);
    }

    private void OnDisable()
    {
        inputManager.OnScanInputDown -= InputManager_OnScanInputDown;
        inputManager.OnScanInputUp -= InputManager_OnScanInputUp;
    }

    private void Update()
    {
        CheckForClue();

        if (!isScanning || targetClue == null) return;

        timer += Time.deltaTime;
        magnifyingGlass.SetActive(true);
        float completePercentage = timer / clueScanTime;
        targetClue.UpdateScan(completePercentage);

        if (timer > clueScanTime)
        {
            magnifyingGlass.SetActive(false);
            targetClue.SetScanned();
            OnClueScanned?.Invoke(targetClue);
            targetClue = null;
        }
    }

    private void InputManager_OnScanInputDown()
    {
        isScanning = true;
        timer = 0f;
    }

    private void InputManager_OnScanInputUp()
    {
        isScanning = false;
    }

    private void CheckForClue()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * scanDistance, Color.red);
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, scanDistance, pickupsLayer))
        {
            if (hit.transform.TryGetComponent<Clue>(out Clue clue))
            {
                if (clue != targetClue && !clue.IsScanned)
                {
                    targetClue?.Reset();
                    targetClue = clue;
                    targetClue.Target();
                }
            }
        }
        else
        {
            if (targetClue != null && !targetClue.IsScanned)
            {
                targetClue.Reset();
            }
            targetClue = null;
            magnifyingGlass.SetActive(false);
        }
    }
}
