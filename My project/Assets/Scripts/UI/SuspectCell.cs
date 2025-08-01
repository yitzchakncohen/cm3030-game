using System;
using TMPro;
using UnityEngine;

public class SuspectCell : MonoBehaviour
{
    [SerializeField] private TMP_Text suspectName;
    [SerializeField] private TMP_Text suspectDescription;

    public void Init(SuspectScriptableObject suspectScriptableObject)
    {
        suspectName.text = suspectScriptableObject.suspectName;
        suspectDescription.text = suspectScriptableObject.Description;
    }
}
