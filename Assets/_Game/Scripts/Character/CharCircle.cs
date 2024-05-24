using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCircle : MonoBehaviour
{
    [SerializeField] private Character character;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            character.ResetAttack();
            Destroy(other.gameObject);  
        }
    }
}
