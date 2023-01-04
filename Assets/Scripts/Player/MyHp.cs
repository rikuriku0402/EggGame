using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyHp : MonoBehaviour
{
    [SerializeField]
    [Header("HPスライダー")]
    private Slider _hpSlider;

    [SerializeField]
    [Header("HP")]
    private int _playerHp;

    void Start()
    {
        _hpSlider.maxValue = _playerHp;
        _hpSlider.value = _playerHp;
    }

    /// <summary>
    /// Playerがダメージを受ける関数
    /// </summary>
    /// <param name="playerHp">何ダメージ受けるか引数で決める</param>
    /// <returns>現在のHpを返す</returns>
    public int PlayerDamage(int playerHp)
    {
        _playerHp -= playerHp;
        SetHP();
        return _playerHp;
    }

    /// <summary>
    /// ダメージを受けた時にスライダーを更新する関数
    /// </summary>
    private void SetHP()
    {
        _hpSlider.value = _playerHp;
    }
}
