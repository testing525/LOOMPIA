using UnityEngine;

public class Garbage : MonoBehaviour
{
    public enum GarbageType { Recyclable, Hazardous, Biodegradable }
    public GarbageType garbageType;
    public GameObject highlight;

    private void Start()
    {
        highlight.SetActive(false);
    }
}
