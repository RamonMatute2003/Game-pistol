using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulom_controller :MonoBehaviour {
    public float speed = 1.5f, limit=75f, random=0;
    public bool random_start=false;

    private void Awake() {
        if(random_start){
            random=Random.Range(0f,1f);
        }
    }

    void Update() {
        float angle=limit*Mathf.Sin(Time.time+random);
        transform.localRotation= Quaternion.Euler(0,0,angle);
    }
}
