using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;
    int health = 200;
    //Player player;

    private void Awake()
    {
        SetUpSingleton();
        //player = FindObjectOfType<Player>();
        //health = player.GetComponent<Player>().GetHealth();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    public int GetHealth()
    {
        return health;
    }

    public void AddToHealth(int healthValue)
    {
        health -= healthValue;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
