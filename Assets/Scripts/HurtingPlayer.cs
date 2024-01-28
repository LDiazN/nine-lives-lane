using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;

public class HurtingPlayer : MonoBehaviour
{
    public int Damage;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Lifemanager.Instance.TryHurt(Damage);
        }
    }

}
