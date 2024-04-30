using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TreeEditor.TreeEditorHelper;

public class BathroomOptions : MonoBehaviour
{

    public List<BathroomOptionCard> options;
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
