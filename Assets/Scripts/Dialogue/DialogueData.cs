using UnityEngine;

[CreateAssetMenu(menuName = "NPCDialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    [TextArea(2, 5)] public string[] lines;
}
