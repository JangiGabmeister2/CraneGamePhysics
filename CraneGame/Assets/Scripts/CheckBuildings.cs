using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CheckBuildings : MonoBehaviour
{
    [SerializeField] private int _buildingCount = 16;

    public UnityEvent onCompleteDestruction;

    public void DestroyedBuilding()
    {
        _buildingCount -= 1;
    }

    private void Start()
    {
        StartCoroutine(nameof(CheckForBuildings));
    }

    private IEnumerator CheckForBuildings()
    {
        while (_buildingCount !<= 0)
        {
            yield return null;
        }

        if (_buildingCount <= 0)
        {
            yield return new WaitForSeconds(15f);
            
            onCompleteDestruction.Invoke();
        }
    }
}