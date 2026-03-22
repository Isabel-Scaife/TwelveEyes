using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public delegate void EnemyDestroyed();
    EnemyDestroyed destroyEnemy;
    private int enemiesKilled;
    private Enemy[] enemies;

    [SerializeField] public ChangeScene changeScene;

    void Start()
    {
        enemiesKilled = 0;

        if (destroyEnemy == null)
        {
            destroyEnemy = DestroyEnemy;
        }

        enemies = Object.FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        for (int i = 0; i < enemies.Length; i++)
        {
            Debug.Log(enemies[i]);
        }
    }

    // Update is called once per frame
    void Update() {}

    /// <summary>
    /// Defines what happens when an enemy dies
    /// </summary>
    public void DestroyEnemy()
    {
        Debug.Log("Enemy died!");
        enemiesKilled++;
        if (enemiesKilled >= enemies.Length)
        {
            changeScene.Change(0);
        }
    }
}
