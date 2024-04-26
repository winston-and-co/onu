using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null; // Wait until the next frame before continuing the loop
        }

        transform.localPosition = originalPos; // Reset the camera position
    }

    private void Awake()
    {
        BattleEventBus.getInstance().entityDamageEvent.AddListener(OnDamage);
    }

    void OnDamage(Entity e, int dmg)
    {
        StartCoroutine(Shake(0.4f, 0.25f)); // TODO: Adjust based on damage
    }


}