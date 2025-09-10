using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NPCTutorialScript : MonoBehaviour
{
    [Header("Prefab Settings")]
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private Transform spawnPoint;

    public Animator animator;

    public void TriggerSpawn()
    {
        StartCoroutine(SpawnAndStop());

        DialogueManager.Instance.OnDialogueFinished += HandleDialogueFinished;

    }

    private void HandleDialogueFinished()
    {
         
        SceneManager.LoadScene(9);
    }

    private IEnumerator SpawnAndStop()
    {
        if (prefabToSpawn == null || spawnPoint == null) yield break;

        Vector3 spawnPos = new Vector3(spawnPoint.position.x, spawnPoint.position.y + 0.7f, spawnPoint.position.z);

        GameObject obj = Instantiate(prefabToSpawn, spawnPos, spawnPoint.rotation);

        yield return new WaitForSeconds(5f);

        if (GameSpeed.Instance != null)
        {
            GameSpeed.Instance.SetStop();
            DialogueManager.Instance.StartDialogue();
            animator.SetBool("isRunning", false);
        }
    }
}
