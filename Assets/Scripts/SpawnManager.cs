using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameManager _gm;
    private List<GameObject> _lastRow = new List<GameObject>();

    public GameObject cloudPrefab;
    public GameObject enemyPrefab;
    public GameObject ammoPrefab;

    private float[] _spawnPoints; 

    private Warrior _player;

    void Start()
    {
        _spawnPoints = new float[3]{ 0.4f, -0.6f, -1.6f };
        _gm = GameObject.FindGameObjectWithTag("Main").GetComponent<GameManager>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Warrior>();

        StartCoroutine(SpawnClouds());
        StartCoroutine(SpawnEnemies());
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Floor") {
            float x = 8f;
            if (_lastRow.Count > 0) {
                x = _lastRow[0].transform.position.x + 1;
            } else {
                x = _gm.lastFloor[0].transform.position.x + 1;
            }
            _lastRow = _gm.SpawnFloorRow(x, new int[3]{0, 0, 1});
            
            // Spawn ammo box on random floor plate
            if (Random.Range(1, _player.bullets + 8) == 1) {
                GameObject tile = _lastRow[Random.Range(0, _lastRow.Count)];
                GameObject ammo = Instantiate(ammoPrefab, tile.transform.position + new Vector3(0.5f, 0), Quaternion.identity);
                ammo.transform.parent = tile.transform;
            }
        }
    }

    IEnumerator SpawnEnemies() {
        yield return new WaitForSeconds(4);
        while (true) {
            float y = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            Vector3 pos = new Vector3(6.5f, y);
            GameObject enemy = Instantiate(enemyPrefab, pos, Quaternion.identity); 
            // enemy.GetComponent<Enemy>().Init();
            yield return new WaitForSeconds(Random.Range(2f, 3f));
        }
    }

    IEnumerator SpawnClouds() {
        while (true) {
            Vector3 pos = new Vector3(7, Random.Range(1.5f, 3.5f));
            Instantiate(cloudPrefab, pos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(2f, 4f));
        }
    }
}
