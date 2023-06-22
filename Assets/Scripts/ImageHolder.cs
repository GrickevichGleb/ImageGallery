using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageHolder : MonoBehaviour
{
    public static ImageHolder Instance;
    
    private Texture2D _imageTexture;
    public List<Texture2D> textureImagesList;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetImageTexture(Texture2D texture)
    {
        _imageTexture = texture;
    }

    public Texture2D GetImageTexture()
    {
        return _imageTexture;
    }
    
}
