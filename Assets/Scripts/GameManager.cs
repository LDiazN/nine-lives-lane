using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Menu,InGame
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject Player;
    public float InvulnerabilityTime = 0.5f;
    public GameState State = GameState.Menu;
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
