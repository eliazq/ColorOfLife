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
    private vThirdPersonController thirdPersonController;
    [SerializeField] private vThirdPersonMotor playerControllerMotor;
    [SerializeField] private float outOfBoundsY = -50f;
    [SerializeField] private float cameraOutOfBoundsY = -5f;
    [SerializeField] private float minFallDamageVelocity = -15;
    [SerializeField] private float normalFallDamageVelocity = -20;
    [SerializeField] private float maxFallDamageVelocity = -30;
    private int minFallDamage = 25;
    private int normalFallDamage = 55;
    private int maxFallDamage = 100;
    Rigidbody rb;
    #endregion

    #region Events
    public event EventHandler OnDead;
    public event EventHandler OnDamageTaken;
    public event EventHandler OnDamageGiven;
    #endregion

    public Vector3 spawnPosition { get; set; }

    bool canFallDamage = true;
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
        rb = GetComponent<Rigidbody>();
        thirdPersonController = GetComponent<vThirdPersonController>();
        playerControllerMotor.OnLandedGround += Player_OnLandedGround;
    }

    private void Player_OnLandedGround(object sender, vThirdPersonMotor.YVelocityEventArgs e)
    {
        CheckFallDamage(e.yVelocity);
    }

    private void Update()
    {
        CheckOutOfBounds();
    }

    private void CheckFallDamage(float yVelo)
    {
        if (!canFallDamage) return;

        if (yVelo < minFallDamageVelocity)
        {
            Damage(minFallDamage);
        }
        else if (yVelo < normalFallDamageVelocity)
        {
            Damage(normalFallDamage);
        }
        else if (yVelo < maxFallDamageVelocity)
        {
            Damage(maxFallDamage);
        }
        else if (yVelo == 0 || yVelo < 0.001f && yVelo > 0f)
        { // When it drops from big heights. It's a bug kinda, i dont wanna deal with it so i found this easy fix
            Damage(maxFallDamage);
        }
    }

    private void CheckOutOfBounds()
    {
        if (transform.position.y < cameraOutOfBoundsY)
        {
            thirdPersonCamera.enabled = false;
            thirdPersonCamera.transform.LookAt(transform.position);
        }
        if (transform.position.y < outOfBoundsY)
        {
            Respawn(false);
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
    [NaughtyAttributes.Button]
    public void Respawn(bool healthBack = true)
    {
        enabled = true;
        thirdPersonCamera.enabled = true;
        GetComponent<vThirdPersonInput>().enabled = true;
        transform.position = spawnPosition;
        health = maxHealth;
        OnDamageGiven?.Invoke(this, EventArgs.Empty);
        StartFallDamageCooldown();
    }
    private void Die()
    {
        enabled = false;
        thirdPersonCamera.enabled = false;
        GetComponent<vThirdPersonInput>().enabled = false;
        StartCoroutine(RespawnAfterDying());
    }
    
    IEnumerator RespawnAfterDying()
    {
        yield return new WaitForSeconds(1f);
        Respawn();
    }

    private void StartFallDamageCooldown(float cooldown = 0.3f)
    {
        StartCoroutine(FallDamageCooldown(cooldown));
    }

    IEnumerator FallDamageCooldown(float cooldownLenght)
    {
        canFallDamage = false;
        yield return new WaitForSeconds(cooldownLenght);
        canFallDamage = true;
    }
}
