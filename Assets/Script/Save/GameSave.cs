using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSave : SubsComponent
{
    // Start is called before the first frame update
    public Dictionary<PLAYERINFOMATION, Object> playerSave = new Dictionary<PLAYERINFOMATION, Object>();

    public void OnSave()
    {
        print(Props._prop.PropsNumber[GAMEPROPS.STRAWBERRY]);

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
