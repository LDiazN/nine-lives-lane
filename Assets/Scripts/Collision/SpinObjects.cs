using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpinObjects : MonoBehaviour
{
    [SerializeField] float spinForce;
    [SerializeField] float timeToDestoy;
    [SerializeField] float speed;
    [SerializeField] AudioClip crashSound;

    bool canRotate = false;
    Vector3 directionToImpulse;
    Rigidbody rb;

    private AudioSource _audioSource;
    private void Start()
    { 
        rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
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

            if (crashSound != null)
            {
                _audioSource.clip = crashSound;
                _audioSource.Play();
            }
        }
    }
}
