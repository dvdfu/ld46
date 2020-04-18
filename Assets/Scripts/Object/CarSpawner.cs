using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour {
    [SerializeField] GameObject carPrefab;
    [SerializeField] Player player;
    [SerializeField] float spawnInterval = 5;

    void Start() {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine() {
        while (true) {
            Vector3 position = Vector3.zero;
            switch (Random.Range(0, 4)) {
                case 0: position = Vector3.right * 220; break;
                case 1: position = Vector3.up * 120; break;
                case 2: position = Vector3.left * 220; break;
                case 3: position = Vector3.down * 120; break;
            }
            Car car = Instantiate(carPrefab, position, Quaternion.identity, transform.parent).GetComponent<Car>();
            car.Chase(player.transform);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
