using UnityEngine;

public class StoryVoicePlayer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private AudioSource audioSource;

    [Header("Instruction Audio Clips")]
    [SerializeField] private AudioClip[] instructionClips;

    [Header("Settings")]
    [SerializeField] private int playFromInstructionIndex = 6;

    private int lastInstructionIndex = -1;
    private int lastDialogueIndex = -1;

    private void Update()
    {
        if (dialogueManager == null || audioSource == null) return;

        int currentInstructionIndex = dialogueManager.CurrentInstructionIndex;
        if (currentInstructionIndex != lastInstructionIndex)
        {
            if (currentInstructionIndex >= playFromInstructionIndex)
                PlayInstructionAudio(currentInstructionIndex);

            lastInstructionIndex = currentInstructionIndex;
        }

    }

    private void PlayInstructionAudio(int index)
    {
        if (instructionClips != null && index >= 0 && index < instructionClips.Length)
        {
            AudioClip clip = instructionClips[index];
            if (clip != null)
            {
                audioSource.Stop();
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }
}
