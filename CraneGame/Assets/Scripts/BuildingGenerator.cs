using NaughtyAttributes;

using System;
using UnityEngine;
using UnityEditor;
using System.Collections;

public enum FloorType { Plain, Windowed, Roof }

public class BuildingGenerator : MonoBehaviour
{
    [Serializable]
    public struct Floor
    {
        public FloorType floorType;
    }

    [SerializeField] GameObject _plainFloor, _windowedFloor, _roof;
    [SerializeField, Space(20)] Floor[] _floors;
    [SerializeField] Transform _buildingPlacement;

    [Button]
    public void CreateBuilding()
    {
        if (_buildingPlacement == null)
        {
            Debug.LogError("Building Generator requires a transform to place the building on.");
            return;
        }

        GenerateBuilding();
    }

    [Button]
    public void ClearBuilding()
    {
        if (_buildingPlacement.childCount == 0)
        {
            Debug.Log("There are no buildings in the transform to clear.");
            return;
        }
        else
        {
            for (int i = 0; i < _buildingPlacement.childCount; i++) DestroyImmediate(_buildingPlacement.GetChild(i).gameObject);
        }
    }

    private void GenerateBuilding()
    {
        GameObject newBuilding = new GameObject();
        newBuilding.transform.parent = _buildingPlacement.transform;
        newBuilding.transform.localPosition = Vector3.zero;
        Vector3 placement = newBuilding.transform.localPosition;

        _floors[_floors.Length - 1].floorType = FloorType.Roof;

        for (int i = 0; i < _floors.Length; i++)
        {
            Floor level = _floors[i];

            if (level.floorType == FloorType.Plain)
            {
                //creates new floor with assigned prefab
                GameObject newFloor = Instantiate(_plainFloor, newBuilding.transform.position, Quaternion.identity);
                newFloor.transform.parent = _buildingPlacement.transform;
            }
            else if (level.floorType == FloorType.Windowed)
            {
                GameObject newFloor = Instantiate(_windowedFloor, newBuilding.transform.position, new Quaternion(180, 0, 0, 0));
                newFloor.transform.parent = _buildingPlacement.transform;
            }
            else if (level.floorType == FloorType.Roof)
            {
                GameObject newFloor = Instantiate(_roof, newBuilding.transform.position, new Quaternion(180, 0, 0, 0));
                newFloor.transform.parent = _buildingPlacement.transform;
            }

            if ((i + 1) != (_floors.Length - 1))
                placement.y += 22;
            else
                placement.y += 14;

            newBuilding.transform.localPosition = placement;
        }

        DestroyImmediate(newBuilding);
    }
}
