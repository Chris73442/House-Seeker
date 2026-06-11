using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class TilemapFixer : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return null; // tunggu 1 frame

        Tilemap tm = GetComponent<Tilemap>();
        if (tm != null)
        {
            tm.RefreshAllTiles();
        }
    }
}