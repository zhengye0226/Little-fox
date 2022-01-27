using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Speed")]
    public float maxSpeed;

    [Header("Rigidbody2D")]
    public Rigidbody2D fox_rigidbody;

    [Header("Aniamtion")]
    public Animator anim;
    //record ruby first position
    public Vector2 FirstPostion;

    public Dictionary<SubsComponents, SubsComponent> SubComponetsDic = new Dictionary<SubsComponents, SubsComponent>();
    public Dictionary<GAMEPROPS, SubsComponent> GamePropDic = new Dictionary<GAMEPROPS, SubsComponent>();
    public Dictionary<PLAYERINFOMATION, Object> PlayerInformation = new Dictionary<PLAYERINFOMATION, Object>();

    // Start is called before the first frame update
    public void Awake()
    {
        anim = this.gameObject.GetComponent<Animator>();
        fox_rigidbody = this.gameObject.GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        //Application.targetFrameRate 可以设置每秒刷新的帧率

        //movePosition=transform.position;

        FirstPostion = this.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //从键盘获取输入进行横线移动
        //float WalkX= Input.GetAxis("Horizontal");
        //从键盘获取输入进行纵向移动
        //float WalkZ = Input.GetAxis("Vertical");

        //movemnt
        SubComponetsDic[SubsComponents.MOVE_PLAYERMOVE].OnUpdate();
        //Health change
        SubComponetsDic[SubsComponents.HEALTH_CHANGE].OnUpdate();
        //Attack
        SubComponetsDic[SubsComponents.ATTACK].OnUpdate();

    }

    private void FixedUpdate()
    {
        //movemnt
        SubComponetsDic[SubsComponents.MOVE_PLAYERMOVE].OnFixedUpdate();
    }

}
