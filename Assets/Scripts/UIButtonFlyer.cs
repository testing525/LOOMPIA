using UnityEngine;
using System.Collections;
using System;

public class UIButtonFlyer : MonoBehaviour
{
    [Header("References")]
    public GameObject flyerPrefab;

    public void SpawnAndMove(Vector3 startWorldPos, Vector3 targetWorldPos, float duration = 0.5f, float stayDuration = 0.7f, Action<GameObject> onFinished = null)
    {
        SpawnAndMoveWithPrefab(flyerPrefab, startWorldPos, targetWorldPos, duration, stayDuration, onFinished);
    }

    public void SpawnAndMoveWithPrefab(GameObject prefab, Vector3 startWorldPos, Vector3 targetWorldPos, float duration = 2f, float stayDuration = 0.75f, Action<GameObject> onFinished = null)
    {
        if (prefab == null) return;

        GameObject flyer = Instantiate(prefab, startWorldPos, Quaternion.identity);
        flyer.transform.localScale = Vector3.one;

        StartCoroutine(MoveAndScale(flyer.transform, targetWorldPos, duration, stayDuration, onFinished));
    }

    private IEnumerator MoveAndScale(Transform obj, Vector3 targetPos, float duration, float stayDuration, Action<GameObject> onFinished)
    {
        yield return new WaitForSeconds(stayDuration);

        Vector3 startPos = obj.position;
        Vector3 startScale = obj.localScale;
        Vector3 endScale = new Vector3(0.6f, 0.6f, 1f);

        float elapsed = 0f;
        Vector3 offset = new Vector3(0f, 0.2f, 0f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            obj.position = Vector3.Lerp(startPos, targetPos + offset, t);
            obj.localScale = Vector3.Lerp(startScale, endScale, t);

            yield return null;
        }

        obj.position = targetPos;
        obj.localScale = endScale;

        onFinished?.Invoke(obj.gameObject);
        AudioManager.FireSFX(AudioManager.SFXSignal.TrashClose);

        Destroy(obj.gameObject);
    }
}
