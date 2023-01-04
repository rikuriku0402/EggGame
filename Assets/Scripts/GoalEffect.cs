using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalEffect : MonoBehaviour, IGoalable
{
    [SerializeField]
    [Header("�S�[���G�t�F�N�g")]
    ParticleSystem _effect;

    public void Goal()
    {
        _effect.gameObject.SetActive(true);
        print("�S�[��");
    }
}
