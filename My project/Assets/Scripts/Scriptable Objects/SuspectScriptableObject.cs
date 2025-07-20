using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "SuspectScriptableObject", menuName = "Scriptable Objects/SuspectScriptableObject")]
public class SuspectScriptableObject : ScriptableObject
{
    public string suspectName;
    public List<ClueScriptableObject> clues;
    public GameObject model;

    public ClueScriptableObject pickRandomClue()
    {
        int x = Random.Range(0, clues.Count());
        // (“Scriptable Objects change thier values during run time and these persist - Unity Engine,” 2024. https://discussions.unity.com/t/scriptable-objects-change-thier-values-during-run-time-and-these-persist/1507422) 
        ClueScriptableObject clue = Instantiate(clues[x]);
        clues.RemoveAt(x);
        return clue;
    }

    public List<ClueScriptableObject> pickRandomClues(int numOfClues)
    {
        Debug.Log("Selecting " + numOfClues.ToString() + " clues.");
        List<ClueScriptableObject> clues = new List<ClueScriptableObject>();

        for (int i = 0; i < numOfClues; i++)
        {
            if (this.clues.Count() == 0)
            {
                break;
            }
            clues.Add(pickRandomClue());
    
        }
        return clues;
    }

}
