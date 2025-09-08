using UnityEngine;

public class GarbagePlayerItemHandler : MonoBehaviour
{
    [Header("References")]
    public Transform placeholder; 
    public KeyCode interactKey = KeyCode.E;

    private Garbage nearbyGarbage;
    public Garbage heldGarbage;
    [HideInInspector] public GarbageBin nearbyBin; 

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            if (heldGarbage != null && nearbyBin != null)
            {
                nearbyBin.TryDisposeGarbage(this);
            }
            else if (heldGarbage != null)
            {
                DropGarbage();
            }
            else if (heldGarbage == null && nearbyGarbage != null)
            {
                PickUpGarbage(nearbyGarbage);
            }
        }
    }



    private void PickUpGarbage(Garbage garbage)
    {
        heldGarbage = garbage;

        garbage.transform.SetParent(placeholder);
        garbage.transform.localPosition = Vector3.zero;
        garbage.transform.localRotation = Quaternion.identity;

        Collider2D col = garbage.GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        if (garbage.highlight != null) 
        {
            garbage.highlight.SetActive(false);
        } 


        Debug.Log($"Picked up garbage: {garbage.garbageType}");
    }

    private void DropGarbage()
    {
        if (heldGarbage == null) return;

        heldGarbage.transform.SetParent(null);

        Vector3 dropPos = heldGarbage.transform.position; 
        dropPos.x = placeholder.position.x;               
        dropPos.y = placeholder.position.y - 0.5f;       
        heldGarbage.transform.position = dropPos;
        heldGarbage.transform.rotation = Quaternion.identity;

        Collider2D col = heldGarbage.GetComponent<Collider2D>();
        if (col != null) 
        {
            col.enabled = true;
        }

        Debug.Log($"⬇️ Dropped garbage: {heldGarbage.garbageType}");

        heldGarbage = null;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Garbage garbage = other.GetComponent<Garbage>();
        if (garbage != null && heldGarbage == null) // ✅ only detect if not already holding
        {
            nearbyGarbage = garbage;
            if (garbage.highlight != null) garbage.highlight.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Garbage garbage = other.GetComponent<Garbage>();
        if (garbage != null && garbage == nearbyGarbage)
        {
            if (garbage.highlight != null) garbage.highlight.SetActive(false);
            nearbyGarbage = null;
        }
    }
}
