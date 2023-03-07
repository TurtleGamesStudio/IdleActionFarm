using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMoverTest : MonoBehaviour
{
    [SerializeField] private WaveMover _waveMover;

    private void Start()
    {
        _waveMover.Move();
    }
}
