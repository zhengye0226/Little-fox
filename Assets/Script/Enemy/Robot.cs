using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    //rebot move speed
    public float speed;
    private Rigidbody2D robotRigid;
    private Vector2 movePosition;
    //control robot move one direction time
    public float timer = 3.0f;
    //control robot move one direction true go on and false turn back
    bool move = true;
    //move Horzontal or vertical
    int xMove;
    // Start is called before the first frame update
    Animator anim;
    public bool isRepair;
    //drop prop when robot repair
    public GameObject props;
    public GameObject props1;
    private ParticleSystem SmokeEffect;
    public AudioSource audioSource;
    public AudioSource walkAudioSource;
    //���Ż������޸���Ч
    public AudioClip RobotFix;
    //���Ż������ܻ���Ч
    public AudioClip[] RobotHit;
    //���Ž�ɫ������Ч
    public AudioClip audioClip;
    public GameObject HitEffectPrefab;

    private void OnEnable()
    {

        xMove = Random.Range(0, 2);

        robotRigid = this.gameObject.GetComponent<Rigidbody2D>();

        anim = this.gameObject.GetComponent<Animator>();

        //Robot is break when it create
        isRepair = false;

        SmokeEffect = this.gameObject.GetComponentInChildren<ParticleSystem>();

    }
    private void Start()
    {
        
        Enemy._control.OnStart(this);

    }
    private void OnDestroy() {
        Enemy._control.SetOnDestroy(this);
    }
    private void Update()
    {

        robotRigid.simulated = !Enemy._control.EnemyIsDeadYorN(this);

        EnemyMovement();

        PlayMoveAnimation();

        if (movePosition != Vector2.zero && !Enemy._control.EnemyIsDeadYorN(this) && GameManger._instance.missionStart[(int)MissionType.FROG_MISSION])
        {

            if (!walkAudioSource.isPlaying)
            {
                walkAudioSource.Play();
                SmokeEffect.Play();
            }
        }
        else
        {
            SmokeEffect.Stop();
            walkAudioSource.Stop();
        }

        Vector2 position = this.transform.position;

        Enemy._control.SaveInUpdate(this.gameObject, this, position);
    }

    private void FixedUpdate()
    {
        EnemyGetMove();
    }

    /// <summary>
    /// robot move
    /// </summary>
    public void EnemyMovement()
    {
        movePosition = Vector2.zero;
        if (xMove == 0)
        {
            if (move)
            {
                movePosition += Vector2.up;
                timer -= Time.deltaTime;
                if (timer <= 0)
                    move = false;
            }
            else
            {
                movePosition += Vector2.down;
                timer += Time.deltaTime;
                if (timer >= 3)
                    move = true;
            }

        }
        else if (xMove == 1)
        {
            if (move)
            {
                movePosition += Vector2.left;
                timer -= Time.deltaTime;
                if (timer <= 0)
                    move = false;
            }
            else
            {
                movePosition += Vector2.right;
                timer += Time.deltaTime;
                if (timer >= 3)
                    move = true;
            }
        }
        //anim.SetBool("isRepair",true);
    }
    private void PlayMoveAnimation()
    {
        anim.SetBool("isRepair", Enemy._control.EnemyIsDeadYorN(this));

        anim.SetFloat("MoveX", movePosition.x);

        anim.SetFloat("MoveY", movePosition.y);

    }
    public void EnemyGetMove()
    {
        CancelPlayerMovement();
        robotRigid.AddForce(movePosition, ForceMode2D.Impulse);
    }

    private void CancelPlayerMovement()
    {
        robotRigid.AddForce(Vector2.up * -robotRigid.velocity.y, ForceMode2D.Impulse);
        robotRigid.AddForce(Vector2.right * -robotRigid.velocity.x, ForceMode2D.Impulse);
    }

    /// <summary>
    /// change PlayerHealth
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponentInChildren<PlayerHealth>();

        if (player != null && !player.isInvincible)
        {

            player.ChangeHealth(-1);
            player.PlaySound(audioClip);
            player.PlayerHitAnimation();
            player.isInvincible = true;

        }

    }

    /// <summary>
    /// Repair Robot
    /// </summary>
    public void IsRepair()
    {
        Vector2 position = this.gameObject.transform.position;
        //Play Effect
        Instantiate(HitEffectPrefab, position + Vector2.up * 0.4f, Quaternion.identity);

        isRepair = true;
        Enemy._control.SaveEnemy(this.gameObject, this, position, isRepair);

        //�����޸�����
        anim.SetBool("isRepair", isRepair);

        //���ɵ���
        GetProps();

        walkAudioSource.Stop();
        //������ű�������Ч
        int randomNum = Random.Range(0, 2);
        audioSource.PlayOneShot(RobotHit[randomNum]);
        //��ʱ�����޸���Ч
        Invoke("RobotFixSound", 0.9f);

        //�޸��Ļ���������1
        GameManger._instance.missionProgress[(int)MissionType.FROG_MISSION] += 1; 
    }
    /// <summary>
    /// After Robot repair 
    /// </summary>
    private void GetProps()
    {
        int x = Random.Range(0, 20);
        //���x<8���׳���ݮ�����16>x>=8���׳��ӵ��������x>=16��ʲôҲ����
        x = (x < 8) ? 0 : ((x < 16) ? 1 : 2);
        switch (x)
        {
            case 0:
                GameObject StrawBerry = Instantiate(props, this.gameObject.transform.position, Quaternion.identity);
                break;
            case 1:
                GameObject BulletBag = Instantiate(props1, this.gameObject.transform.position, Quaternion.identity);
                break;
            case 2:
                break;
        }
    }
    /// <summary>
    /// Fix Robot
    /// </summary>
    private void RobotFixSound()
    {
        audioSource.PlayOneShot(RobotFix);
    }
}
