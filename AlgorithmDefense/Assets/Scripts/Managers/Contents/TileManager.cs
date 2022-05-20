using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager
{
    private Grid _grid = null;
    private Dictionary<Define.Tilemap, Tilemap> _tilemaps = new Dictionary<Define.Tilemap, Tilemap>();

    public void Init()
    {
        _grid = UnityEngine.Object.FindObjectOfType<Grid>();

        var names = Enum.GetNames(typeof(Define.Tilemap));
        for (int i = 0; i < names.Length; i++)
        {
            var tilemap = Util.FindChild<Tilemap>(_grid.gameObject, $"Tilemap_{names[i]}", recursive: false);
            if (tilemap != null)
            {
                Define.Tilemap type = (Define.Tilemap)Enum.Parse(typeof(Define.Tilemap), names[i]);
                _tilemaps.Add(type, tilemap);
            }
        }
    }

    public Grid GetGrid() => _grid;

    public Tilemap GetTilemap(Define.Tilemap type)
    {
        Tilemap tilemap = null;
        _tilemaps.TryGetValue(type, out tilemap);
        return tilemap;
    }

    public TileBase GetTile(Define.Tilemap type, Vector3Int cellPos)
    {
        var tilemap = GetTilemap(type);
        return tilemap.GetTile(cellPos);
    }

    public Vector3 GetCellToWorld(Define.Tilemap type, Vector3Int cellPos)
    {
        var tilemap = GetTilemap(type);
        return tilemap.CellToWorld(cellPos);
    }

    public Vector3 GetCellCenterToWorld(Define.Tilemap type, Vector3Int cellPos)
    {
        var tilemap = GetTilemap(type);
        return tilemap.GetCellCenterWorld(cellPos);
    }

    public void SetTile(Define.Tilemap type, Vector3Int cellPos, TileBase tileBase)
    {
        var tilemap = GetTilemap(type);
        tilemap.SetTile(cellPos, tileBase);
    }
}
