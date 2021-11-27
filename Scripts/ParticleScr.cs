using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScr : MonoBehaviour
{
    ParticleSystem PS;

    private void Start()
    {
        PS = gameObject.GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if (PS.isStopped)
            Destroy(gameObject);
    }
}
