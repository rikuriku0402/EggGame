using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerHp => _playerHp;

    [SerializeField]
    [Header("HP")]
    int _playerHp;

    [SerializeField]
    [Header("移動スピード")]
    private float _speed = 5f;

    [SerializeField]
    [Header("ジャンプパワー")]
    private float _jumpPower;

    private Rigidbody _rb;

    private float _horizontal, _vertical;

    private bool _isJump;

    void Start()
    {
        TryGetComponent(out _rb);
    }

    void Update()
    {
        GetKey();

        if (!_isJump && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IJump jump))
        {
            jump.Jump(IsJumpBool(false));
        }
    }

    void GetKey()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
    }

    /// <summary>
    /// プレイヤーの移動関数
    /// </summary>
    void Move()
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
    /// ジャンプ関数
    /// </summary>
    void Jump()
    {
        _rb.velocity = new Vector3(0.0f, _jumpPower, 0.0f);
        _isJump = true;
    }

    /// <summary>
    /// IsJumpの値を変える関数
    /// </summary>
    bool IsJumpBool(bool isJump)
    {
        _isJump = isJump;
        return _isJump;
    }

    public int PlayerDamage(int playerHp)
    {
        _playerHp -= playerHp;
        return _playerHp;
    }
}
