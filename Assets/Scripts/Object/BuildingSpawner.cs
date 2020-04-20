using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour {
    const float BUILDING_PADDING = 16;
    const float BUILDING_SPAWN_WIDTH = 32;

    [System.Serializable]
    struct SpawningArea {
        public enum Type {
            NW,
            NE,
            SW,
            SE,
            TOP,
            BOTTOM
        }

        public Type type;
        public Rect area;

        public Vector2 GetRandomSpawnPoint() {
            return new Vector2(Random.Range(area.xMin + BUILDING_PADDING * 1.5f, area.xMax - BUILDING_PADDING * 1.5f), Random.Range(area.yMin + BUILDING_PADDING * 1.5f, area.yMax - BUILDING_PADDING * 1.5f));
        }

        public Vector2 GetEdgeSpawnPoint() {
            switch (type) {
                case Type.NW:
                    if (Random.Range(0f, 1f) < 0.5f) {
                        return AlignToGrid(GetRightEdgeSpawn(), Mathf.Ceil, Mathf.Floor);
                    } else {
                        return AlignToGrid(GetBottomEdgeSpawn(), Mathf.Floor, Mathf.Floor);
                    }
                case Type.NE:
                    if (Random.Range(0f, 1f) < 0.5f) {
                        return AlignToGrid(GetLeftEdgeSpawn(), Mathf.Floor, Mathf.Floor);
                    } else {
                        return AlignToGrid(GetBottomEdgeSpawn(), Mathf.Floor, Mathf.Floor);
                    }
                case Type.SW:
                    if (Random.Range(0f, 1f) < 0.5f) {
                        return AlignToGrid(GetTopEdgeSpawn(), Mathf.Ceil, Mathf.Ceil);
                    } else {
                        return AlignToGrid(GetRightEdgeSpawn(), Mathf.Ceil, Mathf.Floor);
                    }
                case Type.SE:
                    if (Random.Range(0f, 1f) < 0.5f) {
                        return AlignToGrid(GetLeftEdgeSpawn(), Mathf.Floor, Mathf.Floor);
                    } else {
                        return AlignToGrid(GetTopEdgeSpawn(), Mathf.Floor, Mathf.Ceil);
                    }
                case Type.TOP:
                    float topR = Random.Range(0f, 1f);
                    if (topR < 0.33f) {
                        return AlignToGrid(GetLeftEdgeSpawn(), Mathf.Floor, Mathf.Floor);
                    } else if (topR < 0.66f) {
                        return AlignToGrid(GetBottomEdgeSpawn(), Mathf.Floor, Mathf.Floor);
                    } else {
                        return AlignToGrid(GetRightEdgeSpawn(), Mathf.Ceil, Mathf.Floor);
                    }
                case Type.BOTTOM:
                    float bottomR = Random.Range(0f, 1f);
                    if (bottomR < 0.33f) {
                        return AlignToGrid(GetLeftEdgeSpawn(), Mathf.Floor, Mathf.Floor);
                    } else if (bottomR < 0.66f) {
                        return AlignToGrid(GetTopEdgeSpawn(), Mathf.Floor, Mathf.Ceil);
                    } else {
                        return AlignToGrid(GetRightEdgeSpawn(), Mathf.Ceil, Mathf.Floor);
                    }
            }

            return Vector2.zero;
        }

        Vector2 AlignToGrid(Vector2 v, System.Func<float, float> fx, System.Func<float, float> fy) {
            return new Vector2(
                fx(v.x / 32) * 32,
                fy(v.y / 32) * 32
            );
        }

        Vector2 GetRightEdgeSpawn() {
            return new Vector2(Random.Range(area.xMax - BUILDING_SPAWN_WIDTH - BUILDING_PADDING, area.xMax - BUILDING_PADDING), Random.Range(area.yMin + BUILDING_PADDING, area.yMax - BUILDING_PADDING));
        }

        Vector2 GetLeftEdgeSpawn() {
            return new Vector2(Random.Range(area.xMin + BUILDING_PADDING, area.xMin + BUILDING_SPAWN_WIDTH + BUILDING_PADDING), Random.Range(area.yMin + BUILDING_PADDING, area.yMax - BUILDING_PADDING));
        }

        Vector2 GetBottomEdgeSpawn() {
            return new Vector2(Random.Range(area.xMin + BUILDING_PADDING, area.xMax - BUILDING_PADDING), Random.Range(area.yMin + BUILDING_PADDING, area.yMin + BUILDING_SPAWN_WIDTH + BUILDING_PADDING));
        }

        Vector2 GetTopEdgeSpawn() {
            return new Vector2(Random.Range(area.xMin + BUILDING_PADDING, area.xMax - BUILDING_PADDING), Random.Range(area.yMax - BUILDING_PADDING, area.yMax - BUILDING_SPAWN_WIDTH - BUILDING_PADDING));
        }
    }

    [SerializeField] List<SpawningArea> spawningAreas;
    [SerializeField] GameObject treePrefab;
    [SerializeField] GameObject housePrefab;
    [SerializeField] GameObject apartmentPrefab;
    
    struct Point {
        public float x, y;
    }

    List<Point> previouslySpawnedBuildings = new List<Point> {
        new Point {
            x = 0,
            y = 64
        },
        new Point {
            x = 0,
            y = -96
        }
    };

    bool spawningTrees = false;

    void Start() {
        for (int i = 0; i < 25; i++) SpawnBuilding(housePrefab);
        for (int i = 0; i < 10; i++) SpawnBuilding(apartmentPrefab);
        spawningTrees = true;
        for (int i = 0; i < 20; i++) SpawnBuilding(treePrefab);
    }

    void SpawnBuilding(GameObject prefab) {
        int tries = 0;

        while (tries++ < 5) {
            SpawningArea spawnArea = spawningAreas[Random.Range(0, spawningAreas.Count)];
            Rect spawningArea = spawnArea.area;
            Vector2 spawnPoint = spawningTrees ? spawnArea.GetRandomSpawnPoint() : spawnArea.GetEdgeSpawnPoint();

            Point p = new Point {
                x = spawnPoint.x,
                y = spawnPoint.y
            };
            if (previouslySpawnedBuildings.Contains(p)) {
                continue;
            }

            Instantiate(prefab, Jitter(spawnPoint), Quaternion.identity, transform);
            previouslySpawnedBuildings.Add(p);
            break;
        }
        
    }

    Vector2 Jitter(Vector2 v) {
        return v + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(0.8f, 4f);
    }

    void OnDrawGizmos() {
        Gizmos.color = new Color(1f, 0.3f, 0.3f, 1f);
        foreach (var spawningArea in spawningAreas) {
            var area = spawningArea.area;
            Gizmos.DrawLine(new Vector3(area.xMin, area.yMin), new Vector3(area.xMax, area.yMin));
            Gizmos.DrawLine(new Vector3(area.xMin, area.yMin), new Vector3(area.xMin, area.yMax));
            Gizmos.DrawLine(new Vector3(area.xMax, area.yMin), new Vector3(area.xMax, area.yMax));
            Gizmos.DrawLine(new Vector3(area.xMin, area.yMax), new Vector3(area.xMax, area.yMax));
        }
    }
}
