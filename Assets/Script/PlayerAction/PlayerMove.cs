using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : SubsComponent
{
    private Vector2 movePosition;

    //control player direction
    private Vector2 PlayerDirection=new Vector2(0,-1);

    //control player idle

    private float speed;
    //play music when ruby move
    private AudioSource FootMusic;

    private void OnEnable()
    {
        control.SubComponetsDic.Add(SubsComponents.MOVE_PLAYERMOVE, this);
        FootMusic = this.gameObject.GetComponent<AudioSource>();
    }
    //public void Start()
    //{
    //    control.SubComponetsDic.Add(SubsComponents.MOVE_PLAYERMOVE,this);
    //}
    public override void OnUpdate()
    {
        NpcTalk();
        PlayerMovement();
        PlayerAnimation();
    }
    public override void OnFixedUpdate()
    {
        PlayerGetMove();
    }
    /// <summary>
    /// movement
    /// </summary>
    public void PlayerMovement()
    {
        movePosition = Vector2.zero;
        float run = 0;
        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    run = 2.0f;
        //}
        if (Input.GetKey(KeyBoardContorller.keyboardController.up))
        {
            movePosition += Vector2.up * (control.maxSpeed + run);
        }
        if (Input.GetKey(KeyBoardContorller.keyboardController.left))
        {
            movePosition += Vector2.left * (control.maxSpeed + run);
        }
        if (Input.GetKey(KeyBoardContorller.keyboardController.down))
        {
            movePosition += Vector2.down * (control.maxSpeed + run);
        }
        if (Input.GetKey(KeyBoardContorller.keyboardController.right))
        {
            movePosition += Vector2.right * (control.maxSpeed + run);
        }
        if (movePosition != Vector2.zero)
        {
            if (!FootMusic.isPlaying) {
                FootMusic.Play();
            }
        }
        else {
            FootMusic.Stop();
        }
    }
    public void PlayerGetMove()
    {
        CancelPlayerMovement();
        control.fox_rigidbody.AddForce(movePosition, ForceMode2D.Impulse);
    }

    private void CancelPlayerMovement()
    {
        control.fox_rigidbody.AddForce(Vector2.up * -control.fox_rigidbody.velocity.y, ForceMode2D.Impulse);
        control.fox_rigidbody.AddForce(Vector2.right * -control.fox_rigidbody.velocity.x, ForceMode2D.Impulse);
    }

    private void PlayerAnimation()
    {
        control.anim.SetFloat("Speed", movePosition.magnitude);
        //����ɫͣ��ʱ����֮ǰ�ĳ���
        if(movePosition!=Vector2.zero)
            PlayerDirection = movePosition;
        control.anim.SetFloat("Look X", PlayerDirection.normalized.x);
        control.anim.SetFloat("Look Y", PlayerDirection.normalized.y);
    }

    //��ȡ��ǰ��ɫ����
    public Vector2 GetPlayerDirection()
    {
        return PlayerDirection.normalized;
    }

    /// <summary>
    /// NPC
    /// </summary>
    public void NpcTalk()
    {
        //����Ƿ���Npc�Ի�
        if (Input.GetKeyDown(KeyCode.K))
        {
            Vector2 start = control.gameObject.transform.position;
            RaycastHit2D ray = Physics2D.Raycast(start+Vector2.up*0.2f, PlayerDirection,1.5f,LayerMask.GetMask("NPC"));
            if (ray.collider != null)
            {
                NpcDialog npc =ray.collider.gameObject.GetComponent<NpcDialog>();
                npc.DisplayDialog();
            }
        }
    }
}
