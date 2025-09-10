using UnityEngine;
using System.Collections;

public class EventSpawnManager : MonoBehaviour
{
    [System.Serializable]
    public class NPCData
    {
        public string npcName;
        public GameObject npcPrefab;
        public Transform spawnPoint;
        public int targetSceneIndex;
    }

    public NPCData[] npcs;

    private GameObject currentNPC;

    public void SpawnNPC(string npcType)
    {
        NPCData data = System.Array.Find(npcs, n => n.npcName == npcType);
        if (data == null || data.npcPrefab == null || data.spawnPoint == null) return;

        currentNPC = Instantiate(data.npcPrefab, data.spawnPoint.position, data.spawnPoint.rotation);
    }

    public void OnPlayerAcceptRequest(string npcType)
    {
        NPCData data = System.Array.Find(npcs, n => n.npcName == npcType);
        if (data != null)
        {
            if (currentNPC != null) Destroy(currentNPC);

            UnityEngine.SceneManagement.SceneManager.LoadScene(data.targetSceneIndex);
        }
    }

    public void OnPlayerCancelRequest()
    {
        if (currentNPC != null) Destroy(currentNPC);
    }
}
