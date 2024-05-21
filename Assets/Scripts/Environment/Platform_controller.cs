using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_controller : MonoBehaviour{
    public Rigidbody platform_rigidbody;
    public Transform[] platform_positions;
    public float platform_speed, wait_time;
    private int next_position=1;
    private int current_position = 0;
    public bool move_to_netx=true;
    public bool rotate_on=false;
    public Vector3 rotateSpeed;

    void Update(){
        move_platform();
    }

    public void move_platform() {
        if(move_to_netx){
            StopCoroutine(wait_for_move(0));
            platform_rigidbody.MovePosition(Vector3.MoveTowards(platform_rigidbody.position,platform_positions[next_position].position,platform_speed*Time.deltaTime));
        }

        if(rotate_on) {
            Quaternion deltaRotation = Quaternion.Euler(rotateSpeed*Time.deltaTime);
            platform_rigidbody.MoveRotation(platform_rigidbody.rotation*deltaRotation);
        }

        if(Vector3.Distance(platform_rigidbody.position, platform_positions[next_position].position)<=0){
            StartCoroutine(wait_for_move(wait_time));
            current_position = next_position;
            next_position++;

            if(next_position>platform_positions.Length-1){
                next_position=0;
            }
        }
    }

    IEnumerator wait_for_move(float time){
        move_to_netx=false;
        yield return new WaitForSeconds(time);
        move_to_netx=true;
    }
}