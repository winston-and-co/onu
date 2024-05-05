using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private GameObject[] parts;
    private int index;

    public void Start()
    {
        parts = new GameObject[gameObject.transform.childCount];
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            parts[i] = gameObject.transform.GetChild(i).gameObject;
        }
    }

    public void Advance()
    {
        parts[index].SetActive(false);
        index++;
        if (index < parts.Length)
        {
            parts[index].SetActive(true);
        }
    }
}
