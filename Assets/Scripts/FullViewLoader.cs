using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FullViewLoader : MonoBehaviour
{
    [SerializeField] private RawImage imageView = null;
    [SerializeField] private float fadeTime = 2f;
    [SerializeField] private string gallerySceneName = "Scene_Gallery";
    
    private void Start()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
        
        ImageHolder imageHolder = FindObjectOfType<ImageHolder>();
        SetImageView(imageHolder.GetImageTexture());
    }

    private void Update()
    {
        //Android back button support
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                BackButton();
            }
        }
    }


    public void BackButton()
    {
        StartCoroutine(Transition());
    }

    private void SetImageView(Texture2D texture)
    {
        imageView.texture = texture;
    }


    private IEnumerator Transition()
    {
        DontDestroyOnLoad(gameObject);
        
        LoadScreen loadScreen = GameObject.FindObjectOfType<LoadScreen>();
        yield return loadScreen.LoadFadeOut(fadeTime);
        
        imageView.texture = null;
        
        yield return SceneManager.LoadSceneAsync(gallerySceneName);
        yield return loadScreen.LoadFadeIn(fadeTime);
        
        Destroy(gameObject);
    }
}
