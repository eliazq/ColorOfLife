using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public static Player Instance;

    public int health { get; private set; }
    public int maxHealth { get; private set; } = 100;
    public event EventHandler OnDead;
    public event EventHandler OnDamageTaken;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }
    private void Start()
    {
        health = maxHealth;
    }

    
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
    [NaughtyAttributes.Button]
    public void TestDamage()
    {
        int damage = 10;
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
        enabled = false;
    }
}
