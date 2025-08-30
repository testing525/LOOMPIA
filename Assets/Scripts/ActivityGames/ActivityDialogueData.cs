using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActivityDialogueData", menuName = "Dialogue/DialogueData")]
public class ActivityDialogueData : ScriptableObject
{
    [TextArea(2, 5)]
    public List<string> dialogues = new List<string>();
}
