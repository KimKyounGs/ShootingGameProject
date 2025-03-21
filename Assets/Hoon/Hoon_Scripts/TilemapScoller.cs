using UnityEngine;

public class TilemapScroller : MonoBehaviour
{
    public float scrollSpeed = 0.01f;         // 스크롤 속도
    public float tileHeight = 20f;         // 타일맵 세로 길이 (유니티 상 크기)
    public Transform[] tilemaps;           // 타일맵 2개 넣어줘야 함

    void Update()
    {
        foreach (Transform tile in tilemaps)
        {
            tile.position += Vector3.down * scrollSpeed * Time.deltaTime;

            if (tile.position.y <= -tileHeight)
            {
                float highestY = GetHighestTilemapY();
                tile.position = new Vector3(tile.position.x, highestY + tileHeight, tile.position.z);
            }
        }
    }

    float GetHighestTilemapY()
    {
        float highestY = tilemaps[0].position.y;
        foreach (Transform tile in tilemaps)
        {
            if (tile.position.y > highestY)
                highestY = tile.position.y;
        }
        return highestY;
    }
}
