using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour, IFloor
{
    [SerializeField] private string floorName = "floor";
    public string FloorName {get => floorName; set => floorName = value;}
    public void Initialize(){
        Debug.Log(floorName + " is created!");
    }
}