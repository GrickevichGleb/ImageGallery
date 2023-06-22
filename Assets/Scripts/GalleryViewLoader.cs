using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GalleryViewLoader : MonoBehaviour
{
    [SerializeField] private float fadeTime = 2f;
    [SerializeField] private string viewSceneName = "Scene_FullView";

    
    void Start()
    {
        Screen.autorotateToPortraitUpsideDown = true;
        Screen.autorotateToPortrait = true;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        
        ScrollViewItem.OnViewItemClicked += ViewItemClickedHandler;
    }

    private void OnDestroy()
    {
        ScrollViewItem.OnViewItemClicked -= ViewItemClickedHandler;
    }
    

    private void ViewItemClickedHandler(Texture2D texture2D)
    {
        // GameObject persistentObj = GameObject.FindWithTag("PersistentObject");
        // persistentObj.GetComponentInChildren<ImageHolder>().SetImageTexture(texture2D);
        
        ImageHolder.Instance.SetImageTexture(texture2D);
        
        Debug.Log("Clicked on image");
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
