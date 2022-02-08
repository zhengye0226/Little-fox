using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class GameSave{
    public int currentHealth;
    public int maxHealth;
    public float playerPositionX;
    public float playerPositionY;

    public Dictionary<int, float> enemyPositionX;
    public Dictionary<int, float> enemyPositionY;
    public Dictionary<int, bool> isDead;
}
