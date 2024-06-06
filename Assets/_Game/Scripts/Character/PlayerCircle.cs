using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCircle : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] private LayerMask enemyLayer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            if ((enemyLayer & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
            {
                Enemy enemy = Cache.GetEnemy(other);
                player.GetTarget();
                if (Vector3.Distance(enemy.transform.position, player.target.position) <= 0.1f)
                {
                    enemy.ShowTarget();
                }
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
