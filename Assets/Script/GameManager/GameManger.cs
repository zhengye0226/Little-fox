using System.Reflection;
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
    public Dictionary<int, bool> missionStart = new Dictionary<int, bool>();
    public Dictionary<int, bool> missionCompelete = new Dictionary<int, bool>();
    public Dictionary<int, int> missionCondition = new Dictionary<int, int>();
    public Dictionary<int, int> missionProgress = new Dictionary<int, int>();
    public Dictionary<int[], int[]> createEnemyAre = new Dictionary<int[], int[]>();
    public List<Vector2> enemyCreatePosition = new List<Vector2>();
    private void Awake() {

        _instance = this;

        isPaused = false;
        
        currentHealth = 5;

        maxHealth = 5;
        
        position = new Vector2(0, 0);
    }

}