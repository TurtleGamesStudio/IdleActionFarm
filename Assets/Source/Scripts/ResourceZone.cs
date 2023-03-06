using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class ResourceZone : MonoBehaviour
{
    [SerializeField] private List<UnpackedResource> _cutAvailableResources;

    private Collider _collider;

    public event Action<ResourceZone> Cleared;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        foreach (UnpackedResource resource in _cutAvailableResources)
        {
            resource.Packed += OnPacked;
            resource.Growed += OnGrowed;
        }
    }

    private void OnDisable()
    {
        foreach (UnpackedResource resource in _cutAvailableResources)
        {
            resource.Packed -= OnPacked;
            resource.Growed -= OnGrowed;
        }
    }

    private void OnPacked()
    {
        bool isZoneAvalable = false;

        foreach (UnpackedResource resource in _cutAvailableResources)
        {
            if (resource.IsAvalable)
            {
                isZoneAvalable = true;
                break;
            }
        }

        _collider.enabled = isZoneAvalable;

        if (isZoneAvalable == false)
            Cleared?.Invoke(this);
    }

    private void OnGrowed()
    {
        _collider.enabled = true;
    }
}
