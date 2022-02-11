using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectRecycle : MonoBehaviour
{
    ParticleSystem ps;
    private void Awake() {
        ps = this.gameObject.GetComponent<ParticleSystem>();
    }
    private void EffectDestroy()
    {
        Destroy(this.gameObject);
    }

    private void Update() {
        if(ps.isStopped)
        {
            EffectDestroy();
        }
    }

}
