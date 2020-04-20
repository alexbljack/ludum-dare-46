using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDeath : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GameOver());
    }

    void Update()
    {
        transform.Translate(Vector3.left * GameManager.runSpeed * Time.deltaTime);
    }

    IEnumerator GameOver() {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
