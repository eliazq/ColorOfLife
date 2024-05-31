using Invector.vCharacterController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public static Player Instance;

    #region InspectorVariables
    public int health { get; private set; }
    public int maxHealth { get; private set; } = 100;
    [SerializeField] private vThirdPersonCamera thirdPersonCamera;
    [SerializeField] private float outOfBoundsY = -50f;
    [SerializeField] private float cameraOutOfBoundsY = -5f;
    #endregion

    #region Events
    public event EventHandler OnDead;
    public event EventHandler OnDamageTaken;
    #endregion

    Vector3 spawnPosition;

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
        spawnPosition = transform.position + Vector3.up;
    }

    private void Update()
    {
        CheckOutOfBounds();
    }

    private void CheckOutOfBounds()
    {
        if (transform.position.y < cameraOutOfBoundsY)
        {
            thirdPersonCamera.enabled = false;
            thirdPersonCamera.transform.LookAt(Player.Instance.transform.position);
        }
        if (transform.position.y < outOfBoundsY)
        {
            transform.position = spawnPosition;
            thirdPersonCamera.enabled = true;
        }
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
