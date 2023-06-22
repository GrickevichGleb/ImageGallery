using System;
using System.Collections;
using System.Net;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class DynamicScrollViewContent : MonoBehaviour
{
    [SerializeField] private TMP_Text debugText = null;
    
    [SerializeField] private ScrollRect scrollRect = null;
    [SerializeField] private Transform contentTransform = null;
    [SerializeField] private GameObject contentItemPrefab = null;
    [SerializeField] private int loadBatchSize = 4;

    //[SerializeField] private List<Texture2D> textureImagesList;

    private const string urlBase = "http://data.ikppbb.com/test-task-unity-data/pics/";
    private const string imgExt = ".jpg";

    private Coroutine _currentActiveRoutine = null;
    private bool _isBatchLoaded = false;
    private bool _isGotAllImages = false;
    
    private void Start()
    {
        LoadImagesOnStart();
    }
    

    public void OnScrollRectValueChanged()
    {
        if (scrollRect.normalizedPosition.y <= 0.3 && !_isGotAllImages)
        {
            if (_isBatchLoaded)
            {
                //debugText.text += " loadingBatch";
                _isBatchLoaded = false;
                StartCoroutine(LoadImages(loadBatchSize));
            }
        }
    }


    private void LoadImagesOnStart()
    {
        if (ImageHolder.Instance.textureImagesList.Count == 0)
        {
            StartCoroutine(LoadImages(12));
        }
        else
        {
            foreach (Texture2D texture in ImageHolder.Instance.textureImagesList)
            {
                DisplayImageInContent(texture);
                _isBatchLoaded = true;
            }
        }
    }

    private void DisplayImageInContent(Texture2D textureImage)
    {
        GameObject newContentItem = Instantiate(contentItemPrefab, contentTransform);
        if (newContentItem.TryGetComponent<ScrollViewItem>(out ScrollViewItem item))
        {
            item.SetImage(textureImage);
        }
    }
    

    private IEnumerator LoadImages(int batchSize)
    {
        string urlAddr;
        int iStart = ImageHolder.Instance.textureImagesList.Count + 1;
        int iEnd = iStart + batchSize;

        for (int i = iStart; i < iEnd; i++) //67
        {
            urlAddr = urlBase + i + imgExt;

            yield return DownloadImage(urlAddr);
            //debugText.text += " +";
        }
        
        //debugText.text += " bL";

        Debug.Log("Batch loaded");
        _isBatchLoaded = true;
    }


    private IEnumerator DownloadImage(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(new Uri(url));
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            //debugText.text += " connErr";
            Debug.Log("Connection error!");
        }
        else
        {
            Texture2D texture = null;

            try
            {
                texture = DownloadHandlerTexture.GetContent(request);
            }
            catch (Exception)
            {
                _isGotAllImages = true;
                Debug.Log("Exception!! on DownloadHandlerTexture.GetContent");
                yield break;
            }
            
            ImageHolder.Instance.textureImagesList.Add(texture);
            DisplayImageInContent(texture);
        }

    }
}