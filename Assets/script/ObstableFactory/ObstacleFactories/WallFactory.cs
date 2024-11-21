using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFactory : ObstacleGenerator
{
    [SerializeField] private Wall prefabs;
    public override GameObject GetObstacle(Vector3 position, Quaternion quaternion){
        GameObject instance = Instantiate(prefabs.gameObject, position, quaternion);
        IObstacle obstacleInstance = instance.GetComponent<IObstacle>();
        obstacleInstance.Initialize();
        return instance;
    }
}
