using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class FallDamage : MonoBehaviour
{
    [SerializeField]
    [Header("レイを飛ばす場所")]
    private Transform _rayPos;

    [SerializeField]
    [Header("レイを飛ばす距離")]
    private float _rayRange;

    [SerializeField]
    [Header("どのくらいの高さからダメージを受けるか")]
    private float _damageDistance;

    [SerializeField]
    [Header("ダメージ数")]
    int _damage;

    /// <summary>
    /// Hpを管理するクラス
    /// </summary>
    private MyHp _myHp;

    /// <summary>
    /// 今落ちている状態か判定する変数
    /// </summary>
    private bool _isFall;

    /// <summary>
    /// 現在のY軸のポジション
    /// </summary>
    private float _fallenPos;

    /// <summary>
    /// 地面との距離を図る変数
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
    /// 現在落ちているかチェックする関数
    /// 一定の高さから落ちたらダメージを受ける
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
