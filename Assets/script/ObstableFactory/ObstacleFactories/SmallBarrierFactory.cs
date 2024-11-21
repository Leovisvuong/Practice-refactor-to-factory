using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBarrierFactory : ObstacleGenerator
{
    [SerializeField] private SmallBarrier prefabs;
    public override GameObject GetObstacle(Vector3 position, Quaternion quaternion){
        GameObject instance = Instantiate(prefabs.gameObject, position, quaternion);
        IObstacle obstacleInstance = instance.GetComponent<IObstacle>();
        obstacleInstance.Initialize();
        return instance;
    }
}
