using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceProducer : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private PackedResource _packedResourceTemplate;

    public event Action Produced;

    public void Produce()
    {
        PackedResource resource = Instantiate(_packedResourceTemplate, _spawnPoint.position, _spawnPoint.rotation);
        Produced?.Invoke();
    }
}
