using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClueSpawner : MonoBehaviour
{
    [SerializeField]
    ClueScriptableObject[] clues;

    public TextAsset spawnPointsFile;

    List<Vector3> spawnPoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPoints = new List<Vector3>();
        
        string[] lines = spawnPointsFile.text.Split("\n");

        foreach (string line in lines)
        {
            string[] words = line.Split(",");
            Debug.Log(line);
            Vector3 vector = new Vector3(float.Parse(words[0]), float.Parse(words[1]), float.Parse(words[2]));
            spawnPoints.Add(vector);
        }


        foreach (var clue in clues)
        {
            foreach (Vector3 spawnPoint in spawnPoints)
            {
                GameObject currentClue = Instantiate(clue.clueModel, spawnPoint, Quaternion.identity);
                currentClue.name = clue.clueName;
            
                
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
