using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObjects : MonoBehaviour
{
    [SerializeField] float spinForce;
    [SerializeField] float timeToDestoy;
    [SerializeField] float speed;
    bool canRotate = false;
    Vector3 directionToImpulse;
    Rigidbody rb;
    private void Start()
    { 
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (canRotate)
        {
            transform.position += directionToImpulse * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0, transform.eulerAngles.y + spinForce * Time.deltaTime, 0);
            rb.velocity = directionToImpulse * speed * Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canRotate = true;
            rb.constraints = RigidbodyConstraints.None;
            directionToImpulse = (transform.position - GameManager.Instance.Player.transform.position).normalized;
            GetComponent<ExplosiveObjects>().canExplote = true;
        }
    }
}
