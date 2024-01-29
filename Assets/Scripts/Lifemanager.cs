using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Lifemanager : MonoBehaviour
{
    public int CurrentLifes = 7;
    public static Lifemanager Instance;
    private bool _canBeHurt = true;

    private AudioSource audioSource;
    [SerializeField] float TimeBetweenHits = 2; // secondss
    [SerializeField] HUDPlayer hud;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        if(Instance == null) 
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TryHurt(int Damage = 1)
    {
        if (!_canBeHurt)
            return;

        CurrentLifes -= Damage;
        Debug.Log($"Player Hurt! You have {CurrentLifes}");
        hud.HideHearth();
        var carController = GameManager.Instance.Player.GetComponent<CarController>();
        carController.Boink();
        StartCoroutine(HurtTimer());
        audioSource.Play();
    }

    IEnumerator HurtTimer()
    {
        _canBeHurt = false;
        yield return new WaitForSeconds(TimeBetweenHits);
        _canBeHurt = true;

    }
}
