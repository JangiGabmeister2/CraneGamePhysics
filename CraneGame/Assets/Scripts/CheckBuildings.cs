using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CheckBuildings : MonoBehaviour
{
    [SerializeField] private int _buildingCount = 16;
    [SerializeField] private Text _buildingGoal;

    public void DestroyedBuilding()
    {
        _buildingCount -= 1;

        _buildingGoal.text = $"Buildings Left: {_buildingCount}";
    }

    private void Start()
    {
        _buildingGoal.text = $"Buildings Left: {_buildingCount}";
    }

    private void Update()
    {
        if (_buildingCount == 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}