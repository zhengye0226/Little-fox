using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class StrawBerry: Props
{
    //播放拾取特效
    public GameObject PropsCatchPrefab;
    public AudioClip audioClip;
    private void Start()
    {

        OnStart(this);
    }

    private void OnDestroy()
    {
        SetOnDestroy(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponentInChildren<PlayerHealth>();
        if (player!=null&&!player.IsMaxHealth())
        {
            Instantiate(PropsCatchPrefab, this.transform.position, Quaternion.identity);
            player.ChangeHealth(1);
            player.PlaySound(audioClip);
            Destroy(this.gameObject);
        }
    }
}
