using UnityEngine;
using System.Collections.Generic;

public class LibraryPlayerItemHandler : MonoBehaviour
{
    [Header("Pickup Settings")]
    public Transform holdPoint;
    public KeyCode interactKey = KeyCode.E;

    private BookBox heldBox = null;
    private List<BookBox> nearbyBoxes = new List<BookBox>();
    private BookBox currentHighlight = null;
    private Collider2D disabledCollider = null;

    private void Update()
    {
        if (heldBox == null)
        {
            BookBox nearest = GetNearestBox();
            if (nearest != currentHighlight)
            {
                if (currentHighlight != null)
                    currentHighlight.ShowHighlight(false);

                currentHighlight = nearest;

                if (currentHighlight != null)
                    currentHighlight.ShowHighlight(true);
            }
        }
        else
        {
            if (currentHighlight != null)
            {
                currentHighlight.ShowHighlight(false);
                currentHighlight = null;
            }
        }

        if (Input.GetKeyDown(interactKey))
        {
            if (heldBox == null && currentHighlight != null)
            {
                PickupBox(currentHighlight);
            }
            else if (heldBox != null)
            {
                DropBox();
            }
        }
    }

    private void PickupBox(BookBox box)
    {
        if (box == null) return;

        heldBox = box;
        heldBox.ShowHighlight(false);
        heldBox.transform.SetParent(holdPoint);
        heldBox.transform.localPosition = Vector3.zero;
        heldBox.transform.localRotation = Quaternion.identity;

        Rigidbody2D rb = heldBox.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        foreach (var col in heldBox.GetComponents<Collider2D>())
        {
            if (!col.isTrigger)
            {
                col.enabled = false;
                disabledCollider = col;
                break;
            }
        }

        currentHighlight = null;
    }

    private void DropBox()
    {
        if (heldBox == null) return;

        heldBox.transform.SetParent(null);
        heldBox.transform.position += new Vector3(0f, -0.2f, 0f);

        Rigidbody2D rb = heldBox.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        if (disabledCollider != null)
        {
            disabledCollider.enabled = true;
            disabledCollider = null;
        }

        heldBox = null;
    }

    public void ClearHeldBox()
    {
        if (heldBox == null) return;

        if (disabledCollider != null)
        {
            disabledCollider.enabled = true;
            disabledCollider = null;
        }

        Destroy(heldBox.gameObject);
        heldBox = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BookBox box = collision.GetComponent<BookBox>();
        if (box != null && !nearbyBoxes.Contains(box))
            nearbyBoxes.Add(box);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        BookBox box = collision.GetComponent<BookBox>();
        if (box != null && nearbyBoxes.Contains(box))
        {
            if (box == currentHighlight)
            {
                box.ShowHighlight(false);
                currentHighlight = null;
            }
            nearbyBoxes.Remove(box);
        }
    }

    private BookBox GetNearestBox()
    {
        BookBox nearest = null;
        float minDist = float.MaxValue;

        foreach (var box in nearbyBoxes)
        {
            if (box == null) continue;

            float dist = Vector2.Distance(transform.position, box.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = box;
            }
        }

        return nearest;
    }

    public bool IsHolding() => heldBox != null;

    public Bookshelf.ShelfType? GetHeldType() => heldBox != null ? heldBox.boxType : (Bookshelf.ShelfType?)null;
}
