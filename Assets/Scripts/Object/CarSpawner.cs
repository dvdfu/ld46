using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour {
    const float BUS_CHANCE = 0.1f;

    [SerializeField] GameObject carPrefab;
    [SerializeField] GameObject busPrefab;
    [SerializeField] Player player;
    [SerializeField] List<SpawningPoint> spawningPoints;

    int carsSpawned = 0;

    void Start() {
        StartCoroutine(SpawnRoutine());
    }

    float GetSpawnDelay() {
        return Mathf.Lerp(1.5f, 0.75f, carsSpawned / 100f);
    }

    IEnumerator SpawnRoutine() {
        while (true) {
            SpawningPoint spawningPoint = spawningPoints[UnityEngine.Random.Range(0, spawningPoints.Count)];
            GameObject prefab = UnityEngine.Random.value < BUS_CHANCE ? busPrefab : carPrefab;
            Car car = Instantiate(prefab, spawningPoint.pos, Quaternion.identity, transform).GetComponent<Car>();
            car.Init(player.transform, spawningPoint.dest);
            carsSpawned++;
            yield return new WaitForSeconds(GetSpawnDelay());
        }
    }

    [Serializable]
    struct SpawningPoint {
        // Position to spawn car
        public Vector2 pos;
        // Destination for a car spawned at this point
        public Vector2 dest;
    }

    private void OnDrawGizmos() {
        foreach (var spawningPoint in spawningPoints) {
            Vector3 spawnPointLoc = new Vector3(spawningPoint.pos.x, spawningPoint.pos.y, 0f);
            Vector3 destinationLoc = new Vector3(spawningPoint.dest.x, spawningPoint.dest.y, 0f);

            Gizmos.color = new Color(0.3f, 1.0f, 0.3f, 1f);
            Gizmos.DrawSphere(spawnPointLoc, 4f);
            Gizmos.color = new Color(1.0f, 0.3f, 0.3f, 1f);
            Gizmos.DrawSphere(destinationLoc, 2f);
            Gizmos.color = new Color(1.0f, 0.3f, 0.3f, 0.2f);
            Gizmos.DrawLine(spawnPointLoc, destinationLoc);
        }
    }
}
