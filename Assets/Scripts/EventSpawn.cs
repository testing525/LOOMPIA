using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventSpawn : MonoBehaviour
{
    [Header("Spawner Settings")]
    public Transform spawner;
    public GameObject eventPrefab;
    public Sprite[] sprites;
    public float moveSpeed = 3f;
    public float respawnDelay = 10f;
    public float offScreenOffset = 2f; // how far outside before stopping

    private static List<string> dataDialogue = new List<string>()
    {
        "Healthy soil isn’t just good for your crops but also holds water, stores carbon from the air, and keeps farms alive.",
        "2024 was the hottest year ever… but each tree planted cools and cleans the air for us all!",
        "If you want to save our planet, we must take care of our nature! Recycling! Planting! and saving energy!",
        "Governments may set laws, but small acts like cutting waste and protecting nature guide the real future!",
        "Plastic bottles don’t just vanish! they end up in rivers, seas, and even our food. Throw them properly!",
        "Save water, cut waste, plant a tree, or keep a plant alive! these daily steps shape our world’s future!",
        "Pests are spreading faster with the changing climate, making it harder for farmers to protect crops!",
        "Soil loses nutrients from chemicals and waste. Be organic and reduce waste protects our farms and future!",
        "Extra food shouldn’t go to the trash, give it to those in need, feed pets or strays, or help farms by composting.",
        "Think before tossing, food can still serve a purpose beyond the plate.",
        "Plant one tree today; it gives shade, clean air, and life tomorrow.",
        "Every plastic we avoid keeps oceans cleaner. Carry reusable bags! it’s a small choice with big impact.",
        "Farmers need clean water for crops. Don’t pollute rivers, protect their lifeline!",
        "One person inspires many. Start eco-habits and others will follow!",
        "Rising heat dries farmlands. Plant trees and cut waste to cool our world!"
    };

    void Start()
    {
        GameObject newEvent = Instantiate(eventPrefab, spawner.position, spawner.rotation);
        StartCoroutine(HandleEvent(newEvent));
    }

    IEnumerator HandleEvent(GameObject evt)
    {
        SpriteRenderer eventSprite = evt.GetComponentInChildren<SpriteRenderer>();
        TextMeshPro text = evt.GetComponentInChildren<TextMeshPro>();

        Camera mainCam = Camera.main;
        float screenLeft = mainCam.ViewportToWorldPoint(new Vector3(0, 0, mainCam.nearClipPlane)).x - offScreenOffset;

        while (true)
        {
            eventSprite.sprite = sprites[Random.Range(0, sprites.Length)];
            text.text = dataDialogue[Random.Range(0, dataDialogue.Count)];

            evt.transform.position = spawner.position;

            bool isMoving = true;

            while (isMoving)
            {
                float objectSpeed = moveSpeed * GameSpeed.Instance.GetMultiplier();
                evt.transform.Translate(Vector3.left * objectSpeed * Time.deltaTime);

                if (evt.transform.position.x < screenLeft)
                {
                    isMoving = false;
                }

                yield return null;
            }

            yield return new WaitForSeconds(respawnDelay);
        }
    }
}
