using UnityEngine;

public class FarmPlayerItemHandler : MonoBehaviour
{
    public enum ItemType { None, Fertilizer, SeedBox, WaterCan }
    public ItemType heldItem = ItemType.None;

    [Header("Interaction Settings")]
    public float interactRange = 0.2f;
    public LayerMask itemLayer;
    public LayerMask farmBoxLayer;
    public KeyCode interactKey = KeyCode.E;
    private FarmBox lastBox; 

    [Header("References")]
    public Transform itemHoldPoint;

    private FarmItem nearbyItem;
    private FarmItem heldFarmItem;
    private FarmBox nearbyBox;

    private void Update()
    {
        DetectNearbyItem();
        DetectFarmBox();

        if (Input.GetKeyDown(interactKey))
        {
            Interact();
        }
    }

    public void Interact()
    {
        if (heldItem == ItemType.None && nearbyItem != null)
        {
            PickupItem(nearbyItem);
        }
        else if (heldItem != ItemType.None && nearbyBox != null)
        {
            nearbyBox.Interact(this);
        }
        else if (heldItem != ItemType.None && heldFarmItem != null)
        {
            DropItem();
        }
    }

    private void DetectNearbyItem()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, interactRange, itemLayer);
        nearbyItem = hit != null ? hit.GetComponent<FarmItem>() : null;
    }

    private void DetectFarmBox()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, interactRange, farmBoxLayer);
        if (hit != null)
        {
            FarmBox box = hit.GetComponent<FarmBox>();
            if (box != null)
            {
                if (box != nearbyBox && nearbyBox != null)
                    nearbyBox.ShowHighlight(false);

                nearbyBox = box;
                nearbyBox.ShowHighlight(true);

                Debug.Log($"Close to farmbox: {nearbyBox.name} (State: {nearbyBox.currentState})");
            }
        }
        else
        {
            if (nearbyBox != null)
            {
                nearbyBox.ShowHighlight(false);
                nearbyBox = null;
            }
        }
    }

    private void PickupItem(FarmItem item)
    {
        heldItem = item.itemType;
        heldFarmItem = item;

        item.transform.SetParent(itemHoldPoint);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;

        if (item.highlight != null) item.highlight.SetActive(false);

        Debug.Log($"Picked up {heldItem}");
    }

    private void DropItem()
    {
        heldFarmItem.transform.SetParent(null);
        heldFarmItem.transform.position = heldFarmItem.originalPosition;

        Debug.Log($"Dropped {heldItem}");

        ClearItem();
    }

    public void ClearItem()
    {
        Debug.Log($"Used {heldItem}, now empty handed.");
        heldItem = ItemType.None;
        heldFarmItem = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
