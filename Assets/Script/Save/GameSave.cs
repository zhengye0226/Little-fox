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

    public Dictionary<ENEMY, Dictionary<int, Vector2>> enemyPosition = new Dictionary<ENEMY, Dictionary<int, Vector2>>();
    public Dictionary<ENEMY, Dictionary<int, bool>> isDead = new Dictionary<ENEMY, Dictionary<int, bool>>();
    public Dictionary<int, bool> missionStart = new Dictionary<int, bool>();
    public Dictionary<int, bool> missionCompelete = new Dictionary<int, bool>();
    public Dictionary<int, int> missionCondition = new Dictionary<int, int>();
    public Dictionary<int, int> missionProgress = new Dictionary<int, int>(); 
    public Dictionary<GAMEPROPS, List<Vector2>> PropsPosition = new Dictionary<GAMEPROPS, List<Vector2>>();
    public Dictionary<GAMEPROPS, int> PropsNumber= new Dictionary<GAMEPROPS, int>();
}
