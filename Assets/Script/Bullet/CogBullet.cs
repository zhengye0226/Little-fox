using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogBullet : MonoBehaviour
{
    private Rigidbody2D cogBullet;

    private float destotyTime;

    // Start is called before the first frame update
    //private void Awake()
    //{
    //    cogBullet = this.gameObject.GetComponent<Rigidbody2D>();

    //    destotyTime = 1.5f;

    //}
    void Start()
    {
        destotyTime = 1.5f;
        cogBullet = this.gameObject.GetComponent<Rigidbody2D>();
        Launch(PlayerAttack.BulletLookDirection, 5);
    }

    // Update is called once per frame
    private void Update()
    {
        TimeEndDestroy();
        //ʹ���ӵ����еĳ������ж��Ƿ�Ҫ�����ӵ�
        //if (this.gameObject.transform.position.magnitude > 20)
        //    Destroy(this.gameObject);
    }

    /// <summary>
    /// Bullet Movemnet
    /// </summary>
    /// <param name="lookPosition">direction</param>
    /// <param name="speed">speed</param>
    private void Launch(Vector2 lookPosition,float speed)
    {
        cogBullet.AddForce(lookPosition*speed,ForceMode2D.Impulse);
    }
    /// <summary>
    /// Repair Robot
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Robot rb = collision.GetComponent<Robot>();
        //��Ϊ�յ�ʱ������޸�����
        if(rb!=null)
            rb.IsRepair();
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Destory bullet when timeEnd
    /// </summary>
    private void TimeEndDestroy()
    {
        destotyTime -= Time.deltaTime;
        if (destotyTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
