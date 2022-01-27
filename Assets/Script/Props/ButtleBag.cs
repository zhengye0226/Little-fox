using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtleBag : Props
{
    //播放拾取特效
    public GameObject PropsCatchPrefab;
    public CogBullet cogBullet;
    public AudioClip audioClip;
    // Start is called before the first frame update
    private void Start()
    {

        OnStart(this);
        //Debug.Log(_prop.GetGameProps(this)+""+this.GetType());

    }

    private void OnDestroy()
    {
        SetOnDestroy(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerAttack player = collision.gameObject.GetComponentInChildren<PlayerAttack>();
        //add cogbullet amount
        if (player != null)
        {
            Instantiate(PropsCatchPrefab, this.transform.position, Quaternion.identity);
            player.PlaySound(audioClip);
            player.ChangeBulletNumber(3);
            player.CogBulletPrefab = cogBullet.gameObject;
            Destroy(this.gameObject);
        }
    }
}
