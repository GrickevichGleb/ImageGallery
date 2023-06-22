using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenLoader : MonoBehaviour
{
    [SerializeField] private float fadeTime = 2f;
    [SerializeField] private string viewSceneName = "Scene_Gallery";


    private void Start()
    {
        Screen.autorotateToPortraitUpsideDown = true;
        Screen.autorotateToPortrait = true;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
    }

    public void OpenGallery()
    {
        StartCoroutine(Transition());
    }
    
    
    private IEnumerator Transition()
    {
        DontDestroyOnLoad(gameObject);

        LoadScreen loadScreen = GameObject.FindObjectOfType<LoadScreen>();
        yield return loadScreen.LoadFadeOut(fadeTime);

        yield return SceneManager.LoadSceneAsync(viewSceneName);

        yield return loadScreen.LoadFadeIn(fadeTime);
        
        Destroy(gameObject);
    }
}
