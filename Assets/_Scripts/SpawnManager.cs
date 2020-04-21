using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject cloudPrefab;
    
    [SerializeField]
    private GameObject enemyPrefab;
    
    [SerializeField]
    private GameObject ammoPrefab;

    [SerializeField]
    private GameObject platformPrefab;

    private float[] _spawnPoints = new float[3]{ 0.4f, -0.6f, -1.6f };
    
    private Player _player;
    private List<GameObject> _lastRow = new List<GameObject>();

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        GenStartingFloor();

        StartCoroutine(SpawnClouds());
        StartCoroutine(SpawnEnemies());
    }

    void GenStartingFloor() {
        for (int x = -7; x < 8; x++) {
            _lastRow = SpawnFloorRow(x);
        }
    }

    public List<GameObject> SpawnFloorRow(float x) {
        List<GameObject> row = new List<GameObject>();
        row.Add(Instantiate(platformPrefab, new Vector2(x, 0), Quaternion.identity));
        row.Add(Instantiate(platformPrefab, new Vector2(x, -1), Quaternion.identity));
        row.Add(Instantiate(platformPrefab, new Vector2(x, -2), Quaternion.identity));
        return row;
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "platform") {
            float x = _lastRow[0].transform.position.x + 1;
            _lastRow = SpawnFloorRow(x);
            
            if (DiceRoll(_player.bullets + 8)) {
                SpawAmmo();
            }
        }
    }

    void SpawAmmo() {
        GameObject tile = _lastRow[Random.Range(0, _lastRow.Count)];
        GameObject ammo = Instantiate(ammoPrefab, tile.transform.position + new Vector3(0.5f, 0), Quaternion.identity);
        ammo.transform.parent = tile.transform;
    }

    bool DiceRoll(int corners) {
        return Random.Range(1, corners) == 1;
    }

    IEnumerator SpawnEnemies() {
        yield return new WaitForSeconds(4);
        while (true) {
            float y = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            Vector3 pos = new Vector3(6.5f, y);
            Instantiate(enemyPrefab, pos, Quaternion.identity); 
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
