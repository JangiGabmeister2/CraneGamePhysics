using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSequence : MonoBehaviour
{
    public GameObject canvas;
    
    public void HitBlock()
    {
        StartCoroutine(nameof(NextScene));
    }

    private IEnumerator NextScene()
    {
        canvas.SetActive(false);
        
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(0);
    }
}
