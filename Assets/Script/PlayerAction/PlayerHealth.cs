using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : SubsComponent
{
    // Start is called before the first frame update
    public float MaxinvincibleTime=2.0f;
    //play music when ruby was hit
    private AudioSource PlayMusic;

    //is invincible
    public bool isInvincible;
    private float invincibleTime;

    private void OnEnable()
    {
        control.SubComponetsDic.Add(SubsComponents.HEALTH_CHANGE, this);
        invincibleTime = MaxinvincibleTime;
        PlayMusic = this.gameObject.GetComponent<AudioSource>();
    }
    //private void Start()
    //{
    //    control.SubComponetsDic.Add(SubsComponents.HEALTH_CHANGE, this);
    //    //Debug.Log(this);
    //    MaxinvincibleTime = invincibleTime;
    //}

    public override void OnUpdate()
    {
        //Dead
        ReturnBack();
        //Invincible time
        IsInvincible();

        ChangeHealthBar();

    }
    /// <summary>
    /// ���Ŷ�Ӧ��Ƶ
    /// </summary>
    /// <param name="a">��Ƶ��Դ</param>
    public void PlaySound(AudioClip audioClip)
    {
        PlayMusic.PlayOneShot(audioClip);
    }
    /// <summary>
    /// Health Add or Decrease
    /// </summary>
    /// <param name="HealthChange"></param>
    public void ChangeHealth(int HealthChange)
    {
        GameManger._instance.currentHealth += HealthChange;
        ChangeHealthBar();
    }

    public void ChangeHealthBar()
    {
        //Ѫ���ı�
        UiHealthControl.instance.SetWidth(GameManger._instance.currentHealth/(float)GameManger._instance.maxHealth);
    }

    /// <summary>
    /// when ruby health==0,back to first position and change currentHealth to maxHealth
    /// </summary>
    /// <param name="healthAmount"></param>
    private void ReturnBack() 
    {
        //��ɫѪ������󷵻س�ʼλ��,Ѫ������
        if (GameManger._instance.currentHealth == 0)
        {
            control.transform.position = control.FirstPostion;
            GameManger._instance.currentHealth = GameManger._instance.maxHealth;
        }
        //make sure currentHealth between 0 to maxHealth
        //currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    /// <summary>
    /// IsInvincible
    /// </summary>
    public void IsInvincible()
    {
        //InvincibleTime Start
        if (isInvincible)
        {
            invincibleTime -= Time.deltaTime; 
            //InvincibleTime back to default
            if (invincibleTime <= 0) {
                invincibleTime = MaxinvincibleTime;
                isInvincible = false;
            }
        }
    }
    public void PlayerHitAnimation()
    {
        //Play Animation when Player was hitted
        control.anim.SetTrigger("Hit");
    }

    /// <summary>
    /// current equal MaxHealth
    /// </summary>
    /// <returns></returns>
    public bool IsMaxHealth()
    {
        if (GameManger._instance.currentHealth >= GameManger._instance.maxHealth)
            return true;
        return false;
    }
}
