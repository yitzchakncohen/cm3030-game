using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class ClueSpawner : MonoBehaviour
{
    List<ClueScriptableObject> activeClues;

    public TextAsset spawnPointsFile;

    public List<Vector3> spawnPoints;

    public List<SuspectScriptableObject> suspects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        SuspectScriptableObject suspect = PickRandomSuspect();

        int redHerrings = Random.Range(1, 4);
        int numOfClues = Random.Range(3, 5);

        activeClues = suspect.pickRandomClues(numOfClues);


        //TODO - add the red herrings code.
        Debug.Log("Selecting" + redHerrings.ToString() + " red herrings.");
        for (var i = 0; i < redHerrings; i++)
        {
            if(suspects.Count() ==0){
                Debug.Log("No suspects remaining to select red herrings from.");
                break;
            }
            suspect = PickRandomSuspect();
            activeClues.Add(suspect.pickRandomClue());
        }

        foreach (ClueScriptableObject clue in activeClues)
        {
            PlaceClue(clue);
        }

        

        // DebugSpawnPoints(suspect.suspectClues[0]);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void DebugSpawnPoints(ClueScriptableObject clue)
    {
        foreach (Vector3 spawnPoint in spawnPoints)
        {
            GameObject currentClue = Instantiate(clue.model, spawnPoint, Quaternion.identity);
            currentClue.name = clue.clueName;
        }
    }

    void CreateSpawnPointsFromCSV()
    {
        // unused.
        spawnPoints = new List<Vector3>();
        string[] lines = spawnPointsFile.text.Split("\n");
        foreach (string line in lines)
        {
            string[] words = line.Split(",");
            Debug.Log(line);
            Vector3 vector = new Vector3(float.Parse(words[0]), float.Parse(words[1]), float.Parse(words[2]));
            spawnPoints.Add(vector);
        }
    }

    SuspectScriptableObject PickRandomSuspect()
    {
        int x = Random.Range(0, suspects.Count());
        SuspectScriptableObject suspect = Instantiate(suspects[x]);
        // (“Scriptable Objects change thier values during run time and these persist - Unity Engine,” 2024. https://discussions.unity.com/t/scriptable-objects-change-thier-values-during-run-time-and-these-persist/1507422) 
        //remove them from the list so we can pick clues from other suspects
        suspects.RemoveAt(x);
        Debug.Log("Selected suspect: " + suspect.suspectName);

        return suspect;
    }
    void PlaceClue(ClueScriptableObject clue)
    {
        int i = Random.Range(0, spawnPoints.Count());
        GameObject currentClue = Instantiate(clue.model, spawnPoints[i], Quaternion.identity);
        currentClue.name = clue.clueName;
        spawnPoints.RemoveAt(i);
    }
}
