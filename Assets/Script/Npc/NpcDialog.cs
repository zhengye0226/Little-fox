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
            dialogText.text = "�ޣ��װ���ruby��\r\n�ҵ�������Jambi ��\r\n�ҵĻ����˶����ˣ����ܰ����޺�������?";
        }
        else if (Misson.instance.StartMisson && !Misson.instance.missionCompelte)
        {
            dialogText.text = "���޸����ڵ����������Ŷ Ŀǰ����" + Misson.instance.fixRobotNum + "/" + Misson.instance.fixRobotSum + "��������";
        } else if (Misson.instance.StartMisson && Misson.instance.missionCompelte) {

            dialogText.text = "��ϲ�㣬������ɣ�ǰ����һ����ͼ��Կ�׸���";

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
