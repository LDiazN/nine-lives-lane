using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifemanager : MonoBehaviour
{
    public int CurrentLifes = 7;
    public static Lifemanager Instance;
    private bool _canBeHurt = true;
    [SerializeField] float TimeBetweenHits = 2; // secondss


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
        var carController = GameManager.Instance.Player.GetComponent<CarController>();
        carController.Boink();
        StartCoroutine(HurtTimer());
    }

    IEnumerator HurtTimer()
    {
        _canBeHurt = false;
        yield return new WaitForSeconds(TimeBetweenHits);
        _canBeHurt = true;

    }
}
