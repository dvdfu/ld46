using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour {
    [SerializeField] List<Rect> spawningAreas;
    [SerializeField] GameObject treePrefab;
    [SerializeField] GameObject housePrefab;
    [SerializeField] GameObject apartmentPrefab;

    float lastSpawnedAt = 0;

    void Start() {
        for (int i = 0; i < 20; i++) SpawnBuilding(treePrefab);
        for (int i = 0; i < 25; i++) SpawnBuilding(housePrefab);
        for (int i = 0; i < 10; i++) SpawnBuilding(apartmentPrefab);
    }

    void SpawnBuilding(GameObject prefab) {
        Rect spawningArea = spawningAreas[Random.Range(0, spawningAreas.Count)];
        Vector2 spawnPoint = new Vector2(
            Random.Range(spawningArea.xMin + 16, spawningArea.xMax - 16),
            Random.Range(spawningArea.yMin + 16, spawningArea.yMax - 16)
        );
        spawnPoint.x = Mathf.Floor(spawnPoint.x / 32) * 32 + 16;
        spawnPoint.y = Mathf.Floor(spawnPoint.y / 32) * 32 + 16;
        Instantiate(prefab, spawnPoint, Quaternion.identity, transform);
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
