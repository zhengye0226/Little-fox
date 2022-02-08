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
    public Dictionary<ENEMY, List<Vector2>> EnmeyPosition = new Dictionary<ENEMY, List<Vector2>>();
    //Save Robot
    public static Dictionary<int, GameObject> RobotList = new Dictionary<int, GameObject>();
    public static int id;
    public static Enemy _control;

    // Start is called before the first frame update
    private void Awake()
    {
        _control = this;
        //����ö�ٵķ�ʽ
        //������˵�������λ����Ϣ
        for (int i = 0; i < (int)ENEMY.NONE; i++)
        {
            List<Vector2> list = new List<Vector2>();
            EnmeyNumber.Add((ENEMY)i, 0);
            EnmeyPosition.Add((ENEMY)i, list);
        }
        if(PlayerPrefs.GetInt("isNewGame") == 1)
        {
            Enemy.id = 0;
            RobotList.Clear();
        }
        //Debug.Log("Is Enemy Awake");
        //foreach (ENEMY item in ENEMY.GetValues(typeof(ENEMY)))
        //{
        //    List<Vector2> listName = new List<Vector2>();
        //    EnmeyNumber.Add(item, 0);
        //    EnmeyPosition.Add(item, listName);
        //}
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
    /// Add Enemy number
    /// </summary>
    /// <param name="game">Enemy type </param>
    /// <param name="position">Enemy Position</param>
    public void AddPosition(ENEMY enmey, Vector2 position)
    {
        _control.EnmeyPosition[enmey].Add(position);

    }
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
    public void RemovePosition(ENEMY enmey, Vector2 position)
    {
        _control.EnmeyPosition[enmey].Remove(position);

    }

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
        _control.AddNumber(_control.GetGameProps(obj));
        _control.AddPosition(_control.GetGameProps(obj), Position);
    }

    /// <summary>
    /// ���������ٺ�����
    /// </summary>
    /// <param name="obj"></param>
    public void SetOnDestroy(Object obj)
    {
        Component ob = (Component)obj;
        Vector2 Position = ob.gameObject.transform.position;
        _control.RemoveNumber(_control.GetGameProps(this));
        _control.RemovePosition(_control.GetGameProps(this), Position);
    }

}
