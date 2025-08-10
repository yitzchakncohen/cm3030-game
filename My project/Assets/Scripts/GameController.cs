using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    [SerializeField] private UIController uIController;


    List<ClueScriptableObject> cluesToFind;

    // public TextAsset spawnPointsFile;
    [SerializeField] private GameObject zones;

    public List<Vector3> spawnPoints;

    public List<SuspectScriptableObject> allSuspects;
    List<SuspectScriptableObject> activeSuspects;
    private SuspectScriptableObject murderer;

    private List<ClueScriptableObject> foundClues;

    [SerializeField] private GameObject player;

    private int totalClues;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Pick a suspect.
        activeSuspects = new List<SuspectScriptableObject>(allSuspects);
        SuspectScriptableObject suspect = PickRandomSuspect();
        murderer = Instantiate(suspect);

        InputManager inputManager = player.GetComponent<InputManager>();
        inputManager.OnSuspectSelectMenuInput += uIController.GameEnded;

        //
        ClueScanner clueScanner = player.GetComponent<ClueScanner>();
        clueScanner.OnClueScanned += OnClueScanned;
        foundClues = new List<ClueScriptableObject>();

        // Debug.Log(activeSuspects.Count());


        int redHerrings = UnityEngine.Random.Range(1, 4);
        int numOfClues = UnityEngine.Random.Range(3, 5);
        totalClues = redHerrings + numOfClues;

        cluesToFind = suspect.pickRandomClues(numOfClues);


        //TODO - add the red herrings code.
        // Debug.Log("Selecting" + redHerrings.ToString() + " red herrings.");
        for (var i = 0; i < redHerrings; i++)
        {
            if (activeSuspects.Count() == 0)
            {
                Debug.Log("No suspects remaining to select red herrings from.");
                break;
            }
            suspect = PickRandomSuspect();
            cluesToFind.Add(suspect.pickRandomClue());
        }

        foreach (ClueScriptableObject clue in cluesToFind)
        {
            PlaceClue(clue);
        }

        // DebugSpawnPoints(suspect.suspectClues[0]);
        uIController.InitializeDossier(allSuspects);


    }

    // Update is called once per frame
    void OnClueScanned(Clue clue)
    {
        Debug.ClearDeveloperConsole();

        // Adapted from a StackOverflow answer from Amritpal Singh in 2012 and edited by Peter Mortensen in 2016
        // https://stackoverflow.com/a/9854954
        ClueScriptableObject scannedClue = cluesToFind.Find(item => item.clueName == clue.gameObject.transform.parent.name);

        foundClues.Add(scannedClue);
        cluesToFind.Remove(scannedClue);

        Debug.Log("Clues found:" + foundClues.Count);

        if (cluesToFind.Count == 0)
        {
            Debug.Log("All clues found.");
            uIController.GameEnded();
        }
    }


 

    public List<string> OnSuspectSelect(string input)
    {
        List<string> results = new();
        if (input == murderer.suspectName)
        {
            results.Add("You got the right suspect!");

        }
        else
        {
            results.Add("You got the wrong suspect.");
        }

        results.Add("The killer was " + murderer.suspectName);
        results.Add($"You got {foundClues.Count()} out of {totalClues} clues.");
        results.Add($"It took you {Time.time} seconds.");
        results.Add("Score: 100");

        
        return results;


    }




    private void DebugSpawnPoints(ClueScriptableObject clue)
    {
        foreach (Vector3 spawnPoint in spawnPoints)
        {
            GameObject currentClue = Instantiate(clue.model, spawnPoint, Quaternion.identity);
            currentClue.name = clue.clueName;
        }
    }

    // private void CreateSpawnPointsFromCSV()
    // {
    //     // unused.
    //     spawnPoints = new List<Vector3>();
    //     string[] lines = spawnPointsFile.text.Split("\n");
    //     foreach (string line in lines)
    //     {
    //         string[] words = line.Split(",");
    //         Debug.Log(line);
    //         Vector3 vector = new Vector3(float.Parse(words[0]), float.Parse(words[1]), float.Parse(words[2]));
    //         spawnPoints.Add(vector);
    //     }
    // }

    private SuspectScriptableObject PickRandomSuspect()
    {
        int x = UnityEngine.Random.Range(0, activeSuspects.Count());
        SuspectScriptableObject suspect = Instantiate(activeSuspects[x]);
        // (“Scriptable Objects change thier values during run time and these persist - Unity Engine,” 2024. https://discussions.unity.com/t/scriptable-objects-change-thier-values-during-run-time-and-these-persist/1507422) 
        //remove them from the list so we can pick clues from other suspects
        activeSuspects.RemoveAt(x);
        Debug.Log("Selected suspect: " + suspect.suspectName);

        return suspect;
    }
    void PlaceClue(ClueScriptableObject clue)
    {

        Vector3 spawnPoint = GetSpawnPointFromRandomZone(clue);

        GameObject currentClue = Instantiate(clue.model, spawnPoint, Quaternion.identity);
        Clue clueObject = currentClue.GetComponent<Clue>();
        if (clueObject != null)
        {
            clueObject.Init(clue);
        }
        else
        {
            Debug.LogWarning($"No clue script found on clue prefab {clue.name}");
        }
        currentClue.name = clue.clueName;
    }
    private Vector3 GetSpawnPointFromRandomZone(ClueScriptableObject clue)
    {
        bool cluePlaced = false;
        int x;
        x = 0;
        while (!cluePlaced)
        {
            x++;
            int i = UnityEngine.Random.Range(0, zones.transform.childCount);
            Zone zone = zones.transform.GetChild(i).GetComponent<Zone>();
            if (zone.AddClueToZone(clue.validSpawnZones))
            {
                return zone.getSpawnPoint();

            }
            if (x == 10)
            {
                break;
            }

        }
        Debug.LogWarning("Could not find a spawn point");
        x = 0;
        return new Vector3(0, 0, 0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("test");
    }

}

