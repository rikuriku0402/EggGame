using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Player : MonoBehaviour
{
    [SerializeField]
    [Header("移動スピード")]
    private float _speed = 5f;

    [SerializeField]
    [Header("ジャンプパワー")]
    private float _jumpPower;

    /// <summary>
    /// 剛体
    /// </summary>
    private Rigidbody _rb;

    /// <summary>
    /// 縦横の判定をとる変数
    /// </summary>
    private float _vertical, _horizontal;

    /// <summary>
    /// 今ジャンプ中か判定する変数
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
    /// 常にキーを更新する関数
    /// </summary>
    private void GetKey()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
    }

    /// <summary>
    /// プレイヤーの移動関数
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
    /// ジャンプ関数
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
    /// IsJumpの値を変える関数
    /// </summary>
    private bool IsJumpBool(bool isJump)
    {
        _isJump = isJump;
        return _isJump;
    }
}
