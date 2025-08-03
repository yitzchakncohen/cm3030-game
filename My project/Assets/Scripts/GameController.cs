using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GameController : MonoBehaviour
{
    [SerializeField] private UIController uIController;

    List<ClueScriptableObject> activeClues;

    // public TextAsset spawnPointsFile;

    public List<Vector3> spawnPoints;

    public List<SuspectScriptableObject> allSuspects;
    List<SuspectScriptableObject> activeSuspects;
    private SuspectScriptableObject murderer;

    private List<ClueScriptableObject> foundClues;
    private static InputManager inputManager;

    private static GameObject player;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Pick a suspect.
        activeSuspects = new List<SuspectScriptableObject>(allSuspects);
        SuspectScriptableObject suspect = PickRandomSuspect();
        murderer = Instantiate(suspect);

        player = GameObject.Find("Player");

        InputManager inputManager = player.GetComponent<InputManager>();
        inputManager.OnSuspectSelectInput += GameController_onSuspectSelectInput;

        //
        ClueScanner clueScanner = player.GetComponent<ClueScanner>();
        clueScanner.OnClueScanned += OnClueScanned;
        foundClues = new List<ClueScriptableObject>();

        // Debug.Log(activeSuspects.Count());


        int redHerrings = UnityEngine.Random.Range(1, 4);
        int numOfClues = UnityEngine.Random.Range(3, 5);

        activeClues = suspect.pickRandomClues(numOfClues);


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
            activeClues.Add(suspect.pickRandomClue());
        }

        foreach (ClueScriptableObject clue in activeClues)
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
        ClueScriptableObject scannedClue = activeClues.Find(item => item.clueName == clue.transform.parent.name);
        Debug.Log("Picked up new clue: " + scannedClue.clueName);

        foundClues.Add(scannedClue);
        activeClues.Remove(scannedClue);

        Debug.Log("Clues found:" + foundClues.Count);

        if (activeClues.Count == 0)
        {
            Debug.Log("All clues found.");
        }
            OnScoreLimitReached();

    }
    

    private void OnScoreLimitReached()
    {
        for (int i = 0; i < allSuspects.Count(); i++)
        {
            int numberToPress = i;
            numberToPress += 1;
            Debug.Log("Suspect " + numberToPress+ ": " + allSuspects[i].suspectName);
        }

        Debug.Log("Press the number of the suspect you want to pick.");

    }

    private void GameController_onSuspectSelectInput(int input)
    {
        if (foundClues.Count() == 0)
        {
            Debug.Log("No clues found. Skipping");
            return;
        }
        Debug.Log("User selected a suspect.");
        Debug.Log(input);
        SuspectScriptableObject selectedSuspect = allSuspects[input - 1];
        if (selectedSuspect.suspectName == murderer.suspectName)
        {
            Debug.Log("Selected the right suspect! You win!");
        }
        else
        {
            Debug.Log("You picked the wrong suspect");
        }
        
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
        int i;
        List<Vector3> activeSpawnPoints;
        if (clue.defaultSpawnPoints.Count() != 0)
        {
            activeSpawnPoints = clue.defaultSpawnPoints;
        }
        else
        {
            activeSpawnPoints = spawnPoints;
        }
        i = UnityEngine.Random.Range(0, clue.defaultSpawnPoints.Count());

<<<<<<< Updated upstream
=======
        Vector3 spawnPoint = GetSpawnPointFromRandomZone(clue);
>>>>>>> Stashed changes

        GameObject currentClue = Instantiate(clue.model, activeSpawnPoints[i], Quaternion.identity);
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
<<<<<<< Updated upstream
        activeSpawnPoints.RemoveAt(i);
=======
    }
    private Vector3 GetSpawnPointFromRandomZone(ClueScriptableObject clue)
    {
        bool cluePlaced = false;
        int x;
        x = 0;
        while (!cluePlaced)
        {
            x++;
            int i = Random.Range(0, zones.transform.childCount);
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
        return new Vector3(0, 0, 0);
>>>>>>> Stashed changes
    }
}
