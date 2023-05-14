using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreviewLoader : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Open());
    }

    private IEnumerator Open()
    {
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}