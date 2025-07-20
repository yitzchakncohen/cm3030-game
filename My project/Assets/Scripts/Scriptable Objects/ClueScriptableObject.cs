using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClueScriptableObject", menuName = "Scriptable Objects/ClueScriptableObject")]
public class ClueScriptableObject : ScriptableObject
{
    public string clueName;

    public GameObject model;

    public string description;

    public List<Vector3> defaultSpawnPoints;
}
