using System.Collections.Generic;
using UnityEngine;
using System;

public class UnpackedResourcesChecker : MonoBehaviour
{
    private List<ResourceZone> _zones;

    public event Action TargetDetected;
    public event Action Cleared;

    private void Awake()
    {
        _zones = new List<ResourceZone>();
    }

    private void OnDisable()
    {
        foreach (ResourceZone zone in _zones)
        {
            zone.Cleared -= OnZoneDisabled;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ResourceZone zone))
        {
            _zones.Add(zone);
            zone.Cleared += OnZoneDisabled;

            if (_zones.Count > 0)
            {
                TargetDetected?.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ResourceZone zone))
        {
            OnZoneDisabled(zone);
        }
    }

    private void OnZoneDisabled(ResourceZone zone)
    {
        zone.Cleared -= OnZoneDisabled;
        _zones.Remove(zone);

        if (_zones.Count == 0)
        {
            Cleared?.Invoke();
        }
    }
}
