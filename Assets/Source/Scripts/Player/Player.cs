using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
   [SerializeField] private BagHolder _bagHolder;

    private PlayerMovement _playerMovement;

    public BagHolder BagHolder => _bagHolder;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    public void Init(Inputer inputer)
    {
        _playerMovement.Init(inputer);
    }
}
