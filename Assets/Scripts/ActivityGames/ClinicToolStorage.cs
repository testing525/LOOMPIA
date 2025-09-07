using UnityEngine;

public class ClinicToolStorage : MonoBehaviour
{
    [Header("Settings")]
    public ClinicEnum.ToolType storageTool;

    [Header("References")]
    public GameObject highlight;

    private void Start()
    {
        if (highlight != null)
        {
            highlight.SetActive(false);
        }
    }

    public ClinicEnum.ToolType GetToolType()
    {
        return storageTool;
    }
}
