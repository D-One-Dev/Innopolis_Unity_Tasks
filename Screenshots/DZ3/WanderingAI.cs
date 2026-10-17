using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float obstacleRange = 5.0f;
    [SerializeField] private float obstacleRadius = 0.75f;
    [SerializeField] private float turnTime = 0.5f;
    [SerializeField] private float maxTurnAngle = 110f;
    [SerializeField] private float wallStopRadius = 0.4f;
    [SerializeField] private float wallStopGap = 0.2f;

    [Header("Shooting")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private float fireInterval = 1.5f;
    [SerializeField] private float sightRange = 20f;
    [SerializeField] private float sightRadius = 0.25f;

    private bool _isAlive = true;
    private bool _isMoving = true;
    private bool _isTurning;
    private float _yaw;
    private float _targetYaw;
    private float _turnVelocity;
    private Rigidbody _rb;
    private float _nextFireTime;
    private PlayerCharacter _player;

    private void Awake()
    {
        _yaw = transform.eulerAngles.y;
        _targetYaw = _yaw;

        _rb = GetComponent<Rigidbody>();
        if (_rb == null) _rb = gameObject.AddComponent<Rigidbody>();
        _rb.isKinematic = true;
        _rb.useGravity = false;
        _rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
    }

    private void Start()
    {
        if (fireballPrefab == null)
        {
            Debug.LogWarning("WanderingAI: fireballPrefab is not assigned, the enemy will not shoot.", this);
        }
    }

    public void SetAlive(bool alive)
    {
        _isAlive = alive;
    }

    private void Update()
    {
        if (!_isAlive) return;

        if (PlayerInSight())
        {
            _isMoving = false;
            FaceTowards(_player.transform.position);
            ShootFireball();
        }
        else
        {
            _isMoving = true;
            if (!_isTurning) _yaw = transform.eulerAngles.y;
            AvoidObstacles();
        }

        UpdateTurning();
    }

    private void FixedUpdate()
    {
        if (!_isAlive) return;

        _rb.MoveRotation(Quaternion.Euler(0f, _yaw, 0f));

        if (_isMoving && !BlockedByWall()) _rb.MovePosition(_rb.position + transform.forward * (speed * Time.fixedDeltaTime));
    }

    private void UpdateTurning()
    {
        if (!_isTurning) return;

        _yaw = Mathf.SmoothDampAngle(_yaw, _targetYaw, ref _turnVelocity, turnTime);
        if (Mathf.Abs(Mathf.DeltaAngle(_yaw, _targetYaw)) < 0.5f)
        {
            _yaw = _targetYaw;
            _turnVelocity = 0f;
            _isTurning = false;
        }
    }

    private void FaceTowards(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0f;
        if (direction.sqrMagnitude < 0.001f) return;

        float desiredYaw = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        if (Mathf.Abs(Mathf.DeltaAngle(_yaw, desiredYaw)) > 0.5f)
        {
            _targetYaw = desiredYaw;
            _isTurning = true;
        }
    }

    private void AvoidObstacles()
    {
        if (_isTurning) return;

        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.SphereCast(ray, obstacleRadius, out RaycastHit hit, obstacleRange))
        {
            _targetYaw = _yaw + Random.Range(-maxTurnAngle, maxTurnAngle);
            _isTurning = true;
        }
    }

    private bool BlockedByWall()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        return Physics.SphereCast(ray, wallStopRadius, out _, wallStopGap);
    }

    private bool PlayerInSight()
    {
        if (_player == null) _player = FindFirstObjectByType<PlayerCharacter>();
        if (_player == null) return false;

        Vector3 origin = transform.position;
        Vector3 toPlayer = _player.transform.position - origin;
        float distance = toPlayer.magnitude;
        if (distance < 0.001f) return true;
        if (distance > sightRange) return false;

        if (!Physics.SphereCast(new Ray(origin, toPlayer / distance), sightRadius, out RaycastHit hit, distance)) return false;

        return hit.transform.gameObject.GetComponentInParent<PlayerCharacter>() != null;
    }

    private void ShootFireball()
    {
        if (fireballPrefab == null) return;
        
        if (Time.time < _nextFireTime)
            return;

        GameObject fireball = Instantiate(fireballPrefab);
        Vector3 spawnPosition = transform.TransformPoint(Vector3.forward * 1.5f);
        fireball.transform.position = spawnPosition;
        
        Vector3 aimDirection = _player.transform.position - spawnPosition;
        fireball.transform.rotation = aimDirection.sqrMagnitude > 0.001f ? Quaternion.LookRotation(aimDirection) : transform.rotation;

        _nextFireTime = Time.time + fireInterval;
    }
}