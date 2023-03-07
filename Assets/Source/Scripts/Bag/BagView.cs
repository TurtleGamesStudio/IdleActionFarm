using System.Collections.Generic;
using UnityEngine;

public class BagView : MonoBehaviour, IBagView
{
    [SerializeField] private float _requierTime;
    [SerializeField] private Transform _holder;
    [SerializeField] private Transform _firstPoint;
    [SerializeField] private Ring _template;

    private List<Ring> _rings;
    private List<Transform> _places;
    private Vector3 _previousHolderPosition;
    private Vector3 _previousHolderDirection;

    public IReadOnlyList<Transform> Places => _places;

    public void Init(int capacity)
    {
        _rings = new List<Ring>();
        _places = new List<Transform>();
        //Transform previousTransform = _firstPoint;

        for (int i = 0; i < capacity; i++)
        {
            Vector3 spawnPosition = GetPosition(i);



            GameObject newGameObject = new GameObject();
            newGameObject.transform.position = spawnPosition;
            newGameObject.transform.rotation = Quaternion.identity;
            newGameObject.transform.parent = _holder;
            Transform IdleTransform = newGameObject.transform;




            Ring ring = Instantiate(_template, spawnPosition, Quaternion.identity);//, transform);
            ring.Init(IdleTransform);
            _rings.Add(ring);
            _places.Add(ring.transform);


        }
    }

    //It will not work in future. Player must placed in Vector3.zero
    //Increase capacity unavailable
    private Vector3 GetPosition(int index)
    {
        return _firstPoint.position + Vector3.up * index;
    }

    public void Update()
    {
        if (_previousHolderPosition != _holder.position || _previousHolderDirection != _holder.forward)
        {
            UpdatePosition();
            ChangePositionSmoothly();
            _previousHolderPosition = _holder.position;
        }
    }

    private void UpdatePosition()
    {
        Quaternion rotation = Quaternion.FromToRotation(_previousHolderDirection, _holder.forward);
        _previousHolderDirection = _holder.forward;

        foreach (Ring ring in _rings)
        {
            Vector3 offset = ring.transform.position - _holder.position;
            ring.transform.position = _holder.position + rotation * offset;
            ring.transform.rotation = _holder.rotation;//or set anoter option
        }
    }

    private void ChangePositionSmoothly()
    {
        for (int i = 0; i < _rings.Count; i++)
        {
            Ring ring = _rings[i];
            float time = _requierTime + _requierTime * i;
            //ring.Move(time);
        }
    }
}
