using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swiper : MonoBehaviour,IObstacle
{
    [SerializeField] private string obstacleName = "swiper";
    public string ObstacleName {get => obstacleName; set => obstacleName = value;}
    public void Initialize(){
        Debug.Log(ObstacleName + " is created!");
    }
}
