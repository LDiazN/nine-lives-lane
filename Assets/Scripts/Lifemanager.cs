using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifemanager : MonoBehaviour
{
    public float CurrentLifes = 7;
    public static Lifemanager Instance;

    private void Awake()
    {
        if(Instance == null) 
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LifeBehaviour(int damage)
    {
        CurrentLifes -= damage;
    }
}
