using TMPro;
using UnityEngine;

public class ClueUI : MonoBehaviour
{
    [SerializeField] private ClueScanner clueScanner;
    [SerializeField] private TMP_Text cluesScannedText;
    [SerializeField] private int totalClues = 3;
    private int cluesScanned = 0;

    private void Start()
    {
        clueScanner.OnClueScanned += ClueScanner_OnClueScanned;
        cluesScannedText.text = $"Clues {cluesScanned}/{totalClues}";
    }

    private void OnDestroy()
    {
        clueScanner.OnClueScanned -= ClueScanner_OnClueScanned;
    }

    private void ClueScanner_OnClueScanned(Clue clue)
    {
        cluesScanned++;
        cluesScannedText.text = $"Clues {cluesScanned}/{totalClues}";
    }
}
