using UnityEngine;

[CreateAssetMenu(fileName = "ClueScriptableObject", menuName = "Scriptable Objects/ClueScriptableObject")]
public class ClueScriptableObject : ScriptableObject
{
    public string clueName;

    public GameObject model;

    public string description;
}
