using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeRoller : MonoBehaviour,IObstacle
{
    [SerializeField] private string obstacleName = "spike roller";
    public string ObstacleName {get => obstacleName; set => obstacleName = value;}
    public void Initialize(){
        Debug.Log(ObstacleName + " is created!");
    }
}
