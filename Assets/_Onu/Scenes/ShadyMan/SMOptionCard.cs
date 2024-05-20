using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMOptionCard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var initial_pos = transform.position;
        transform.position = new Vector3(-50, initial_pos.y, initial_pos.z);
        transform.DOMoveX(initial_pos.x, 1.0f).SetEase(Ease.OutBack);
    }

    private void OnMouseEnter()
    {
        transform.localScale = (Vector3.one * 1.2f);
    }

    private void OnMouseExit()
    {
        transform.localScale = Vector3.one;
    }
}
