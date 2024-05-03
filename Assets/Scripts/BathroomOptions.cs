using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TreeEditor.TreeEditorHelper;

public class BathroomOptions : MonoBehaviour
{

    public List<GameObject> prefabs;
    private List<BathroomOptionCard> options = new List<BathroomOptionCard>();

    public void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            int idx = UnityEngine.Random.Range(0, prefabs.Count);
            GameObject spawned = GameObject.Instantiate(prefabs[idx]);
            spawned.transform.parent = transform;
            options.Add(spawned.GetComponent<BathroomOptionCard>());
            spawned.transform.localPosition = new Vector3(200*i - 200,0,0);
        }
    }
    // Start is called before the first frame update
    public void OptionPicked(GameObject go)
    {
        StartCoroutine(OptionPickedAnimations(go));
    }

    IEnumerator OptionPickedAnimations(GameObject go)
    {
        BathroomOptionCard c = go.GetComponent<BathroomOptionCard>();
        foreach (var o in options)
        {
            if (o != c)
            {
                o.transform.DOLocalMoveY(-500, 0.5f).SetEase(Ease.InBack);
                
            } else
            {
                o.transform.DOScale(Vector3.one * 1.5f, 0.3f).SetDelay(0.05f);
            }
        }
        yield return new WaitForSeconds(1.0f);
        OnuSceneManager.GetInstance().ChangeScene(Scene.Map);

    }
}
