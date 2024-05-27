using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCircle : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            if ((enemyLayer & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
            {
                Enemy enemy = Cache.GetEnemy(other);
                enemy.ShowTarget();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            if (other.CompareTag("Character"))
            {
                if ((enemyLayer & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
                {
                    Enemy enemy = Cache.GetEnemy(other);
                    enemy.HideTarget();
                }
            }
        }
    }
}
