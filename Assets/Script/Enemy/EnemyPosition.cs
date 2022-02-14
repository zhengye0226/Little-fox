using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class EnemyPosition : MonoBehaviour
{
    public static EnemyPosition _instance;
    private void Awake() {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        ReturnAreaPosition();
    }

    /// <summary>
    /// Record the center position width length and height length about EnemyCreateArea
    /// </summary>
    private void ReturnAreaPosition()
    {
        int areaCenterX = (int)this.gameObject.transform.position.x;
        int areaCenterY = (int)this.gameObject.transform.position.y;

        int areaWidthX = (int)this.gameObject.transform.localScale.x;
        int areaHeightY = (int)this.gameObject.transform.localScale.y;

        int minAreaWidth = areaCenterX - (areaWidthX/2);
        int maxAreaWidth = areaCenterX + (areaWidthX/2);

        int minAreaHeight = areaCenterY - (areaHeightY/2);
        int maxAreaHeight= areaCenterY + (areaHeightY/2);

        GameManger._instance.createEnemyAre.Add(new int[]{minAreaWidth, maxAreaWidth}, new int[]{minAreaHeight, maxAreaHeight});
        Debug.Log("Center is ("+ areaCenterX + ", "+ areaCenterY + ") Width is "+ areaWidthX + " Height is " + areaHeightY);
    }

    /// <summary>
    /// Make a random position in EnemyCreatArea
    /// </summary>
    /// <returns></returns>
    private Vector2 ReturnEnemyPosition()
    {
        int areaMaxId = GameManger._instance.createEnemyAre.Count;

        int areaId = Random.Range(0,areaMaxId);

        int[] areaMaxMinWidth = GameManger._instance.createEnemyAre.ElementAt(areaId).Key;
        
        int[] areaMaxMinHeight = GameManger._instance.createEnemyAre.ElementAt(areaId).Value;

        int enemyPositionX = GetRandomPosition(areaMaxMinWidth[0], areaMaxMinWidth[1]);

        int enemyPositionY = GetRandomPosition(areaMaxMinHeight[0], areaMaxMinHeight[1]);
        
        return new Vector2(enemyPositionX, enemyPositionY);
    }

    /// <summary>
    /// randomPosition
    /// </summary>
    /// <param name="minNumber"></param>
    /// <param name="maxNumber"></param>
    /// <returns></returns>
    private int GetRandomPosition(int minNumber, int maxNumber)
    {
        int randomPosition = Random.Range(minNumber, maxNumber);

        return randomPosition;
    }

    /// <summary>
    /// Make serval position in EnemyCreationPosition
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    private List<Vector2> GetRandomEnemyPostion(int number)
    {
        List<Vector2> list = new List<Vector2>();
        for(int i=0; i<number; i++)
        {
            Vector2 enemyPosition = ReturnEnemyPosition();
            while(list.Contains(enemyPosition))
            {
                enemyPosition = ReturnEnemyPosition();
            }
            list.Add(enemyPosition);
        }
        return list;
    } 
    /// <summary>
    /// Add enemys in map
    /// </summary>
    /// <param name="enemyType">enemyType</param>
    /// <param name="enemyNumebr">enemyAmount</param>
    public void AddEnemy(int enemyType, int enemyNumebr)
    {
        List<Vector2> position = GetRandomEnemyPostion(enemyNumebr);
        foreach(var item in position)
        {
            Instantiate(Enemy._control.gameObjects[enemyType], item, Quaternion.identity);
        }
    }
    /// <summary>
    /// Find a enemy, get its position and go there
    /// </summary>
    /// <param name="enemy"></param>
    public void FindEnemy(int enemyType)
    {
        foreach(var item in GetAllSenceObjectInActiveScene())
        {
            if(item.layer == Enemy._control.gameObjects[enemyType].layer)
            {
                PlayerController.player.gameObject.transform.position = item.transform.position;
                break;
            }
        }
    }
    /// <summary>
    /// Return all gameobject in active scene
    /// </summary>
    /// <returns></returns>
    private List<GameObject> GetAllSenceObjectInActiveScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        List<GameObject> allGameOjectInScene = new List<GameObject>();
        foreach(var item in scene.GetRootGameObjects())
        {
            allGameOjectInScene.Add(item);
        }
        return allGameOjectInScene;
    }
    
    //用于获取所有Hierarchy中的物体，包括被禁用的物体
    private List<GameObject> GetAllSceneObjectsWithInactive()
    {
        var allTransforms = Resources.FindObjectsOfTypeAll(typeof(Transform));
        var previousSelection = Selection.objects;
        Selection.objects = allTransforms.Cast<Transform>()
            .Where(x => x != null)
            .Select(x => x.gameObject)
            //如果你只想获取所有在Hierarchy中被禁用的物体，反注释下面代码
            //.Where(x => x != null && !x.activeInHierarchy)
            .Cast<UnityEngine.Object>().ToArray();

        var selectedTransforms = Selection.GetTransforms(SelectionMode.Editable | SelectionMode.ExcludePrefab);
        Selection.objects = previousSelection;

        return selectedTransforms.Select(tr => tr.gameObject).ToList();
    }
}
