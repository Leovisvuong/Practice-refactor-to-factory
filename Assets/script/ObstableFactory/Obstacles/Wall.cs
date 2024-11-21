using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour,IObstacle
{
    [SerializeField] private string obstacleName = "wall";
    public string ObstacleName {get => obstacleName; set => obstacleName = value;}
    public void Initialize(){
        Debug.Log(ObstacleName + " is created!");
    }
}
