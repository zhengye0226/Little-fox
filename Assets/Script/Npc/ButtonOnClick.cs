using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOnClick : MonoBehaviour
{
    private void Awake()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(ButtonOnclick);
    }
    // Start is called before the first frame update
    public void ButtonOnclick()
    {
        Misson.instance.MissionStart();
        //this.gameObject.SetActive(false);
        //Destroy(startMission);
    }
}
