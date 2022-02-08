using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public static GameManger _instance;
    public int currentHealth;
    public int maxHealth;
    public Vector2 position;
    public bool isPaused;
    public bool isNewGame;
    private void Awake() {

        _instance = this;

        isPaused = false;

        if(PlayerPrefs.GetInt("isNewGame") == 1)
        {
            currentHealth = 5;

            maxHealth = 5;

            position = new Vector2(0, 0);

            isNewGame = true;
        }

        if(PlayerPrefs.GetInt("isNewGame") == 0)
        {
            BattleMenuController hall = new BattleMenuController();
            hall.HallIntoBattle();
        }
    }

}