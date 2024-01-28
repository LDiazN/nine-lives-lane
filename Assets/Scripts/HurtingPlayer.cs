using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtingPlayer : MonoBehaviour
{
    public int Damage;
    bool canBeHurt = true;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canBeHurt)
        {
            canBeHurt = false;
            Lifemanager.Instance.CurrentLifes -= Damage;
        }
    }

}
