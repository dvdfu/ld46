using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour {
    [SerializeField] GameObject carPrefab;
    [SerializeField] Player player;
    [SerializeField] float spawnInterval = 5;
    [SerializeField] List<SpawningPoint> spawningPoints;

    void Start() {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine() {
        while (true) {
            SpawningPoint spawningPoint = spawningPoints[UnityEngine.Random.Range(0, spawningPoints.Count)];
            Car car = Instantiate(carPrefab, spawningPoint.pos, Quaternion.identity, transform.parent).GetComponent<Car>();
            car.Init(player.transform, spawningPoint.dest);
            yield return new WaitForSeconds(spawnInterval);
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
