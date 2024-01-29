using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HUDPlayer : MonoBehaviour
{
    [SerializeField] Transform parentLifeTransform;
    [SerializeField] GameObject HearthSprite;
    [SerializeField] RectTransform GameOverPanel;
    [SerializeField] TMP_Text ScoreText;
    public List<GameObject> CurrentHearth = new List<GameObject>();

    [SerializeField] float timeToAppearHeath;
    [SerializeField] float delayBtwAppear;
    [SerializeField] LeanTweenType type;
    private void Update()
    {
        ScoreText.text = "Score: " + GameManager.Instance.Score.ToString("D8");
    }
    public IEnumerator StartHeartAnim()
    {
        float initialPosHearth = 50.00002f;
        yield return new WaitForSeconds(1.8f);
        for (int i = 0; i < Lifemanager.Instance.CurrentLifes; i++)
        {
            GameObject SH = Instantiate(HearthSprite, parentLifeTransform, false);
            SH.GetComponent<RectTransform>().anchoredPosition = new Vector3(-109, parentLifeTransform.GetComponent<RectTransform>().anchoredPosition.y);
            CurrentHearth.Add(SH);
            LeanTween.moveX(SH.GetComponent<RectTransform>(), initialPosHearth, timeToAppearHeath).setEase(type);
            initialPosHearth += HearthSprite.GetComponent<RectTransform>().rect.width;
            yield return new WaitForSeconds(delayBtwAppear);
        }
        ShowScore();
    }
    public void HideHearth()
    {

        if (Lifemanager.Instance.CurrentLifes == 0)
        {
            GameManager.Instance.State = GameState.GameOver;
            LeanTween.moveY(GameOverPanel, 272, 0.6f).setEaseOutBounce();
            return;
        }
        LeanTween.size(CurrentHearth[Lifemanager.Instance.CurrentLifes].GetComponent<RectTransform>(), Vector2.zero, 0.7f).setEaseInOutBounce();

        
    }
    void ShowScore()
    {
        LeanTween.moveX(ScoreText.rectTransform, 1777, timeToAppearHeath).setEaseOutBounce();
    }
}
