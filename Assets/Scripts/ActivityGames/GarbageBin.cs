using UnityEngine;

public class GarbageBin : MonoBehaviour
{
    public enum BinType { Biodegradable, Hazardous, Recyclable }
    public BinType binType;

    [Header("Highlight")]
    public GameObject highlight;

    private void Start()
    {
        if (highlight != null) highlight.SetActive(false);
    }

    public void TryDisposeGarbage(GarbagePlayerItemHandler handler)
    {
        if (handler == null || handler.heldGarbage == null) return;

        if (handler.heldGarbage.garbageType.ToString() == binType.ToString())
        {
            Destroy(handler.heldGarbage.gameObject);
            handler.heldGarbage = null;
            ObjectiveManager.Instance.AddObjective();
            Debug.Log($"Garbage disposed into {binType} bin!");
        }
        else
        {
            DialogueManager.Instance.Chat("Don't put it in a wrong bin!");
            Debug.Log($"Garbage type does not match {binType} bin.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GarbagePlayerItemHandler handler = other.GetComponent<GarbagePlayerItemHandler>();
        if (handler != null)
        {
            handler.nearbyBin = this;
            if (highlight != null) 
            { 
                highlight.SetActive(true); 
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GarbagePlayerItemHandler handler = other.GetComponent<GarbagePlayerItemHandler>();
        if (handler != null && handler.nearbyBin == this)
        {
            handler.nearbyBin = null;
            if (highlight != null) 
            {
                highlight.SetActive(false); 
            }
        }
    }
}
