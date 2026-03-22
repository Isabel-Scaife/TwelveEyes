using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEditor.SearchService;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public int numEnemies;

    [SerializeField] public ChangeScene changeScene;

    void Start()
    {
    }

    // Update is called once per frame
    void Update(){}

    /// <summary>
    /// Win when all enemies die
    /// </summary>
    public void WinCondition()
    {
        if (numEnemies <= 0)
        {
            changeScene.Change(0);
        }
    }
}
