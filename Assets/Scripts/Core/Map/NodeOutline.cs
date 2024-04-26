using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NodeOutline : MonoBehaviour
{
    MapNode _node;
    SpriteRenderer _spriteRenderer;
    SpriteRenderer _backgroundRenderer;

    public bool clicked;
    public bool valid;

    void Start()
    {
        OnEnable(); // Initialize
        clicked = false;
    }

    void OnEnable()
    {
        _node = GetComponent<MapNode>();
        valid = _node.IsValid();

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _backgroundRenderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();

        UpdateColors();

        if (valid && !clicked) 
        {
            StartCoroutine(FlashEffect());
        }
    }

    void UpdateColors()
    {
        if (valid || clicked)
        {
            _spriteRenderer.color = Color.white;
            _backgroundRenderer.color = Color.white;
        }
        else
        {
            _spriteRenderer.color = new Color(0.8f, 0.8f, 0.8f, 1);
            _backgroundRenderer.color = new Color(0.8f, 0.8f, 0.8f, 1);
        }

    }
    IEnumerator ShakeEffect(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = originalPosition.x + UnityEngine.Random.Range(-1f, 1f) * magnitude;
            float y = originalPosition.y + UnityEngine.Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);
            elapsed += Time.deltaTime;

            yield return null; // Wait until next frame before continuing
        }

        transform.localPosition = originalPosition; // Reset to original position
    }

    IEnumerator FlashEffect()
    {
        while (valid && !clicked)
        {
            float flashDuration = 2.0f;
            float elapsedTime = 0f;
            while (elapsedTime < flashDuration)
            {
                float phase = (Mathf.PI * 2 * elapsedTime / flashDuration) - Mathf.PI / 2;
                float alpha = (Mathf.Sin(phase) + 1) / 2;  // Normalizes the sine wave to 0-1
                _backgroundRenderer.color = new Color(1, 1, 0, alpha);  // Fading between transparent and yellow
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }

    void OnMouseEnter()
    {
        if (valid)
        {
            gameObject.transform.localScale = new Vector3(4.5f, 4.5f);
            _backgroundRenderer.color = Color.yellow;
        }
    }

    void OnMouseExit()
    {
        if (!clicked && valid)
        {
            gameObject.transform.localScale = new Vector3(4, 4);
            UpdateColors();  // Reset colors based on validity and clicked status
        }
    }

    void OnMouseDown()
    {
        if (valid)
        {
            _backgroundRenderer.color = Color.white;
            _spriteRenderer.color = Color.white;
            clicked = true;
            StartCoroutine(ShakeEffect(0.25f, 0.05f));
        }
    }
}
