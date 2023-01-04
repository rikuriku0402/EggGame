using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalEffect : MonoBehaviour, IGoalable
{
    [SerializeField]
    [Header("ゴールエフェクト")]
    ParticleSystem _effect;

    public void Goal()
    {
        _effect.gameObject.SetActive(true);
        print("ゴール");
    }
}
