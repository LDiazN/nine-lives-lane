using UnityEngine;

public class ExplosiveObjects : MonoBehaviour
{
    [SerializeField] GameObject explosiveObject;
    [HideInInspector] public bool canExplote;

    private void Start()
    {
        if (explosiveObject.activeInHierarchy)
        {
            explosiveObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (canExplote && !collision.gameObject.CompareTag("Road"))
        {
            Instantiate(explosiveObject, transform.position, Quaternion.identity);
            GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject);
        }
    }
}