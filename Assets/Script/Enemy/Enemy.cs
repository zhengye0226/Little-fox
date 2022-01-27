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
    //用来记录在场的机器人并唤醒
    public static Dictionary<int, GameObject> RobotList = new Dictionary<int, GameObject>();
    public static int id=0;
    public static Enemy _control;

    // Start is called before the first frame update
    private void Awake()
    {
        _control = this;
        //遍历枚举的方式
        //储存敌人的数量和位置信息
        for (int i = 0; i < (int)ENEMY.NONE; i++)
        {
            List<Vector2> list = new List<Vector2>();
            EnmeyNumber.Add((ENEMY)i, 0);
            EnmeyPosition.Add((ENEMY)i, list);
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
    /// 记录敌人数量
    /// </summary>
    /// <param name="game"></param>
    public void AddNumber(ENEMY enmey)
    {
        _control.EnmeyNumber[enmey] += 1;
    }
    /// <summary>
    /// 记录敌人位置
    /// </summary>
    /// <param name="game"></param>
    /// <param name="position"></param>
    public void AddPosition(ENEMY enmey, Vector2 position)
    {
        _control.EnmeyPosition[enmey].Add(position);

    }
    /// <summary>
    /// 删除敌人数量
    /// </summary>
    /// <param name="game"></param>
    public void RemoveNumber(ENEMY enmey)
    {
        _control.EnmeyNumber[enmey] -= 1;
    }
    /// <summary>
    /// 删除敌人位置
    /// </summary>
    /// <param name="game"></param>
    /// <param name="position"></param>
    public void RemovePosition(ENEMY enmey, Vector2 position)
    {
        _control.EnmeyPosition[enmey].Remove(position);

    }

    /// <summary>
    /// 根据传入的敌人获取对应的类型
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
    /// 道具类加载之后运行
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
    /// 道具类销毁后运行
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
