using UnityEngine;

//��� ����� ��������� ��������������� ������� - ����������� ����������
//������������ ������ ������
//[RequireComponent(typeof(TweenerMover))]
public class Ring : MonoBehaviour
{
    private Transform _target;//another variant previous Ring
    //private TweenerMover _tweenerMover;
    private WaveMover _waveMover;
    private LocalMoverX _localMover;

    public void Init(Transform target)
    {
        _target = target;
        //_tweenerMover = GetComponent<TweenerMover>();
        _waveMover = GetComponent<WaveMover>();
        _localMover = GetComponent<LocalMoverX>();
    }

    public void Move()
    {
        _localMover.StopMoving();
        _waveMover.Move();
    }

    //public void Move(float requireTime)
    //{
    //    _tweenerMover.Move(_target.position, requireTime);
    //}

    public void StopMoving()
    {
        _waveMover.StopMoving();
        //�������� ����������� � �������� ���������
        _localMover.Move(0);
    }
}
