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
        if (!GameManger._instance.missionStart[(int)MissionType.FROG_MISSION] && !GameManger._instance.missionCompelete[(int)MissionType.FROG_MISSION])
        {
            dialogText.text = "Hello Ruby.\r\n My name is Jambi and I have a problem with my robots. Can you help fix them?";
            startMission.gameObject.SetActive(true);
        }
        else if (GameManger._instance.missionStart[(int)MissionType.FROG_MISSION] && !GameManger._instance.missionCompelete[(int)MissionType.FROG_MISSION])
        {
            dialogText.text = "The mission progress is " + GameManger._instance.missionProgress[(int)MissionType.FROG_MISSION] + "/" + GameManger._instance.missionCondition[(int)MissionType.FROG_MISSION] + " You need Fix more Robot";
            startMission.gameObject.SetActive(false);
        } else if (GameManger._instance.missionStart[(int)MissionType.FROG_MISSION] && GameManger._instance.missionCompelete[(int)MissionType.FROG_MISSION]) {

            dialogText.text = "Congratulation, Here is a key that you can use to open next map";

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
}
