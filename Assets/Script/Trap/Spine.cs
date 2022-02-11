using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spine : MonoBehaviour
{
    public AudioClip audioClip;
    // Start is called before the first frame update

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponentInChildren<PlayerHealth>();
        if (player != null&& !player.isInvincible)
        {
            player.ChangeHealth(-1);
            player.PlaySound(audioClip);
            player.PlayerHitAnimation();
            player.isInvincible = true;
        }
    }
}
