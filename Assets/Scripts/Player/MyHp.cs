using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyHp : MonoBehaviour
{
    [SerializeField]
    [Header("HP�X���C�_�[")]
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
    /// Player���_���[�W���󂯂�֐�
    /// </summary>
    /// <param name="playerHp">���_���[�W�󂯂邩�����Ō��߂�</param>
    /// <returns>���݂�Hp��Ԃ�</returns>
    public int PlayerDamage(int playerHp)
    {
        _playerHp -= playerHp;
        SetHP();
        return _playerHp;
    }

    /// <summary>
    /// �_���[�W���󂯂����ɃX���C�_�[���X�V����֐�
    /// </summary>
    private void SetHP()
    {
        _hpSlider.value = _playerHp;
    }
}
