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

    private float _turnAnimationStatus = 0;

    [SerializeField]
    private float _turnAnimationSpeed = 2;

    [SerializeField]
    private float _turnAnimationMaxDegrees = 30;

    [SerializeField]
    private GameObject TireSmokeLeft;

    [SerializeField]
    private GameObject TireSmokeRight;

    [SerializeField]
    private float _timeToWaitBetweenTireSmokes = 0.5f;

    private Quaternion _originalRotation;
    // Target position is a position in **local space**, it's where the car will try to go at any moment. 
    // It should be clamped between the both sides of the car
    Vector3 _targetPosition = Vector3.zero;

    private BoxCollider _carCollider;

    private float _timeSinceLastSmoke = 0;

    // Start is called before the first frame update
    void Start()
    {
        _originalRotation = _carBody.transform.rotation;

        Debug.Assert(_carBody != null, "Car Body property in CarController is null. Did you forget to set up the the rigth game object?");
        _carCollider = _carBody.GetComponent<BoxCollider>();
        Debug.Assert(_carCollider != null, "Car Body should provide a collider");
    }

    // Update is called once per frame
    void Update()
    {

        PlaySmokeIfNecessary();

        UpdateTargetPosition();

        MoveCarBody();

        UpdateTurnAnimation();

        // DEBUG: DELETE LATER
        if (Input.GetKeyDown(KeyCode.C))
        {
            Boink();
        }

        _timeSinceLastSmoke += Time.deltaTime;
    }

    bool ShouldPlayTireSmoke()
    {
        bool justStartedToMove = Mathf.Abs(_turnAnimationStatus) < 0.1;
        bool playedAWhileAgo = _timeSinceLastSmoke > _timeToWaitBetweenTireSmokes;
        bool hasUserInput = ShouldMoveLeft() || ShouldMoveRight();


        return justStartedToMove && playedAWhileAgo && hasUserInput;
    }

    void PlaySmokeIfNecessary()
    {
        if (!ShouldPlayTireSmoke())
            return;

        bool isLeft = !ShouldMoveLeft();
        PlaySmoke(isLeft);
    }

    private void PlaySmoke(bool isLeft)
    {

        GameObject tireSmoke = isLeft ? TireSmokeLeft : TireSmokeRight;
        ParticleSystem particles = tireSmoke.GetComponent<ParticleSystem>();

        particles.Play();
        _timeSinceLastSmoke = 0.0f;
    }

    private void UpdateTurnAnimation()
    {
        if (ShouldMoveRight())
        {
            _turnAnimationStatus += Time.deltaTime * _turnAnimationSpeed;
        }
        
        if (ShouldMoveLeft())
        {
            _turnAnimationStatus -= Time.deltaTime * _turnAnimationSpeed;
        }

        if (!ShouldMoveLeft() && !ShouldMoveRight())
        {
            _turnAnimationStatus -= Mathf.Sign(_turnAnimationStatus) * 
                Mathf.Min(
                    Time.deltaTime, 
                    Mathf.Abs(_turnAnimationStatus)
            );

            if (Mathf.Approximately(0, _turnAnimationStatus))
            {
                _turnAnimationStatus = 0;
                ResetRotation();
            }
        }

        _turnAnimationStatus = Mathf.Clamp(_turnAnimationStatus, -1, 1);
        _carBody.transform.eulerAngles = new Vector3(
            _carBody.transform.rotation.eulerAngles.x,
            _originalRotation.eulerAngles.y + _turnAnimationStatus * _turnAnimationMaxDegrees,
            _carBody.transform.rotation.eulerAngles.z
        );

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

    private void ResetRotation()
    {
        _carBody.transform.rotation = _originalRotation;
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
        Gizmos.DrawWireSphere(transform.position, 1);

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
