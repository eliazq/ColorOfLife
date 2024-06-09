using Invector.vCharacterController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    public static Player Instance;

    [SerializeField] private GameObject playerVisual;

    [SerializeField] private Image[] liveImages = new Image[3];

    public event EventHandler OnPlayerLivesDead;

    #region InspectorVariables
    public int health { get; private set; }
    public int maxHealth { get; private set; } = 100;
    [SerializeField] private vThirdPersonCamera thirdPersonCamera;
    private vThirdPersonController thirdPersonController;
    [SerializeField] private vThirdPersonMotor playerControllerMotor;
    private vThirdPersonInput thirdPersonInput;
    [SerializeField] private float outOfBoundsY = -50f;
    [SerializeField] private float cameraOutOfBoundsY = -5f;
    [SerializeField] private float minFallDamageVelocity = -15;
    [SerializeField] private float normalFallDamageVelocity = -20;
    [SerializeField] private float maxFallDamageVelocity = -30;
    private int minFallDamage = 25;
    private int normalFallDamage = 55;
    private int maxFallDamage = 100;
    bool isInvincible = false;
    Rigidbody rb;
    bool hasRespawned;
    Vector3 GameStartPosition;
    int lives = 3;
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
        GameStartPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        thirdPersonController = GetComponent<vThirdPersonController>();
        thirdPersonInput = GetComponent<vThirdPersonInput>();
        playerControllerMotor.OnLandedGround += Player_OnLandedGround;
    }

    private void Player_OnLandedGround(object sender, vThirdPersonMotor.YVelocityEventArgs e)
    {
        CheckFallDamage(e.yVelocity);
    }

    private void Update()
    {
        CheckOutOfBounds();
        if (rb.velocity.magnitude > 3f && rb.velocity.magnitude < 5f && playerControllerMotor.isGrounded)
        {
            SoundManager.Sound[] soundList = new SoundManager.Sound[] { SoundManager.Sound.Step1, SoundManager.Sound.Step2, SoundManager.Sound.Step3, };
            int ranNum = UnityEngine.Random.Range(0, soundList.Length);
            SoundManager.PlaySoundWithCooldown(soundList[ranNum], 0.38f);
        }
        else if (rb.velocity.magnitude > 5f && playerControllerMotor.isGrounded)
        {
            SoundManager.Sound[] soundList = new SoundManager.Sound[] { SoundManager.Sound.Step1, SoundManager.Sound.Step2, SoundManager.Sound.Step3, };
            int ranNum = UnityEngine.Random.Range(0, soundList.Length);
            SoundManager.PlaySoundWithCooldown(soundList[ranNum], 0.3f);
        }
        
    }

    private void CheckFallDamage(float yVelo)
    {
        if (!canFallDamage) return;

        if (yVelo < minFallDamageVelocity && yVelo > normalFallDamage)
        {
            Damage(minFallDamage);
        }
        else if (yVelo < normalFallDamageVelocity && yVelo > maxFallDamage)
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
            isInvincible = true;
            StartCoroutine(LoseInvinsible());
            Respawn(true);
            thirdPersonCamera.enabled = true;
        }
    }

    IEnumerator LoseInvinsible()
    {
        yield return new WaitForSeconds(5f);
        isInvincible = false;
    }

    public void Damage(int damage)
    {
        if (isInvincible) return;
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
        lives--;
        liveImages[lives].color = Color.white;
        enabled = true;
        thirdPersonCamera.enabled = true;
        transform.position = spawnPosition + Vector3.up;
        if (lives <= 0) LoseGame();
        health = maxHealth;
        transform.position += Vector3.up;
        OnDamageGiven?.Invoke(this, EventArgs.Empty);
        hasRespawned = true;
        StartFallDamageCooldown();
        StartCoroutine(CooldownController());
    }
    float maxCounter = 1.3f;
    float counter = 0;
    IEnumerator CooldownController()
    {
        while (true)
        {
            playerVisual.SetActive(false);
            transform.position = spawnPosition;
            rb.velocity = Vector3.zero;
            thirdPersonInput.enabled = false;
            thirdPersonController.enabled = false;
            counter += Time.deltaTime;
            if (counter >= maxCounter)
            {
                counter = 0;
                thirdPersonInput.enabled = true;
                thirdPersonController.enabled = true;
                playerVisual.SetActive(true);
                break;
            }
            yield return null;
        }

    }

    private void LoseGame()
    {
        spawnPosition = GameStartPosition + Vector3.up;
        lives = 3;
        OnPlayerLivesDead?.Invoke(this, EventArgs.Empty);
        foreach(Image image in liveImages)
        {
            image.color = Color.red;
        }
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
