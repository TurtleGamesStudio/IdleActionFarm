using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 10f;
    [SerializeField] private float _rotationSpeed = 1000f;

    private Inputer _inputer;
    private NavMeshAgent _agent;
    private Coroutine _transformUpdating;
    private Transform _cameraTransform;

    //public event Action<float> SpeedChanged;
    public event Action<float> InputerShareChanged;

    public event Action Stopped;
    public event Action Moved;

    public Inputer Inputer => _inputer;

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnDisable()
    {
        _inputer.TouchStarted -= OnTouchStarted;
        _inputer.TouchFinished -= OnTouchFinished;

        if (_transformUpdating != null)
        {
            StopCoroutine(_transformUpdating);
        }
    }

    public void Init(Inputer inputer)
    {
        _inputer = inputer;
        Subscribe();
    }

    public void Subscribe()
    {
        _inputer.TouchStarted += OnTouchStarted;
        _inputer.TouchFinished += OnTouchFinished;
    }

    private void OnTouchStarted()
    {
        if (_transformUpdating != null)
        {
            StopCoroutine(_transformUpdating);
        }

        _transformUpdating = StartCoroutine(TransformUpdating());
    }

    private IEnumerator TransformUpdating()
    {
        Moved?.Invoke();

        while (true)
        {
            UpdateTransform();
            yield return null;
        }
    }

    private void OnTouchFinished()
    {
        if (_transformUpdating != null)
        {
            StopCoroutine(_transformUpdating);
        }

        if (_cameraTransform)
        {
            UpdateTransform();
        }

        Stopped?.Invoke();
    }

    private void UpdateTransform()
    {
        Vector3 direction = Quaternion.AngleAxis(_cameraTransform.eulerAngles.y, Vector3.up) * RotateTo90degres(_inputer.Direction);
        Rotate(direction);
        Move(direction);
    }

    private void Move(Vector3 direction)
    {
        float speed = _maxSpeed * _inputer.Share;
        _agent.velocity = speed * direction;

        InputerShareChanged?.Invoke(_inputer.Share);
    }

    private void Rotate(Vector3 direction)
    {
        Quaternion targetQuaternion = Quaternion.FromToRotation(Vector3.forward, direction);
        float targetAngle = Mathf.Sign(direction.x) * Quaternion.Angle(Quaternion.identity, targetQuaternion);
        float angle = Mathf.MoveTowardsAngle(transform.localEulerAngles.y, targetAngle, _rotationSpeed * Time.deltaTime);
        transform.localEulerAngles = new Vector3(0f, angle, 0f);
    }

    private Vector3 RotateTo90degres(Vector3 vector)
    {
        return new Vector3(vector.x, vector.z, vector.y);
    }
}
