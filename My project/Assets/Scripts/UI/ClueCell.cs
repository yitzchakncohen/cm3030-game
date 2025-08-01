using UnityEngine;

public class ClueCell : MonoBehaviour
{
    private ClueScriptableObject clue;
    
    public void Init(ClueScriptableObject clue)
    {
        this.clue = clue;
    }
}
