using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public int health { get; private set; }
    public event EventHandler OnDead;
    public event EventHandler OnDamageTaken;

    public void Damage(int damage)
    {
        health -= damage;
        OnDamageTaken?.Invoke(this, EventArgs.Empty);
        if (health <= 0)
        {
            health = 0;
            Die();
            OnDead?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Die()
    {
        Debug.Log("Player Died");
    }
}
