using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuAnim : MonoBehaviour
{
    public Button[] buttons; 
    public float delay = 0.2f;
    public LeanTweenType animationbutton;
    public GameObject textObject;

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.rotateX(textObject.GetComponent<RectTransform>().gameObject, -180, 2).setEase(LeanTweenType.easeInQuad);
        StartCoroutine(AnimateButtons());
    }

    IEnumerator AnimateButtons() 
    {
        foreach (Button button in buttons)
        { 
            LeanTween.moveX(button.GetComponent<RectTransform>(), 5, 2).setEase(animationbutton);
            yield return new WaitForSeconds(delay);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
