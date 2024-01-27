using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtingPlayer : MonoBehaviour
{
    public int Damage;
    bool canBeHurt = true;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && canBeHurt)
        {
            StartCoroutine(HurtPlayer());
        }
    }
    IEnumerator HurtPlayer()
    {
        canBeHurt = false;
        Lifemanager.Instance.CurrentLifes -= Damage;
        yield return new WaitForSeconds(GameManager.Instance.InvulnerabilityTime);
    }
}
