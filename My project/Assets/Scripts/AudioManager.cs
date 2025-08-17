using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource ambientNoiseSource;
    [SerializeField] private AudioSource uiNoiseSource;
    [SerializeField] private AudioSource footStepSource;
    [SerializeField] private AudioSource playerSource;

    [Header("Sound FX")]
    [SerializeField] private AudioClip[] footStepSounds;
    [SerializeField] private AudioClip music;
    [SerializeField] private AudioClip ambientSound;
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip clueScanning;
    [SerializeField] private AudioClip clueScanned;

    [Header("Game References")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private ClueScanner clueScanner;

    private Button[] buttons;

    private void Start()
    {
        musicSource.loop = true;
        musicSource.clip = music;
        musicSource.Play();

        ambientNoiseSource.loop = true;
        ambientNoiseSource.clip = ambientSound;
        ambientNoiseSource.Play();

        buttons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(OnButtonClick);
        }

        clueScanner.OnClueScanned += OnClueScanned;
    }

    void OnDestroy()
    {
        foreach (Button button in buttons)
        {
            button.onClick.RemoveAllListeners();
        }

        clueScanner.OnClueScanned -= OnClueScanned;
    }

    private void Update()
    {
        if (characterController.IsGrounded && characterController.IsMoving)
        {
            if (!footStepSource.isPlaying)
            {
                int randomSound = Random.Range(0, footStepSounds.Length);
                AudioClip footStepSound = footStepSounds[randomSound];
                footStepSource.PlayOneShot(footStepSound);
            }
        }

        if (clueScanner.IsScanning && clueScanner.TargetClue)
        {
            if (!playerSource.isPlaying)
            {
                playerSource.loop = true;
                playerSource.clip = clueScanning;
                playerSource.Play();
            }
        }
        else
        {
            playerSource.loop = false;
        }
    }

    private void OnButtonClick()
    {
        uiNoiseSource.PlayOneShot(buttonClick);
    }

    private void OnClueScanned(Clue clue)
    {
        playerSource.PlayOneShot(clueScanned);
    }
}
