using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObstacle
{
    public string ObstacleName {get;set;}
    public void Initialize();
}
public abstract class ObstacleGenerator : MonoBehaviour
{
    public abstract GameObject GetObstacle(Vector3 position, Quaternion quaternion);
}
