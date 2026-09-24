using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class TilemapFixer : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return null; 

        Tilemap tm = GetComponent<Tilemap>();
        if (tm != null)
        {
            tm.RefreshAllTiles();
        }
    }
}