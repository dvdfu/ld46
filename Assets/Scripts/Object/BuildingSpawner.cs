using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour {
    [SerializeField] List<Rect> spawningAreas;
    [SerializeField] float spawningRate = 5; // Seconds between spawning buildings
    [SerializeField] List<GameObject> buildings;

    float lastSpawnedAt = 0;

    void Update() {
        if (Time.time - lastSpawnedAt >= spawningRate) {
            lastSpawnedAt = Time.time;

            Rect spawningArea = spawningAreas[Random.Range(0, spawningAreas.Count)];
            Vector2 spawnPoint = new Vector2(Random.Range(spawningArea.xMin, spawningArea.xMax), Random.Range(spawningArea.yMin, spawningArea.yMax));
            
            int buildingType = Random.Range(0, buildings.Count);
            Instantiate(buildings[buildingType], spawnPoint, Quaternion.identity);
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = new Color(1f, 0.3f, 0.3f, 1f);
        foreach (var area in spawningAreas) {
            Gizmos.DrawLine(new Vector3(area.xMin, area.yMin), new Vector3(area.xMax, area.yMin));
            Gizmos.DrawLine(new Vector3(area.xMin, area.yMin), new Vector3(area.xMin, area.yMax));
            Gizmos.DrawLine(new Vector3(area.xMax, area.yMin), new Vector3(area.xMax, area.yMax));
            Gizmos.DrawLine(new Vector3(area.xMin, area.yMax), new Vector3(area.xMax, area.yMax));
        }
        
    }
}
