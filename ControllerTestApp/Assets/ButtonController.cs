using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    public KeyCode ButtonKeyCode;

    public float DeltaAlpha;
    private float CurrentAlpha;

    private void Start()
    {
        CurrentAlpha = GetComponent<Image>().color.a;
    }

    public void DestroyButton()
    {
        StartCoroutine(StartDestroyAnimation());
    }

    IEnumerator StartDestroyAnimation()
    {
        while(GetComponent<Image>().color.a > 0)
        {
            Color temp = GetComponent<Image>().color;
            temp.a -= DeltaAlpha * Time.deltaTime;
            GetComponent<Image>().color = temp;
            GetComponent<RectTransform>().localScale += Vector3.up * Time.deltaTime * DeltaAlpha * 2;
            GetComponent<RectTransform>().localScale += Vector3.right * Time.deltaTime * DeltaAlpha * 2;

            yield return new WaitForEndOfFrame();
        }

        Debug.Log("Button should be destroyed.");
        Destroy(this.gameObject);
        

    }

}
