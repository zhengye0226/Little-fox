using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum SubsComponents
{ 
    NONE,
    MOVE_PLAYERMOVE,
    CANCEL_MOVE,
    HEALTH_CHANGE,
    ATTACK
    }

    public enum PLAYERINFOMATION
    {
    CURRENTHEALTH,
    MAXHEALTH,
    NAME,
    POSITION,
    PROPNUM
    }

public  class SubsComponent : MonoBehaviour
{
    [Header("Awake Player Controller")]
    public PlayerController control;

    public Dictionary<int, List<string>> PlayerName = new Dictionary<int, List<string>>();

    public static SubsComponent _subs;
    // Start is called before the first frame update
    private void Awake()
    {
        #region 遍历枚举的方式
        //int i = 10;
        //object num = (object)i;
        //遍历枚举的方式
        //for(int i = 0; i < (int)PLAYERINFOMATION.PROPNUM; i++)
        //{
        //}

        //_subs = new SubsComponent();
        //foreach (PLAYERINFOMATION playerInformation in PLAYERINFOMATION.GetValues(typeof(PLAYERINFOMATION)))
        //{
        //    string information = playerInformation.ToString();
        //    switch (information)
        //    {
        //        case "CURRENTHEALTH": 
        //            return;
        //    }
        //}
        #endregion

        control = this.gameObject.GetComponentInParent<PlayerController>();
    }

    public virtual void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
    public virtual void OnFixedUpdate()
    {
        throw new System.NotImplementedException();
    }
    public virtual void ChenckOnTriggerEnter2D(Collider2D collider)
    {
        throw new System.NotImplementedException();
    }

}
