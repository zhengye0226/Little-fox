using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GAMEPROPS
{
    STRAWBERRY,
    BULLET_BAG,
    NONE
}

public  class Props : MonoBehaviour
{
    public Dictionary<GAMEPROPS, List<Vector2>> PropsPosition = new Dictionary<GAMEPROPS, List<Vector2>>();
    public Dictionary<GAMEPROPS, int> PropsNumber= new Dictionary<GAMEPROPS, int>();
    public static Props _prop;
    private void Awake()
    {
        _prop = this;
        //循环初始化道具
        foreach (GAMEPROPS item in GAMEPROPS.GetValues(typeof(GAMEPROPS)))
        {     
            List<Vector2> listName = new List<Vector2>();
            PropsNumber.Add(item, 0);
            PropsPosition.Add(item, listName);
        }
        //Debug.Log("Is Props Awake");
    }

    private void Start()
    {
        //Debug.Log("Is Props start");
    }
    /// <summary>
    /// 记录道具数量
    /// </summary>
    /// <param name="game"></param>
    public void AddNumber(GAMEPROPS game)
    {
         _prop.PropsNumber[game] += 1;
    }
    /// <summary>
    /// 记录道具位置
    /// </summary>
    /// <param name="game"></param>
    /// <param name="position"></param>
    public void AddPosition(GAMEPROPS game,Vector2 position)
    {
        _prop.PropsPosition[game].Add(position);

    }
    /// <summary>
    /// 删除道具数量
    /// </summary>
    /// <param name="game"></param>
    public void RemoveNumber(GAMEPROPS game)
    {
        _prop.PropsNumber[game] -= 1;
    }
    /// <summary>
    /// 删除道具位置
    /// </summary>
    /// <param name="game"></param>
    /// <param name="position"></param>
    public void RemovePosition(GAMEPROPS game, Vector2 position)
    {
        _prop.PropsPosition[game].Remove(position);

    }
    /// <summary>
    /// 根据传入的道具类获取对应的枚举值
    /// </summary>
    /// <param name="gameProp"></param>
    /// <returns></returns>
    public GAMEPROPS GetGameProps(Object gameProp)
    {
        switch (gameProp.GetType().ToString()) {
            case "StrawBerry":
                    return GAMEPROPS.STRAWBERRY;
            case "ButtleBag": 
                    return GAMEPROPS.BULLET_BAG;       
        }
        return GAMEPROPS.NONE;
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
        _prop.AddNumber(_prop.GetGameProps(obj));
        _prop.AddPosition(_prop.GetGameProps(obj), Position);
    }

    /// <summary>
    /// 道具类销毁后运行
    /// </summary>
    /// <param name="obj"></param>
    public void SetOnDestroy(Object obj)
    {
        Component ob = (Component)obj;
        Vector2 Position = ob.gameObject.transform.position;
        _prop.RemoveNumber(_prop.GetGameProps(this));
        _prop.RemovePosition(_prop.GetGameProps(this), Position);
    }
}
