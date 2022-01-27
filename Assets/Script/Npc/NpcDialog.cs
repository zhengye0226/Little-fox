using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcDialog : MonoBehaviour
{
    public GameObject dialogBox;
    public float showTime = 4.0f;
    private float CloseTime;
    private bool StartDialog;
    public Text dialogText;
    public GameObject PropsPrefab;
    public Button startMission;
    // Start is called before the first frame update 
    void Start()
    {
        dialogBox.SetActive(false);
        StartDialog = false;
        CloseTime = showTime;
        //startMission.GetComponent<Button>().onClick.AddListener(onClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (StartDialog)
        {
            CloseDialog();
        }
    }

    /// <summary>
    /// Open Dialog
    /// </summary>
    public void DisplayDialog()
    {
        if (!Misson.instance.StartMisson && !Misson.instance.missionCompelte)
        {
            dialogText.text = "噢，亲爱的ruby。\r\n我的名字是Jambi 。\r\n我的机器人都坏了，你能帮我修好他们吗?";
        }
        else if (Misson.instance.StartMisson && !Misson.instance.missionCompelte)
        {
            dialogText.text = "请修复场内的五个机器人哦 目前进度" + Misson.instance.fixRobotNum + "/" + Misson.instance.fixRobotSum + "继续加油";
        } else if (Misson.instance.StartMisson && Misson.instance.missionCompelte) {

            dialogText.text = "恭喜你，任务完成，前往下一个地图的钥匙给你";

        }
        StartDialog = true;
        dialogBox.SetActive(true);
    }
    /// <summary>
    /// Close Dialog
    /// </summary>
    private void CloseDialog()
    {
        CloseTime -= Time.deltaTime;
        if (CloseTime < 0)
        {
            dialogBox.SetActive(false);
            CloseTime = showTime;
            StartDialog = false;
        }
    }

    public void onClick()
    {
        Misson.instance.StartMisson = true;
        Misson.instance.MissionStart();
        StartDialog = true;
        CloseTime = showTime;
        startMission.GetComponentInChildren<Text>().text = "";
        //destroy(startmission);
    }
}
