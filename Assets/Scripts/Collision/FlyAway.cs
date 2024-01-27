using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyAway : MonoBehaviour
{
    [SerializeField] float forceForward = 10.0f;
    [SerializeField] float forceUp = 10.0f;
    [SerializeField] float timeToDestoy;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            //Move m = other.GetComponent<Move>();
            if (rb != null)
            {
                //Lifemanager.Instance.LifeBehaviour(m.damageToPlayer);
                Vector3 directionToImpulse = (transform.position - GameManager.Instance.Player.transform.position).normalized * forceForward;
                rb.AddForce(directionToImpulse + Vector3.up * forceUp, ForceMode.Impulse);
                Destroy(gameObject, timeToDestoy);
            }
        }
    }
}
