using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Misson : MonoBehaviour
{
    public bool StartMisson= false;

    public GameObject missionStart;

    public Text dialogText;

    public bool missionCompelte;

    public int fixRobotNum;

    public int fixRobotSum;

    public AudioClip audioClip;

    private AudioSource audioSource;
    public static Misson instance {
        private set;
        get;
    }
    private void Awake()
    {

        instance = this;

        StartMisson = false;

        missionCompelte = false;

        fixRobotNum = 0;

        fixRobotSum = 5;

        audioSource = this.gameObject.GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MissionComplete();
    }

    //MissionStart
    public void MissionStart()
    {
        dialogText.text = "Thank you.Here are three BulletBag for you.You will need it";
        StartMisson = true;
        Vector2 position = this.transform.position;
        //drop three BulletBag beside the Dialog 
        for (int i = 0; i < 3; i++)
        {
            Instantiate(missionStart, position+Vector2.down*i, Quaternion.identity);
        }

        //load all robot
        foreach (var item in Enemy.RobotList.Keys)
        {
            Enemy.RobotList[item].SetActive(true);
        }
    }

    public void MissionComplete()
    {
        if (fixRobotNum >= fixRobotSum)
        {
            if (!missionCompelte)
            {
                audioSource.PlayOneShot(audioClip);
            }

            missionCompelte = true;
        }
    }
}
