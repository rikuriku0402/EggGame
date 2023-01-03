using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Player _player;

    private float _fallenPos;

    private bool _isFall;

    private float _fallenDisrance;

    void Start()
    {
        TryGetComponent(out _player);
        _fallenDisrance = 0.0f;
        _fallenPos = transform.position.y;
        _isFall = false;
    }

    void Update()
    {
        if (_isFall)
        {
            _fallenPos = Mathf.Max(_fallenPos, transform.position.y);

            if (Physics.Linecast(_rayPos.position, _rayPos.position + Vector3.down * _rayRange, LayerMask.GetMask("Field", "Block")))
            {
                _fallenDisrance = _fallenPos - transform.position.y;

                if (_fallenDisrance >= _damageDistance)
                {
                    _player.PlayerDamage(10);
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
