using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObjects : MonoBehaviour
{
    [SerializeField] float spinForce;
    bool canRotate = false;
    Vector3 directionToImpulse;

    private void Update()
    {
        if (canRotate)
        {
            transform.position += directionToImpulse * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0, transform.eulerAngles.y + spinForce * Time.deltaTime, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canRotate = true;
            directionToImpulse = (transform.position - GameManager.Instance.Player.transform.position).normalized * 10;
        }
    }
}
