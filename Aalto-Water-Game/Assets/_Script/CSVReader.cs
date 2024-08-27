using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVReader
{
    public static Dictionary<Vector2Int, TileType> ReadCSV(int levelIndex)
    {
        Dictionary<Vector2Int, TileType> mapDict = new Dictionary<Vector2Int, TileType>();
        
        TextAsset csvFile = Resources.Load<TextAsset>($"level{levelIndex}");
        string[] splitLine = csvFile.text.Split("/n"[0]);
        int mapHeight = splitLine.Length;

        for (int y = 0; y < mapHeight; y++)
        {
            string[] splitGrid = splitLine[y].Split(",");
            int mapWidth = splitGrid.Length;
            
            for (int x = 0; x < mapWidth; x++)
            {
                mapDict.Add(new Vector2Int(x, y), (TileType)Int32.Parse(splitGrid[x]));
                Debug.Log($"{new Vector2Int(x, y)}: {(TileType)Int32.Parse(splitGrid[x])}");
            }
        }
        
        return mapDict;
    }
}
