using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FlyAway : MonoBehaviour
{
    [SerializeField] float forceForward = 10.0f;
    [SerializeField] float forceUp = 10.0f;
    [SerializeField] float timeToDestoy = 5;
    [SerializeField] AudioClip _crashAudio;

    private AudioSource _audioSource;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
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
                _audioSource.clip = _crashAudio;
                _audioSource.Play();
                Destroy(gameObject, timeToDestoy);
            }
        }
    }
}
