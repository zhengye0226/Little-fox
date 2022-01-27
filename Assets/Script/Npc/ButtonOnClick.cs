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
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ButtonOnclick()
    {
        Misson.instance.StartMisson = true;
        Misson.instance.MissionStart();
        Destroy(this.gameObject);
        //Destroy(startMission);
    }
}
