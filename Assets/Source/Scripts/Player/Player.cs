using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] private BagHolder _bagHolder;

    private PlayerMovement _playerMovement;

    public BagHolder BagHolder => _bagHolder;

    public void Init(Inputer inputer)
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.Init(inputer);
    }
}
