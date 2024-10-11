using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Player Variables
    public float playerMaxHealth = 100f;
    public float playerCurrentHealth = 100f;
    public float playerSpeed = 2;
    public float playerMoveSmoothTime = 0.05f;

    public float playerIncinvinbleDuration = 0.5f;

    public float playerBulletSpeed = 3f;
    public float playerTimeBetweenShots = 0.5f;

    public float screenBorder = 20;

    // Enemy A damage, speed, rotation speed
    public float enemyADamage = 20;
    public float enemyASpeed = 1.5f;
    public float enemyARotationSpeed = 1000;

    // Distance enemy notice and chase player
    public float playerAwarenessDistance = 20;

    // Enemy A Spawn Variables
    public float enemyAMinSpawnTime = 0;
    public float enemyAMaxSpawnTime = 1;
    public int enemyAMaxCount = 30;
    public float enemyASpawnDistance = 1;

    public static GameController instance;

    void Awake()
    {
        // Ensure there's only one instance of GameController
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this GameObject alive across scenes
        }
        else
        {
            Destroy(gameObject); // If another instance exists, destroy this one
        }
    }

    public void RestartGame()
    {
        // Reloads the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        playerCurrentHealth = playerMaxHealth;
    }

    public static GameController GetInstance()
    {
        return instance; // Returns the current instance of GameController
    }
}
