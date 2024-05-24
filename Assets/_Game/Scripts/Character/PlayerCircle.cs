using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCircle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            Enemy enemy;
            if (other.gameObject.TryGetComponent<Enemy>(out enemy))
            {
                enemy.ShowTarget();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            Enemy enemy;
            if (other.gameObject.TryGetComponent<Enemy>(out enemy))
            {
                enemy.HideTarget();
            }
        }
    }
}
