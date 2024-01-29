using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Menu, InGame, GameOver
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject Player;
    public int Score;
    public float InvulnerabilityTime = 0.5f;
    public GameState State = GameState.Menu;
    [SerializeField] LevelManager LevelManager;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Player = GameObject.Find("Car");
    }
}
