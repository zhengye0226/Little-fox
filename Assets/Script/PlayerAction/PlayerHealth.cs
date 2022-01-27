using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : SubsComponent
{
    // Start is called before the first frame update

    //ruby maxHealthy
    private int maxHealth =5;
    //ruby currentHealth
    private int currentHealth = 5;
    //invincibleTime
    public float MaxinvincibleTime=2.0f;
    //play music when ruby was hit
    private AudioSource PlayMusic;

    //is invincible
    public bool isInvincible;
    private float invincibleTime;
    public int CurrentHealth
    {
        set { currentHealth = value; }
        get { return currentHealth; }
    }

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
        //死亡归零
        ReturnBack();
        //无敌倒计时
        IsInvincible();
    }
    /// <summary>
    /// 播放对应音频
    /// </summary>
    /// <param name="a">音频资源</param>
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
        CurrentHealth = CurrentHealth + HealthChange;
        //血量改变
        UiHealthControl.instance.SetWidth(CurrentHealth/(float)maxHealth);

    }

    /// <summary>
    /// when ruby health==0,back to first position and change currentHealth to maxHealth
    /// </summary>
    /// <param name="healthAmount"></param>
    private void ReturnBack() 
    {
        //角色血量归零后返回初始位置,血量充满
        if (currentHealth == 0)
        {
            control.transform.position = control.FirstPostion;
            currentHealth = maxHealth;
            //血条回满
            UiHealthControl.instance.SetWidth(CurrentHealth / (float)maxHealth);
        }
        //保证角色血量在[0,maxHealth]之间
        //currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    /// <summary>
    /// 判断角色是否受伤害
    /// </summary>
    public void IsInvincible()
    {
        //如果正在受到伤害，无敌倒计时启动
        if (isInvincible)
        {
            invincibleTime -= Time.deltaTime; 
            //倒计时小于等于0表示无敌状态时间到
            if (invincibleTime <= 0) {
                invincibleTime = MaxinvincibleTime;
                isInvincible = false;
            }
        }
    }
    public void PlayerHitAnimation()
    {
        //播放无敌动画
        control.anim.SetTrigger("Hit");
    }

    /// <summary>
    /// current equal MaxHealth
    /// </summary>
    /// <returns></returns>
    public bool IsMaxHealth()
    {
        if (currentHealth >= maxHealth)
            return true;
        return false;
    }
}
