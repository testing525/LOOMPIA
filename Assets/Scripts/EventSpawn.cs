using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EventSpawn : MonoBehaviour
{
    public Transform spawner;
    public GameObject eventPrefab;
    public Sprite[] sprites;
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
        StartCoroutine(SpawnEvents());
    }

    IEnumerator SpawnEvents()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(60f); 
        }
    }

    void Spawn()
    {
        Instantiate(eventPrefab, spawner.position, spawner.rotation);

        SpriteRenderer eventSprite = eventPrefab.GetComponentInChildren<SpriteRenderer>();
        TextMeshPro text = eventPrefab.GetComponentInChildren<TextMeshPro>();
        Sprite randomSprite = sprites[Random.Range(0, sprites.Length)];
        eventSprite.sprite = randomSprite;

        string randomText = dataDialogue[Random.Range(0, dataDialogue.Count)];
        text.text = randomText;

    }

}
