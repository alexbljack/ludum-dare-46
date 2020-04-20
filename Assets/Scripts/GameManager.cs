using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float runSpeed = 1.5f;

    public Texture2D upCursor;
    public Texture2D downCursor;
    public Texture2D backCursor;
    public Texture2D forwardCursor;
    public Texture2D fireCursor;
    public Texture2D noAmmoCursor;

    public static string _currentCmd = "UP";

    public GameObject platformTop;
    public GameObject platformFront;

    public List<GameObject> lastFloor = new List<GameObject>();

    public GameObject player;

    void Start()
    {
        runSpeed = 1.5f;
        _currentCmd = "FORWARD";

        GenStartingFloor();
        SetCursorForCommand(_currentCmd);
        // StartCoroutine(SpawnRows());
    }

    void Update()
    {
        ProcessInput();
        runSpeed += Time.deltaTime * 0.03f;
    }

    void GenStartingFloor() {
        for (int x = -7; x < 8; x++) {
            lastFloor = SpawnFloorRow(x, new int[3]{0, 0, 1});
        }
    }

    IEnumerator SpawnRows() {
        int x = 8;
        while (true) {
            SpawnFloorRow(x, new int[3]{0, 0, 1});
            yield return new WaitForSeconds(GameManager.runSpeed * 0.25f);
        }
    }

    public List<GameObject> SpawnFloorRow(float x, int[] row) {
        List<GameObject> lastRow = new List<GameObject>();
        for (int i=0; i < 3; i++) {
            int platform = row[i];
            int y = -i;
            switch(platform) {
                case 0:
                lastRow.Add(Instantiate(platformTop, new Vector2(x, y), Quaternion.identity));
                break;
                case 1:
                lastRow.Add(Instantiate(platformFront, new Vector2(x, y), Quaternion.identity));
                break;
            }
        }
        return lastRow;
    }

    void SetCursorForCommand(string cmd) {
        switch (cmd) {
            case "UP":
            Cursor.SetCursor(upCursor, Vector2.zero, CursorMode.Auto);
            break;
            case "DOWN":
            Cursor.SetCursor(downCursor, Vector2.zero, CursorMode.Auto);
            break;
            case "BACK":
            Cursor.SetCursor(backCursor, Vector2.zero, CursorMode.Auto);
            break;
            case "FORWARD":
            Cursor.SetCursor(forwardCursor, Vector2.zero, CursorMode.Auto);
            break;
            case "FIRE":
            Cursor.SetCursor(fireCursor, Vector2.zero, CursorMode.Auto);
            break;
        }
    }

    void ProcessInput() {
        if (Input.GetKeyDown(KeyCode.W)) {
            _currentCmd = "UP";
            SetCursorForCommand(_currentCmd);
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            _currentCmd = "DOWN";
            SetCursorForCommand(_currentCmd);
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            _currentCmd = "BACK";
            SetCursorForCommand(_currentCmd);
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            _currentCmd = "FORWARD";
            SetCursorForCommand(_currentCmd);
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            _currentCmd = "FIRE";
            SetCursorForCommand(_currentCmd);
        }
    }
}
