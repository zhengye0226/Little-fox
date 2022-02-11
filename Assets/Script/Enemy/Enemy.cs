using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENEMY
{
    ROBOT,
    NONE
}
public class Enemy : MonoBehaviour
{

    public Dictionary<ENEMY, int> EnmeyNumber = new Dictionary<ENEMY, int>();
    public Dictionary<ENEMY, Dictionary<int, Vector2>> EnmeyPosition = new Dictionary<ENEMY, Dictionary<int, Vector2>>();
    public Dictionary<ENEMY, Dictionary<int, bool>> EnemyIsDead = new Dictionary<ENEMY, Dictionary<int, bool>>();
    public List<GameObject> EnemyGameObject = new List<GameObject>();
    public Dictionary<GameObject, int> returnEnemyId = new Dictionary<GameObject, int>();
    public static Enemy _control;
    public GameObject[] gameObjects = new GameObject[(int)ENEMY.NONE];
    public static int _id = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        _control = this;
        //����ö�ٵķ�ʽ
        //������˵�������λ����Ϣ
        for (int i = 0; i < (int)ENEMY.NONE; i++)
        {
            Dictionary<int, Vector2> list = new Dictionary<int, Vector2>();
            _control.EnmeyNumber.Add((ENEMY)i, 0);
            _control.EnmeyPosition.Add((ENEMY)i, list);
            Dictionary<int, bool> isDeadList = new Dictionary<int, bool>();
            _control.EnemyIsDead.Add((ENEMY)i, isDeadList);
        }

        //Debug.Log("Is Enemy Awake");
        //foreach (ENEMY item in ENEMY.GetValues(typeof(ENEMY)))
        //{
        //    List<Vector2> listName = new List<Vector2>();
        //    EnmeyNumber.Add(item, 0);
        //    EnmeyPosition.Add(item, listName);
        //}
    }
    public int ReturnEnemyId(GameObject enemy)
    {
        return _control.returnEnemyId[enemy];
    }

    public bool EnemyIsDeadYorN(Component enemy)
    {
        return _control.EnemyIsDead[GetGameProps(enemy)][ReturnEnemyId(enemy.gameObject)];
    }
    public void SaveIdEnemy(GameObject enemyGameObject, Component enemy, Vector2 position, bool isDead)
    {
        if(!_control.EnmeyPosition[GetGameProps(enemy)].ContainsKey(_control.EnmeyNumber[GetGameProps(enemy)]))
        {
            _control.EnmeyPosition[GetGameProps(enemy)].Add(_control.EnmeyNumber[GetGameProps(enemy)], position);
            _control.EnemyIsDead[GetGameProps(enemy)].Add(_control.EnmeyNumber[GetGameProps(enemy)], isDead);
        }
        _control.EnemyGameObject.Add(enemyGameObject);
        _control.returnEnemyId.Add(enemyGameObject, _control.EnmeyNumber[GetGameProps(enemy)]);

    }
    public void SaveInUpdate(GameObject enemyGameObject, Component enemy, Vector2 position)
    {
        _control.EnmeyPosition[_control.GetGameProps(enemy)][ReturnEnemyId(enemyGameObject)] = position;
    }

    public void SaveEnemy(GameObject enemyGameObject, Component enemy, Vector2 position, bool isDead)
    {
        _control.EnmeyPosition[_control.GetGameProps(enemy)][ReturnEnemyId(enemyGameObject)] = position;
        _control.EnemyIsDead[_control.GetGameProps(enemy)][ReturnEnemyId(enemyGameObject)] = isDead;
    }

    private void Start()
    {
        //Debug.Log("Is Enemy Start");
    }
    /// <summary>
    /// Return ENEMY values
    /// </summary>
    /// <param name="game">Enemy type</param>
    public void AddNumber(ENEMY enmey)
    {
        _control.EnmeyNumber[enmey] += 1;
    }

    /// <summary>
    /// Add Enemy Position
    /// </summary>
    /// <param name="game">Enemy type </param>
    /// <param name="position">Enemy Position</param>
    // public void AddPosition(ENEMY enmey, Vector2 position)
    // {
    //     _control.EnmeyPosition[enmey].Add(position);
    // }

    /// <summary>
    /// Remove Enemynumber
    /// </summary>
    /// <param name="game">Enemy type</param>
    public void RemoveNumber(ENEMY enmey)
    {
        _control.EnmeyNumber[enmey] -= 1;
    }

    /// <summary>
    /// Remove EnemyPosition
    /// </summary>
    /// <param name="game">Enemy type</param>
    /// <param name="position">Enemy Position</param>
    // public void RemovePosition(ENEMY enmey, Vector2 position)
    // {
    //     _control.EnmeyPosition[enmey].Remove(position);

    // }

    /// <summary>
    /// ���ݴ���ĵ��˻�ȡ��Ӧ������
    /// </summary>
    /// <param name="enmeyType"></param>
    /// <returns></returns>
    public ENEMY GetGameProps(Object enmeyType)
    {
        switch (enmeyType.GetType().ToString())
        {
            case "Robot":
                return ENEMY.ROBOT;
        }
        return ENEMY.NONE;
    }

    /// <summary>
    /// ���������֮������
    /// </summary>
    /// <param name="obj"></param>
    public void OnStart(Object obj)
    {
        Component ob = (Component)obj;
        //Debug.Log(ob);
        Vector2 Position = ob.gameObject.transform.position;
        //Debug.Log(Position);

        SaveIdEnemy(ob.gameObject, ob, Position, false);
         AddNumber(GetGameProps(obj));
        //_control.AddPosition(_control.GetGameProps(obj), Position);
    }

    /// <summary>
    /// ���������ٺ�����
    /// </summary>
    /// <param name="obj"></param>
    public void SetOnDestroy(Component obj)
    {

        RemoveNumber(GetGameProps(obj));

        _control.EnmeyPosition[GetGameProps(obj)].Remove(ReturnEnemyId(obj.gameObject));
        _control.EnemyIsDead[GetGameProps(obj)].Remove(ReturnEnemyId(obj.gameObject));
        _control.EnemyGameObject.Remove(obj.gameObject);
        _control.returnEnemyId.Remove(obj.gameObject);
        //Vector2 Position = ob.gameObject.transform.position;
        //_control.RemovePosition(_control.GetGameProps(obj), Position);
    }

}
