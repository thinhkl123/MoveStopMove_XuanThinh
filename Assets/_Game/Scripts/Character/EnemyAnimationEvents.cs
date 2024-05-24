using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    public void LaunchWeapon()
    {
        enemy.LaunchWeapon();
    }
}
