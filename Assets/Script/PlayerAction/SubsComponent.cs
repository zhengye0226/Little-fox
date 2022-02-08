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


public  class SubsComponent : MonoBehaviour
{
    [Header("Awake Player Controller")]
    public PlayerController control;
    
    // Start is called before the first frame update
    private void Awake()
    {
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
