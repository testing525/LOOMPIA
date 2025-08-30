using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class CustomSceneManager : MonoBehaviour
{
    public GameManager GameManager;
    public Menu menu;

    public void Cancel()
    {
        GameManager.StartNewRound();
        menu.HideActivityMenu();
    }

    public void LoadScene1()
    {
        StartCoroutine(LoadSceneWithTransition(1));
    }

    public void LoadScene2()
    {
        StartCoroutine(LoadSceneWithTransition(2));
    }

    public void LoadScene3()
    {
        StartCoroutine(LoadSceneWithTransition("Scene3"));
    }

    private IEnumerator LoadSceneWithTransition(int sceneIndex)
    {
        yield return SceneTransition.Instance.StartCoroutine(SceneTransition.Instance.FadeIn());

        SceneManager.LoadScene(sceneIndex);
    }

    private IEnumerator LoadSceneWithTransition(string sceneName)
    {
        yield return SceneTransition.Instance.StartCoroutine(SceneTransition.Instance.FadeIn());

        SceneManager.LoadScene(sceneName);
    }
}
