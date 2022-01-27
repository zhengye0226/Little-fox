using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : SubsComponent
{
    public GameObject CogBulletPrefab;

    private PlayerMove playermove;

    public float BulletSpeed = 5.0f;

    private float BulletNumber = 0f;

    private AudioSource audioSource;
    //BulletLookDirection
    public static Vector2 BulletLookDirection;
    // Start is called before the first frame update
    private void OnEnable()
    {
        control.SubComponetsDic.Add(SubsComponents.ATTACK, this);
        playermove = control.GetComponentInChildren<PlayerMove>();
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    //void Start()
    //{
    //    control.SubComponetsDic.Add(SubsComponents.ATTACK, this);
    //    playermove = control.GetComponentInChildren<PlayerMove>();
    //}

    public override void OnUpdate()
    {
        //���ӵ�����0��ʱ��ſ������
        if (Input.GetKeyDown(KeyBoardContorller.keyboardController.attack)&& BulletNumber>0) {
            Launch(playermove.GetPlayerDirection(), control.transform.position, BulletSpeed);
            control.anim.SetTrigger("Launch");
        }
    }
    /// <summary>
    /// shoot
    /// </summary>
    /// <param name="BulletLookDirection">bullet direction</param>
    /// <param name="PlayerPosition">bullet startposition</param>
    /// <param name="speed">bullet speed</param>
    private void Launch(Vector2 LookPosition,Vector2 PlayerPosition,float speed)
    {
        Instantiate(CogBulletPrefab, PlayerPosition+Vector2.up*0.4f, Quaternion.identity);
        //��ȡ��ɫ��ǰ����
        BulletLookDirection = LookPosition.normalized;
        ChangeBulletNumber(-1);
    }

    /// <summary>
    /// change BulletNumber
    /// </summary>
    /// <param name="number"></param>
    public void ChangeBulletNumber(int number)
    {
        BulletNumber += number;
    }
    /// <summary>
    /// ���Ŷ�Ӧ��Ƶ
    /// </summary>
    /// <param name="a">��Ƶ��Դ</param>
    public void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

}
