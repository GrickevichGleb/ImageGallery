using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScrollViewItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RawImage imageContent = null;
    
    private Texture2D _texture2D;
    public static event Action<Texture2D> OnViewItemClicked;
    
    public void SetImage(Texture2D imageTexture)
    {
        _texture2D = imageTexture;
        imageContent.texture = imageTexture;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnViewItemClicked?.Invoke(_texture2D);
    }
    
}
