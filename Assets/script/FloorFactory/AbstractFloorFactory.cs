using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public interface IFloor
{
    public string FloorName {get; set;}
    public void Initialize();
}
public abstract class FloorGenerator : MonoBehaviour
{
    public abstract GameObject GetFloor(Vector3 position, Quaternion quaternion);
}
