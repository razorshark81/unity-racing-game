using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonEnlarge : MonoBehaviour
{
    private float scale = 1f;
    private Coroutine enlargeCoroutine;

    public void PointerEnterCall()
    {
        if (enlargeCoroutine != null)
        {
            StopCoroutine(enlargeCoroutine);
        }

        enlargeCoroutine = StartCoroutine(PointerEnter());
    }

    public void PointerExitCall()
    {
        if (enlargeCoroutine != null)
        {
            StopCoroutine(enlargeCoroutine);
        }

        enlargeCoroutine = StartCoroutine(PointerExit());
    }

    IEnumerator PointerExit()
    {
        while (scale >= 0.95f)
        {
            
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.localScale = new Vector3 (scale, scale, 1);
            rectTransform.position = Vector3.Lerp(new Vector3(rectTransform.position.x,rectTransform.position.y,0),new Vector3(rectTransform.position.x -75, rectTransform.position.y - 30, 0),0.1f);
            rectTransform.SetAsFirstSibling();
            scale -= 0.05f;
            yield return new WaitForSeconds(0.0001f);
        }
    }

    IEnumerator PointerEnter()
    {
        while (scale < 1.3f)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.localScale = new Vector3(scale, scale, 1);
            rectTransform.position = Vector3.Lerp(new Vector3(rectTransform.position.x, rectTransform.position.y, 0), new Vector3(rectTransform.position.x + 75, rectTransform.position.y + 30,0), 0.1f);
            rectTransform.SetAsLastSibling();
            
            scale += 0.05f;
            yield return new WaitForSeconds(0.0001f);
        }

    }
}
