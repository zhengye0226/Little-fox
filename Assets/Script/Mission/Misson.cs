using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MissionType
{
    FROG_MISSION,
    NONE
}

public class Misson : MonoBehaviour
{
    public bool StartMisson = false;
    public GameObject[] missionStart = new GameObject[(int)MissionType.NONE];
    public Text dialogText;
    public bool missionCompelte = false;
    public int fixEnemyNum;
    public int fixEnemySum;
    public AudioClip audioClip;
    private Dictionary<int, ArrayList> missionList;
    private AudioSource audioSource;
    public static Misson instance
    {
        private set;
        get;
    }
    private void Awake()
    {

        instance = this;

        fixEnemyNum = 0;

        fixEnemySum = 1;

        audioSource = this.gameObject.GetComponent<AudioSource>();

        for(int i=0; i<(int)MissionType.NONE; i++)
        {
            GameManger._instance.missionStart.Add(i, StartMisson);

            GameManger._instance.missionCompelete.Add(i, missionCompelte);

            GameManger._instance.missionProgress.Add(i, fixEnemyNum);

            GameManger._instance.missionCondition.Add(i, fixEnemySum);
        }

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

        GameManger._instance.missionStart[(int)MissionType.FROG_MISSION] = true;

        AddRobot();

        GetDropProps();
    }

    public void MissionComplete()
    {
        if (GameManger._instance.missionProgress[(int)MissionType.FROG_MISSION] 
        >= GameManger._instance.missionCondition[(int)MissionType.FROG_MISSION])
        {
            if (!GameManger._instance.missionCompelete[(int)MissionType.FROG_MISSION])
            {
                audioSource.PlayOneShot(audioClip);
                GameManger._instance.missionCompelete[(int)MissionType.FROG_MISSION] = true;
            }
        }
    }

    private void GetDropProps()
    {
        Vector2 position = this.transform.position;
        //drop three BulletBag beside the Dialog 
        for (int i = 0; i < 3; i++)
        {
            Instantiate(missionStart[(int)MissionType.FROG_MISSION], position + Vector2.down * i, Quaternion.identity);
        }
    }

    private void AddRobot()
    {
        GameManger._instance.missionCondition[(int)MissionType.FROG_MISSION] = 6;

        // float[] enmeyPositionX = RandomPosition(6, -27, 12); 
        // float[] enmeyPositionY = RandomPosition(6, -8, 6); 
        
        // for(int i=0; i<6; i++)
        // {   
        //     Instantiate(Enemy._control.gameObjects[0], new Vector2(enmeyPositionX[i], enmeyPositionY[i]), Quaternion.identity);
        // }

        EnemyPosition enemyPosition = new EnemyPosition();
        enemyPosition.AddEnemy(0, 6);
        
    }

    // private float[] RandomPosition(int length, int minNumber, int maxNumber)
    // {
    //     float[] array = new float[length];

    //     for(int i=0; i<length; i++)
    //     {
    //         int number = Random.Range(minNumber, maxNumber);
    //         while(RemoveRepetition(array, number))
    //         {
    //             number = Random.Range(minNumber, maxNumber);
    //         }
    //         array[i] = number;
    //     }

    //     return array;
    // }

    // private bool RemoveRepetition(float[] array, int number)
    // {
    //     for(int i=0; i<array.Length; i++)
    //     {
    //         if(number == array[i])
    //         {
    //             return true;
    //         }
    //     }
    //     return false;
    // }

}
