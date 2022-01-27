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
        //Application.targetFrameRate ��������ÿ��ˢ�µ�֡��

        //movePosition=transform.position;

        FirstPostion = this.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //�Ӽ��̻�ȡ������к����ƶ�
        //float WalkX= Input.GetAxis("Horizontal");
        //�Ӽ��̻�ȡ������������ƶ�
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
