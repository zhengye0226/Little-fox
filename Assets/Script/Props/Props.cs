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
    public List<GameObject> propslist = new List<GameObject>();
    public static Props _prop;
    public GameObject[] gameObjects = new GameObject[(int)(GAMEPROPS.NONE)];
    private void Awake()
    {
        _prop = this;
        //ѭ����ʼ������
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
    /// ��¼��������
    /// </summary>
    /// <param name="game"></param>
    public void AddNumber(GAMEPROPS game)
    {
         _prop.PropsNumber[game] += 1;
    }
    /// <summary>
    /// ��¼����λ��
    /// </summary>
    /// <param name="game"></param>
    /// <param name="position"></param>
    public void AddPosition(GAMEPROPS game,Vector2 position)
    {
        _prop.PropsPosition[game].Add(position);

    }
    /// <summary>
    /// ɾ����������
    /// </summary>
    /// <param name="game"></param>
    public void RemoveNumber(GAMEPROPS game)
    {
        _prop.PropsNumber[game] -= 1;
    }
    /// <summary>
    /// ɾ������λ��
    /// </summary>
    /// <param name="game"></param>
    /// <param name="position"></param>
    public void RemovePosition(GAMEPROPS game, Vector2 position)
    {
        _prop.PropsPosition[game].Remove(position);

    }
    public void AddPropsGameObject(GameObject gameObject)
    {
        _prop.propslist.Add(gameObject);
    }
    public void RemovePropsGameObject(GameObject gameObject)
    {
        _prop.propslist.Remove(gameObject);
    }
    /// <summary>
    /// ���ݴ���ĵ������ȡ��Ӧ��ö��ֵ
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
    /// ���������֮������
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
        _prop.AddPropsGameObject(ob.gameObject);
    }

    /// <summary>
    /// ���������ٺ�����
    /// </summary>
    /// <param name="obj"></param>
    public void SetOnDestroy(Object obj)
    {
        Component ob = (Component)obj;
        Vector2 Position = ob.gameObject.transform.position;
        _prop.RemoveNumber(_prop.GetGameProps(obj));
        _prop.RemovePosition(_prop.GetGameProps(obj), Position);
        _prop.RemovePropsGameObject(ob.gameObject);
    }
}
