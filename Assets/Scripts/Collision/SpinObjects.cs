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
            // transform.position += directionToImpulse * Time.deltaTime;
            transform.eulerAngles = new Vector3(
                transform.rotation.eulerAngles.x, 
                transform.rotation.eulerAngles.y + Time.deltaTime * spinForce, 
                transform.rotation.eulerAngles.z
               );
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canRotate = true;
            rb.constraints = RigidbodyConstraints.None;
            var player = GameManager.Instance.Player;
            directionToImpulse = (transform.position - player.transform.position).normalized;
            directionToImpulse += Vector3.up * 0.5f;
            rb.velocity = directionToImpulse * speed;

            GetComponent<ExplosiveObjects>().canExplote = true;
        }
    }
}
