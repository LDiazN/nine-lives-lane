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
    public LeanTweenType TitleAnim;
    [SerializeField] RectTransform Title;
    //public GameObject textObject;

    // Start is called before the first frame update
    void Start()
    {
        //LeanTween.moveY(textObject.GetComponent<RectTransform>(), -250, 2).setEase(LeanTweenType.easeInQuad);
        StartCoroutine(AnimateButtons(-600));
        LeanTween.moveY(Title, 434, 0.8f).setEaseInBounce();
    }
    public void quit()
    {
        Application.Quit();
    }
    public void ChangeToGame()
    {
        GameManager.Instance.State = GameState.InGame;
        LeanTween.moveY(Title, 887, 0.8f).setEaseInBounce();
        StartCoroutine(AnimateButtons(-1280));
    }
    //IEnumerato OutButtons()
    IEnumerator AnimateButtons(int i)
    {
        foreach (Button button in buttons)
        {
            LeanTween.moveX(button.GetComponent<RectTransform>(), i, 2).setEase(animationbutton);
            yield return new WaitForSeconds(delay);
        }
    }
}
