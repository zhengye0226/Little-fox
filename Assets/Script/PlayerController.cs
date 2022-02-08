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
    public static PlayerController player;
    // Start is called before the first frame update
    public void Awake()
    {
        player = this;
        
        anim = this.gameObject.GetComponent<Animator>();
        fox_rigidbody = this.gameObject.GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        //Application.targetFrameRate ��������ÿ��ˢ�µ�֡��

        //movePosition=transform.position;

        FirstPostion = new Vector2(0, 0);

        this.gameObject.transform.position = GameManger._instance.position;

    }

    // Update is called once per frame
    void Update()
    {
        //movement left or right
        //float WalkX= Input.GetAxis("Horizontal");
        //movement up or down
        //float WalkZ = Input.GetAxis("Vertical");
        if(!GameManger._instance.isPaused){
            //movemnt
            SubComponetsDic[SubsComponents.MOVE_PLAYERMOVE].OnUpdate();
            //Health change
            SubComponetsDic[SubsComponents.HEALTH_CHANGE].OnUpdate();
            //Attack
            SubComponetsDic[SubsComponents.ATTACK].OnUpdate();
        }
        GameManger._instance.position = this.gameObject.transform.position;

    }

    private void FixedUpdate()
    {
        if(!GameManger._instance.isPaused){
            //movemnt
            SubComponetsDic[SubsComponents.MOVE_PLAYERMOVE].OnFixedUpdate();
        }

    }

}
