using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Third_person_camera: MonoBehaviour{
    public Vector3 offset;
    private Transform target;
    [Range(0,1)] public float lerp_value;
    public float sensitivity;

    private void Start() {
        target = GameObject.Find("Player").transform;
    }

    void LateUpdate(){
        transform.position = Vector3.Lerp(transform.position, target.position+offset, lerp_value);
        offset=Quaternion.AngleAxis(Input.GetAxis("Mouse X")*sensitivity, Vector3.up)*offset;

        transform.LookAt(target);
    }
}
