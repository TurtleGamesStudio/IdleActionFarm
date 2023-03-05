using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private InputerConstantCanvas _inputerConstantCanvas;
    [SerializeField] private Inputer _inputer;
    [SerializeField] private Player _player;

    private void Start()
    {
        _inputerConstantCanvas.Init(_inputer);
        _player.Init(_inputer);
    }
}
