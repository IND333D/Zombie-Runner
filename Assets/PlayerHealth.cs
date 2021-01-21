using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float playerHealth = 100f;

    private void Start()
    {

    }

    public void TakeDamage(float damage)
    {
        if (playerHealth <= 0f)
        {
            print("you are dead");
            playerHealth = 0f;
            GetComponent<DeathHandler>().HandleDeath();
            return;
        }
        playerHealth -= damage;
    }
}
