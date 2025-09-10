using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ActivityDone : MonoBehaviour
{
    [Header("References")]
    public Objective objective;
    public CulinaryObjective culinaryObjective;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (objective != null && objective.remainingObjectives <= 0)
            {
                StartCoroutine(LoadSceneWithTransition(3));
            }
            else if (culinaryObjective != null && culinaryObjective.remainingObjectives <= 0)
            {
                StartCoroutine(LoadSceneWithTransition(3));
            }
            else
            {
                Debug.Log("lol nah");
            }
        }
    }

    private IEnumerator LoadSceneWithTransition(int sceneIndex)
    {
        SceneTransition.Instance.isReadyToChangeScene = false;

        yield return SceneTransition.Instance.StartCoroutine(SceneTransition.Instance.FadeIn());

        SceneManager.LoadScene(sceneIndex);
    }
}
