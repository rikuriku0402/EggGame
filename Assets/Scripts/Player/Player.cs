using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Player : MonoBehaviour
{
    [SerializeField]
    [Header("�ړ��X�s�[�h")]
    private float _speed = 5f;

    [SerializeField]
    [Header("�W�����v�p���[")]
    private float _jumpPower;

    /// <summary>
    /// ����
    /// </summary>
    private Rigidbody _rb;

    /// <summary>
    /// �c���̔�����Ƃ�ϐ�
    /// </summary>
    private float _vertical, _horizontal;

    /// <summary>
    /// ���W�����v�������肷��ϐ�
    /// </summary>
    private bool _isJump;

    void Start()
    {
        TryGetComponent(out _rb);

        this.UpdateAsObservable().Subscribe(_ => GetKey());
        this.UpdateAsObservable().Subscribe(_ => Jump());
        this.FixedUpdateAsObservable().Subscribe(_ => Move());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IJump jump))
        {
            jump.Jump(IsJumpBool(false));
        }
    }

    /// <summary>
    /// ��ɃL�[���X�V����֐�
    /// </summary>
    private void GetKey()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
    }

    /// <summary>
    /// �v���C���[�̈ړ��֐�
    /// </summary>
    private void Move()
    {
        Vector3 cameraFroward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1f, 0f, 1f)).normalized;

        Vector3 moveForward = cameraFroward * _vertical + Camera.main.transform.right * _horizontal;

        _rb.velocity = moveForward * _speed + new Vector3(0f, _rb.velocity.y, 0f);
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    /// <summary>
    /// �W�����v�֐�
    /// </summary>
    private void Jump()
    {
        if (!_isJump && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.velocity = new Vector3(0.0f, _jumpPower, 0.0f);
            _isJump = true;
        }
    }

    /// <summary>
    /// IsJump�̒l��ς���֐�
    /// </summary>
    private bool IsJumpBool(bool isJump)
    {
        _isJump = isJump;
        return _isJump;
    }
}
