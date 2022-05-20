using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerEx
{
    public List<GameObject> CitizenSpawnOrderList { get; private set; } = null;

    public void Init()
    {
        CitizenSpawnOrderList = new List<GameObject>(UI_CitizenSpawnButtons.BUTTON_NUM);
    }

    public GameObject Spawn(Define.WorldObject type, string path, Vector3 position, Transform parent = null)
    {
        var go = Managers.Resource.Instantiate(path, position, parent);

        switch (type)
        {
            case Define.WorldObject.Citizen:
                var citizen = go.GetComponent<CitizenController>();
                citizen.PrevPos = Managers.Tile.GetTilemap(Define.Tilemap.Global).WorldToCell(position);
                citizen.MoveType = Define.MoveType.Down;
                citizen.IsExit = false;
                Debug.Log(citizen.PrevPos);
                break;
        }

        return go;
    }

    public void Despawn(GameObject go)
    {
        var type = GetWorldObjectType(go);

        switch (type)
        {
            case Define.WorldObject.Citizen:
                break;
        }

        Managers.Resource.Destroy(go);
    }

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        var bc = go.GetComponent<BaseController>();
        if (bc == null)
        {
            return Define.WorldObject.Unknown;
        }

        return bc.WorldObjectType;
    }
}
