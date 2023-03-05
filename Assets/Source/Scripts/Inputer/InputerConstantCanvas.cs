using UnityEngine;

public class InputerConstantCanvas : MonoBehaviour
{
    [SerializeField] private WheelOfInput _wheelOfInput;
    [SerializeField] private CircleOfInput _circleOfInput;

    public void Init(Inputer inputer)
    {
        _wheelOfInput.Init(inputer);
        _circleOfInput.Init(inputer);
    }

    public void ActivateJoystick()
    {
        _wheelOfInput.gameObject.SetActive(true);
    }
}
