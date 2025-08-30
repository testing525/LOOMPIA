using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PickAndDrop : MonoBehaviour
{
    [Header("Pickup Settings")]
    public Transform holdPoint; // Where the item attaches

    private Rigidbody2D heldRb;
    private GameObject heldItem;
    public GameObject HeldItem => heldItem;
    private Vector3 heldItemOriginalScale;

    // Instead of 1, keep a list of items in range
    private List<PickupItem> itemsInRange = new List<PickupItem>();

    void Update()
    {
        // Show nearest item prompt
        PickupItem nearestItem = GetNearestItem();
        if (nearestItem != null && heldItem == null)
        {
           // Debug.Log("Press E to pick up: " + nearestItem.name);
            
        }

        // Pickup / Drop logic
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldItem == null && nearestItem != null)
            {
                PickUpItem(nearestItem.gameObject);
            }
            else if (heldItem != null)
            {
                DropItem();
            }
        }
    }

    void PickUpItem(GameObject item)
    {
        Debug.Log("Picked up " + item.name);

        heldItem = item;
        heldRb = heldItem.GetComponent<Rigidbody2D>();

        if (heldRb != null)
        {
            heldRb.isKinematic = true;
            heldRb.velocity = Vector2.zero;
        }

        PickupItem pickupData = item.GetComponent<PickupItem>();
        if (pickupData != null)
        {
            pickupData.SetPickedUp(true);
        }

        heldItemOriginalScale = heldItem.transform.localScale;

        heldItem.transform.SetParent(holdPoint);
        heldItem.transform.localPosition = Vector3.zero;
        heldItem.transform.localScale = heldItemOriginalScale; 
    }


    void DropItem()
    {
        if (heldItem == null) return;

        Debug.Log("Dropped " + heldItem.name);

        // Detach
        heldItem.transform.SetParent(null);

        if (heldRb != null)
        {
            heldRb.isKinematic = false;
        }

        PickupItem pickupData = heldItem.GetComponent<PickupItem>();
        if (pickupData != null)
        {
            pickupData.SetPickedUp(false);
        }

        heldItem.transform.localScale = heldItemOriginalScale;

        // Clear references
        heldItem = null;
        heldRb = null;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        PickupItem item = other.GetComponent<PickupItem>();
        if (item != null && !itemsInRange.Contains(item))
        {
            itemsInRange.Add(item);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        PickupItem item = other.GetComponent<PickupItem>();
        if (item != null && itemsInRange.Contains(item))
        {
            itemsInRange.Remove(item);
        }
    }

    // Helper: Get the nearest item to the player
    PickupItem GetNearestItem()
    {
        if (itemsInRange.Count == 0) return null;

        PickupItem nearest = null;
        float minDist = Mathf.Infinity;

        foreach (PickupItem item in itemsInRange)
        {
            if (item == null) continue; // safety
            float dist = Vector2.Distance(transform.position, item.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = item;
            }
        }
        return nearest;
    }

    public void ClearHeldItem()
    {
        heldItem = null;
        heldRb = null;
    }

    public void OnPickupButtonPressed()
    {
        PickupItem nearestItem = GetNearestItem();

        if (heldItem == null && nearestItem != null)
        {
            PickUpItem(nearestItem.gameObject);
            Debug.Log("Pickup button pressed: picked up " + nearestItem.name);
        }
        else if (heldItem != null)
        {
            DropItem();
            Debug.Log("Pickup button pressed: dropped " + heldItem.name);
        }
    }


}