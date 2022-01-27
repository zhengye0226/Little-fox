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
        //��������
        ReturnBack();
        //�޵е���ʱ
        IsInvincible();
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
        CurrentHealth = CurrentHealth + HealthChange;
        //Ѫ���ı�
        UiHealthControl.instance.SetWidth(CurrentHealth/(float)maxHealth);

    }

    /// <summary>
    /// when ruby health==0,back to first position and change currentHealth to maxHealth
    /// </summary>
    /// <param name="healthAmount"></param>
    private void ReturnBack() 
    {
        //��ɫѪ������󷵻س�ʼλ��,Ѫ������
        if (currentHealth == 0)
        {
            control.transform.position = control.FirstPostion;
            currentHealth = maxHealth;
            //Ѫ������
            UiHealthControl.instance.SetWidth(CurrentHealth / (float)maxHealth);
        }
        //��֤��ɫѪ����[0,maxHealth]֮��
        //currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    /// <summary>
    /// �жϽ�ɫ�Ƿ����˺�
    /// </summary>
    public void IsInvincible()
    {
        //��������ܵ��˺����޵е���ʱ����
        if (isInvincible)
        {
            invincibleTime -= Time.deltaTime; 
            //����ʱС�ڵ���0��ʾ�޵�״̬ʱ�䵽
            if (invincibleTime <= 0) {
                invincibleTime = MaxinvincibleTime;
                isInvincible = false;
            }
        }
    }
    public void PlayerHitAnimation()
    {
        //�����޵ж���
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
