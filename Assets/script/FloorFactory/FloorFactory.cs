using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorFactory : FloorGenerator
{
    [SerializeField] private Floor prefabs;
    public override GameObject GetFloor(Vector3 position, Quaternion quaternion){
        GameObject instance = Instantiate(prefabs.gameObject, position, quaternion);
        IFloor floorInstance = instance.GetComponent<IFloor>();
        floorInstance.Initialize();
        return instance;
    }
}
