using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour{
    public GameObject shoot_start, shoot_prefab;
    public float speed_shoot;
    public float fireRate = 0.5f;
    public float nextFireTime = 0f;

    public void Fire(){
        GameObject shoot_temp = Instantiate(shoot_prefab,shoot_start.transform.position,shoot_start.transform.rotation) as GameObject;
        Rigidbody rb = shoot_temp.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward*speed_shoot);
        Destroy(shoot_temp,5.0f);
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject==shoot_prefab) {
            Destroy(collision.gameObject);
        }
    }
}
