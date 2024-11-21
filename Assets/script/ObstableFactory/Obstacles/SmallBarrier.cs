using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBarrier : MonoBehaviour,IObstacle
{
    [SerializeField] private string obstacleName = "small barrier";
    public string ObstacleName {get => obstacleName; set => obstacleName = value;}
    public void Initialize(){
        Debug.Log(ObstacleName + " is created!");
    }
}
