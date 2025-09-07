using UnityEngine;

[CreateAssetMenu(menuName = "NPC/Dialogue", fileName = "NewDialogue")]
public class NPCDialogueSO : ScriptableObject
{
    [TextArea(3, 6)]
    public string[] dialogueLines;
}
