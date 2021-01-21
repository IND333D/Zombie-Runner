using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float damage = 40f;
    [SerializeField] PlayerHealth playerHealth;

    private void Start()
    {

    }

    public void EnemyAttackEvent()
    {
        if (playerHealth == null)
        {
            return;
        }
        playerHealth.TakeDamage(damage);
        Debug.Log("You are dying");
    }
}
