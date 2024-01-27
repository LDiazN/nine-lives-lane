using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    [SerializeField, Range(0,100)]
    private float _maxDistanceFromCenter = 10;

    [SerializeField]
    private float _turnSpeed = 20;

    [SerializeField]
    private GameObject _carBody;

    [SerializeField]
    private float _maxCarBodySpeed = 10;

    // Target position is a position in **local space**, it's where the car will try to go at any moment. 
    // It should be clamped between the both sides of the car
    Vector3 _targetPosition = Vector3.zero;

    private Vector3 _originalBodyPosition;

    private BoxCollider _carCollider;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(_carBody != null, "Car Body property in CarController is null. Did you forget to set up the the rigth game object?");
        _carCollider = _carBody.GetComponent<BoxCollider>();
        Debug.Assert(_carCollider != null, "Car Body should provide a collider");
        _originalBodyPosition = _carBody.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTargetPosition();

        MoveCarBody();
    }

    private void MoveCarBody()
    {
        Vector3 Velocity = _maxCarBodySpeed * (_targetPosition -  _carBody.transform.position);

        if ( Vector3.Distance(_carBody.transform.localPosition, _targetPosition) >= 0.1)
        {
            _carBody.transform.position += Velocity * Time.deltaTime;
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
            transform.position + Vector3.right * _maxDistanceFromCenter
            );

        Gizmos.DrawLine(
            transform.position,
            transform.position + Vector3.left * _maxDistanceFromCenter
            );

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1);

        // Draw left cube
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + Vector3.right * _maxDistanceFromCenter + Vector3.right * 1, new Vector3(2,2,2));
        Gizmos.DrawWireCube(transform.position + Vector3.left * _maxDistanceFromCenter + Vector3.left * 1, new Vector3(2,2,2));

        // Draw Target location
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + _targetPosition, 1);

        // Draw limits
        if (_carCollider != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(transform.position, new Vector3(_carCollider.bounds.size.x, 1, 1));
        }
    }
}
