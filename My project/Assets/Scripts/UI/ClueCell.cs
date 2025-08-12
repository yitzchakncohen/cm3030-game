using TMPro;
using UnityEngine;

public class ClueCell : MonoBehaviour
{
    private ClueScriptableObject clue;
    [SerializeField] private TMP_Text clueName;
    [SerializeField] private TMP_Text clueDescription;

    public void Init(ClueScriptableObject clue)
    {
        this.clue = clue;
        clueDescription.text = clue.description;
        clueName.text = clue.clueName;
    }
}
