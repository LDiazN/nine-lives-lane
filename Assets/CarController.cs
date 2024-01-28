using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    [SerializeField, Range(0, 100)]
    private float _maxDistanceFromCenter = 10;

    [SerializeField]
    private float _turnSpeed = 20;

    [SerializeField]
    private GameObject _carBody;
    public GameObject CarBody { get { return _carBody; } }

    [SerializeField]
    private float _maxCarBodySpeed = 10;

    [SerializeField]
    private float _boinkTime = 0.5f; // in seconds

    [SerializeField]
    private float _maxBoinkHeight = 100;

    // Target position is a position in **local space**, it's where the car will try to go at any moment. 
    // It should be clamped between the both sides of the car
    Vector3 _targetPosition = Vector3.zero;

    private BoxCollider _carCollider;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(_carBody != null, "Car Body property in CarController is null. Did you forget to set up the the rigth game object?");
        _carCollider = _carBody.GetComponent<BoxCollider>();
        Debug.Assert(_carCollider != null, "Car Body should provide a collider");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTargetPosition();

        MoveCarBody();

        // DEBUG: DELETE LATER
        if (Input.GetKeyDown(KeyCode.C))
        {
            Boink();
        }
    }

    private void MoveCarBody()
    {
        Vector3 Velocity = _maxCarBodySpeed * (_targetPosition - _carBody.transform.localPosition);

        if (Vector3.Distance(_carBody.transform.localPosition, _targetPosition) >= 0.1)
        {
            _carBody.transform.localPosition += Velocity * Time.deltaTime;
        }
    }

    private bool ShouldMoveRight()
    {
        return Input.GetKey(KeyCode.D);
    }

    private bool ShouldMoveLeft()
    {
        return Input.GetKey(KeyCode.A);
    }

    private void UpdateTargetPosition()
    {
        if (ShouldMoveRight())
        {
            _targetPosition += Vector3.right * Time.deltaTime * _turnSpeed;
        }

        if (ShouldMoveLeft())
        {
            _targetPosition += Vector3.left * Time.deltaTime * _turnSpeed;
        }

        // Check for max distance
        float BodyExtentsLateral = _carCollider.bounds.size.x / 2;
        float size = _targetPosition.magnitude;

        if (size > (_maxDistanceFromCenter - BodyExtentsLateral))
        {
            _targetPosition = _targetPosition.normalized * (_maxDistanceFromCenter - BodyExtentsLateral);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(
            transform.position,
            transform.TransformPoint(Vector3.right * _maxDistanceFromCenter)
            );

        Gizmos.DrawLine(
            transform.position,
            transform.TransformPoint(Vector3.left * _maxDistanceFromCenter)
            );

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1);

        Gizmos.color = Color.blue;
        // Draw left cube
        Gizmos.DrawWireCube(transform.TransformPoint(Vector3.right * _maxDistanceFromCenter + Vector3.right), new Vector3(2, 2, 2));
        // Draw Right cube
        Gizmos.DrawWireCube(transform.TransformPoint(Vector3.left * _maxDistanceFromCenter + Vector3.left), new Vector3(2, 2, 2));

        // Draw Target location
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.TransformPoint(_targetPosition), 1);

        // Draw limits
        if (_carCollider != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(_carBody.transform.position, new Vector3(_carCollider.bounds.size.x, 1, 1));
        }
    }

    public void Boink()
    {
        StartCoroutine(StartBoink());
    }

    IEnumerator StartBoink()
    {
        float timePassed = 0;


        while (timePassed < _boinkTime)
        {
            timePassed += Time.deltaTime;
            float t = timePassed / _boinkTime;

            _targetPosition.y -= Mathf.Cos(t * Mathf.Deg2Rad * 180) * _maxBoinkHeight;

            yield return new WaitForFixedUpdate();
        }

        _targetPosition.y = 0;
    }
}
