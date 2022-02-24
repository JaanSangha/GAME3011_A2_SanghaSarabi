using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPickButtons : MonoBehaviour
{

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void HardButtonPressed()
    {
        gameManager.SetLockRange(5);
    }
    public void EasyButtonPressed()
    {
        gameManager.SetLockRange(20);

    }
    public void MediumButtonPressed()
    {
        gameManager.SetLockRange(10);
    }
}
