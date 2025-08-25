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

    /// <summary>
    /// Add a clue to the zone if the max number of clues in this zone has not been reached.
    /// Call <c> GetSpawnPoint</c> to get a valid location to place the clue.
    /// </summary>
    /// <returns> True if a clue can be placed within this zone.
    /// False if the max number of clues has already been placed, or the clue is not allowed to spawn in this zone.</returns>
    public bool AddClueToZone(List<string> validSpawnZones)
    {
        
        if (numOfClues == maxNumberOfClues ||  !IsValidZoneForClue(validSpawnZones))
        {
            return false;
        }


        numOfClues++;
        return true;
    }

    public bool IsValidZoneForClue(List<string> validSpawnZones)
    {
        if (validSpawnZones.Count == 0) return true;

        if (validSpawnZones.Contains(gameObject.name)) return true;

        return false;
    }

    /// <summary>
    /// Locates a valid spawn point within this zone.
    /// </summary>
    /// <returns>
    /// A Vector3 containing a valid spawn point. If the zone has manually set spawn points, this will be one of these.
    /// If not, a spawn point will be determined within the bounds of the GameObject.
    /// </returns>
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

        return new Vector3(x, 0f, z);
    }
    
}
