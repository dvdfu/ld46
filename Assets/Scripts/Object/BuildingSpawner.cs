using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour {
    [SerializeField] List<Rect> spawningAreas;
    [SerializeField] float spawningRate = 5; // Seconds between spawning buildings
    [SerializeField] List<GameObject> buildings;
    [SerializeField] int initialBuildings = 10;

    float lastSpawnedAt = 0;

    void Start() {
        for (int i = 0; i < initialBuildings; i++) {
            SpawnBuilding();
        }
    }

    void Update() {
        if (Time.time - lastSpawnedAt >= spawningRate) {
            lastSpawnedAt = Time.time;
            SpawnBuilding();
        }
    }

    void SpawnBuilding() {
        Rect spawningArea = spawningAreas[Random.Range(0, spawningAreas.Count)];
        Vector2 spawnPoint = new Vector2(Random.Range(spawningArea.xMin, spawningArea.xMax), Random.Range(spawningArea.yMin, spawningArea.yMax));
        spawnPoint.x = Mathf.Floor(spawnPoint.x / 16) * 16 + 8;
        spawnPoint.y = Mathf.Floor(spawnPoint.y / 16) * 16;
        int buildingType = Random.Range(0, buildings.Count);
        Instantiate(buildings[buildingType], spawnPoint, Quaternion.identity, transform);
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
