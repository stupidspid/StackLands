using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAroundService
{
    private static Vector3 CalculatePosition(Vector3 center, float radius, float angle)
    {
        float radian = angle * Mathf.Deg2Rad; 
        float x = center.x + radius * Mathf.Cos(radian); 
        float y = center.y + radius * Mathf.Sin(radian); 

        return new Vector3(x, y, center.z);
    }

    public static Vector3 GetNextPosition(int spawnCount, int currentObjectToSpawnIndex, Vector3 objectToSpawnAround,
        float radius)
    {
        float angleStep = 360f / spawnCount;
        float angle = currentObjectToSpawnIndex * angleStep; 
        return CalculatePosition(objectToSpawnAround, radius, angle);
    }
}
