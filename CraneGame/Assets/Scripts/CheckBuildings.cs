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

    public UnityEvent onAllDestroyed;

    public void DestroyedBuilding()
    {
        _buildingCount -= 1;

        UpdateText();

        if (_buildingCount <= 0)
        {
            onAllDestroyed.Invoke();
        }
    }

    private void Start()
    {
        onAllDestroyed.AddListener(() => StartCoroutine(nameof(AllBuildingsDestroyed)));

        UpdateText();
    }

    private void UpdateText()
    {
        _buildingGoal.text = $"Buildings Left: {_buildingCount}";
    }

    private void UpdateText(string newText)
    {
        _buildingGoal.text = $"{newText}";
    }

    private IEnumerator AllBuildingsDestroyed()
    {
        UpdateText("You destroyed all small buildings!\nGame Over");

        yield return new WaitForSeconds(15f);

        SceneManager.LoadScene(0);
    }
}