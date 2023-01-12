using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class FallDamage : MonoBehaviour
{
    [SerializeField]
    [Header("���C���΂��ꏊ")]
    private Transform _rayPos;

    [SerializeField]
    [Header("���C���΂�����")]
    private float _rayRange;

    [SerializeField]
    [Header("�ǂ̂��炢�̍�������_���[�W���󂯂邩")]
    private float _damageDistance;

    [SerializeField]
    [Header("�_���[�W��")]
    int _damage;

    /// <summary>
    /// Hp���Ǘ�����N���X
    /// </summary>
    private MyHp _myHp;

    /// <summary>
    /// �������Ă����Ԃ����肷��ϐ�
    /// </summary>
    private bool _isFall;

    /// <summary>
    /// ���݂�Y���̃|�W�V����
    /// </summary>
    private float _fallenPos;

    /// <summary>
    /// �n�ʂƂ̋�����}��ϐ�
    /// </summary>
    private float _fallenDisrance;

    void Start()
    {
        TryGetComponent(out _myHp);

        this.UpdateAsObservable().Subscribe(_ => FallingCheck());

        _fallenDisrance = 0.0f;
        _fallenPos = transform.position.y;
        _isFall = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IFloor floor))
        {
            _myHp.PlayerDamage(_damage);
        }
    }

    /// <summary>
    /// ���ݗ����Ă��邩�`�F�b�N����֐�
    /// ���̍������痎������_���[�W���󂯂�
    /// </summary>
    private void FallingCheck()
    {
        if (_isFall)
        {
            _fallenPos = Mathf.Max(_fallenPos, transform.position.y);

            if (Physics.Linecast(_rayPos.position, _rayPos.position + Vector3.down * _rayRange, LayerMask.GetMask("Field", "Block")))
            {
                _fallenDisrance = _fallenPos - transform.position.y;

                if (_fallenDisrance >= _damageDistance)
                {
                    _myHp.PlayerDamage(_damage);
                }

                _isFall = false;
            }
        }
        else
        {
            if (!Physics.Linecast(_rayPos.position, _rayPos.position + Vector3.down * _rayRange, LayerMask.GetMask("Field", "Block")))
            {
                _fallenPos = transform.position.y;
                _fallenDisrance = 0.0f;
                _isFall = true;
            }
        }
    }
}
