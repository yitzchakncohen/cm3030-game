using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource ambientNoiseSource;
    [SerializeField] private AudioSource footStepSource;

    [Header("Sound FX")]
    [SerializeField] private AudioClip[] footStepSounds;
    [SerializeField] private AudioClip music;
    [SerializeField] private AudioClip ambientSound;
    
    [Header("Game References")]
    [SerializeField] private CharacterController characterController;

    private void Start()
    {
        musicSource.loop = true;
        musicSource.clip = music;
        musicSource.Play();

        ambientNoiseSource.loop = true;
        ambientNoiseSource.clip = ambientSound;
        ambientNoiseSource.Play();
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
    }
}
