using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyAway : MonoBehaviour
{
    [SerializeField] float forceForward = 10.0f;
    [SerializeField] float forceUp = 10.0f;
    [SerializeField] float timeToDestoy = 5;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (rb != null)
            {
                CarController p = other.gameObject.GetComponent<CarController>();
                Vector3 directionToImpulse = (transform.position - GameManager.Instance.Player.transform.position).normalized * forceForward;
                rb.AddForce(directionToImpulse + Vector3.up * forceUp, ForceMode.Impulse);
                GetComponent<ExplosiveObjects>().canExplote = true;
                Destroy(gameObject, timeToDestoy);
            }
        }
    }
}
