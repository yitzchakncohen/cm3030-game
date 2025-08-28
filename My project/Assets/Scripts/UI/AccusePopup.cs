using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccusePopup : MonoBehaviour
{
    public event Action<SuspectScriptableObject> OnAccuseSuspect;
    private const string TITLE_TEXT = "Accuse {suspect}?";
    private const string DESCRIPTION_TEXT = "Are you sure you want to accuse the {suspect}? You only get one chance to get it right.";
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private Button keepLookingButton;
    [SerializeField] private Button accuseButton;
    private SuspectScriptableObject suspect;

    private void Start()
    {
        keepLookingButton.onClick.AddListener(OnKeepLookingPress);
        accuseButton.onClick.AddListener(OnAccusePress);
    }

    private void OnDestroy()
    {
        keepLookingButton.onClick.RemoveListener(OnKeepLookingPress);
        accuseButton.onClick.RemoveListener(OnAccusePress);
    }

    public void Init(SuspectScriptableObject accusedSuspect)
    {
        suspect = accusedSuspect;
        title.text = TITLE_TEXT.Replace("{suspect}", accusedSuspect.name);
        description.text = DESCRIPTION_TEXT.Replace("{suspect}", accusedSuspect.name);
    }
    
    private void OnKeepLookingPress()
    {
        gameObject.SetActive(false);
    }

    private void OnAccusePress()
    {
        OnAccuseSuspect?.Invoke(suspect);
    }
}
