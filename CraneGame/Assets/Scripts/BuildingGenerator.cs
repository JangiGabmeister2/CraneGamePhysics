//Script is used to create a building, which is used for the city, which will be destroyed by the crane/wrecking ball.

using NaughtyAttributes;

using System;
using UnityEngine;

//floor types for variation
public enum FloorType { Plain, Windowed, Roof }

public class BuildingGenerator : MonoBehaviour
{
    [Serializable]
    public struct Floor
    {
        public FloorType floorType;
    }

    //prefabs to use for generator
    [SerializeField] GameObject _plainFloor, _windowedFloor, _roof;
    //an array where its items determine what type a specific floor is
    [SerializeField, Space(20)] Floor[] _floors;

    //makes a button in the inspector
    [Button]
    //destroys 'all' children of the generator (for if you don't like the size of the building)
    public void ClearBuilding()
    {
        //if there is nothing to destroy, returns message of such.
        if (transform.childCount == 0)
        {
            Debug.Log("There are no buildings in the transform to clear.");
            return;
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++) DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    [Button]
    //generates a building based oon each array item's floor type
    public void GenerateBuilding()
    {
        //creates a temporary placement for where the prefabs are instantiated.
        GameObject newBuilding = new GameObject();
        newBuilding.transform.parent = transform;
        newBuilding.transform.localPosition = Vector3.zero;
        Vector3 placement = newBuilding.transform.localPosition;

        //sets the last/top floor to be a roof type.
        _floors[_floors.Length - 1].floorType = FloorType.Roof;

        //cycles through array, creating a floor based on floor type.
        for (int i = 0; i < _floors.Length; i++)
        {
            Floor level = _floors[i];

            if (level.floorType == FloorType.Plain)
            {
                //creates new floor with assigned prefab.
                GameObject newFloor = Instantiate(_plainFloor, newBuilding.transform.position, Quaternion.identity);
                //sets the instantiate floor to be a child of the generator.
                newFloor.transform.parent = transform;
            }
            else if (level.floorType == FloorType.Windowed)
            {
                //the prefab for this floor is upside down, hence the quaternion flip.
                GameObject newFloor = Instantiate(_windowedFloor, newBuilding.transform.position, new Quaternion(180, 0, 0, 0));
                newFloor.transform.parent = transform;
            }
            else if (level.floorType == FloorType.Roof)
            {
                //same with this prefab.
                GameObject newFloor = Instantiate(_roof, newBuilding.transform.position, new Quaternion(180, 0, 0, 0));
                newFloor.transform.parent = transform;
            }

            //if the next floor is not the roof, sets the placement to be at a position where the next non-roof floor is to be placed without looking weird.
            if ((i + 1) != (_floors.Length - 1))
                placement.y += 22;
            else
                placement.y += 14;

            newBuilding.transform.localPosition = placement;
        }

        //destroys the temporary placement.
        DestroyImmediate(newBuilding);
    }
}
