using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnSpawner : MonoBehaviour
{
    public GameObject columnPrefab; // 기둥 프리팹

    private float yMax = 2.4f;  // 최대로 올라갈 수 있는 y 높이
    private float yMin = -2.4f; // 최소로 내려갈 수 있는 y 높이
    private float x = 5f;       // 기둥이 생성될 위치 x 좌표

    private float timeSpawnMin = 1f;    // 기둥 생성 최소 시간
    private float timeSpawnMax = 1.5f;  // 기둥 생성 최대 시간
    private float spawnTime = 0f;       // 랜덤하게 생성된 시간
    private float lastSpawnTime = 0f;   // 최근에 생성된 시점

    private Vector3 poolPosition = new Vector3(100, 0, 0);  // 첫 기둥 생성 위치
    private GameObject[] columns;   // 생성된 기둥 인스턴스

    private int count = 3;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        columns = new GameObject[count];

        // 안보이는 곳에 세개를 미리 생성해
        for (int i = 0; i < count; i++)
        {
            columns[i] = Instantiate(columnPrefab, poolPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameState != GameState.Start) return;

        // 기둥을 생성할 시점이면
        if (Time.time >= lastSpawnTime + spawnTime)
        {
            lastSpawnTime = Time.time;

            // 랜덤 시간 생성
            spawnTime = Random.Range(timeSpawnMin, timeSpawnMax);

            // 랜덤 좌표 생성
            float y = Random.Range(yMin, yMax);

            //columns[index].SetActive(true);

            // 해당 좌표로 이동
            columns[index++].transform.position = new Vector2(x, y);

            if (index >= 3) index = 0;
        }
    }
}
