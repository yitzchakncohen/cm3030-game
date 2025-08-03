using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] int maxNumberOfClues;
    [SerializeField] List<Vector3> spawnPoints;
    private int numOfClues;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public bool AddClueToZone()
    {
        //returns false if 
        if (numOfClues == maxNumberOfClues)
        {
            return false;
        }

        numOfClues++;
        return true;
    }

    public Vector3 getSpawnPoint()
    {
        if (spawnPoints.Count != 0)
        {
            int i = Random.Range(0, spawnPoints.Count);
            return spawnPoints[i];
        }
        float minX = transform.position.x - transform.lossyScale.x / 2;
        float maxX = transform.position.x + transform.lossyScale.x / 2;

        float minZ = transform.position.z - transform.lossyScale.z / 2;
        float maxZ = transform.position.z + transform.lossyScale.z / 2;

        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);

        return new Vector3(x, 0.5f, z);
    }
    
}
