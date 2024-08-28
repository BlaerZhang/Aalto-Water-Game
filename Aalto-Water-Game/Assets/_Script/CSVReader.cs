using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVReader
{
    public static Dictionary<Vector2Int, TileType> ReadCSV2(int levelIndex)
    {
        Dictionary<Vector2Int, TileType> mapDict = new Dictionary<Vector2Int, TileType>();

        TextAsset csvFile = Resources.Load<TextAsset>($"Level CSV/level{levelIndex}");
        string[] splitLine = csvFile.text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
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

    public static Dictionary<Vector2Int, TileType> ReadCSV(int levelIndex, out int mapWidth, out int mapHeight)
    {
        Dictionary<Vector2Int, TileType> mapDict = new Dictionary<Vector2Int, TileType>();

        mapWidth = 0;
        mapHeight = 0;

        // Load the CSV file from Resources folder
        TextAsset csvFile = Resources.Load<TextAsset>($"Level CSV/level{levelIndex}");
        if (csvFile == null)
        {
            Debug.LogError($"CSV file for level {levelIndex} not found.");
            return mapDict;
        }

        // Split the text into lines
        string[] splitLine = csvFile.text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        mapHeight = splitLine.Length;

        for (int y = 0; y < mapHeight; y++)
        {
            // Split each line into grid cells
            string[] splitGrid = splitLine[y].Split(',');
            mapWidth = splitGrid.Length;

            for (int x = 0; x < mapWidth; x++)
            {
                if (int.TryParse(splitGrid[x], out int tileTypeInt))
                    // Add to the dictionary
                    mapDict.Add(new Vector2Int(x, y), (TileType)tileTypeInt);
            }
        }

        return mapDict;
    }
}
