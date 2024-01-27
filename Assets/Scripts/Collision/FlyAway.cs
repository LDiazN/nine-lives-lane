using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyAway : MonoBehaviour
{
    [SerializeField] float force = 10.0f;
    [SerializeField] float timeToDestoy;
    Rigidbody rb;
    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
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
                Vector3 directionToImpulse = (  transform.position - player.transform.position).normalized * force;
                rb.AddForce(directionToImpulse + Vector3.up * force / 2, ForceMode.Impulse);
                Destroy(gameObject, timeToDestoy);
            }
        }
    }
}
