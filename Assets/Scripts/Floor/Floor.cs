using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour, IFloor
{
    public void FloorDamage(int damage)
    {
        print(damage + "ダメージを受け続けてます");
    }
}
