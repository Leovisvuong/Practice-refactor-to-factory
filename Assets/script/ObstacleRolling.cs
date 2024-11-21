using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRolling : MonoBehaviour
{
    private int speed;
    private int rollRotation = 0;
    public bool stop;
    public void SetStop(bool value){
        stop = value;
    }
    void Start(){
        speed = Random.Range(180,270);
        while(rollRotation == 0 ) rollRotation = Random.Range(-1,2);
    }
    void Update()
    {
        if(stop) return;
        transform.rotation *= Quaternion.Euler(0,speed * rollRotation * Time.deltaTime,0);
    }
}
