using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class LoadScreen : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private Coroutine _currentlyActiveLoadFade = null;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }


    public Coroutine LoadFadeOut(float time)
    {
        return LoadFade(target: 1f, time);
    }
    
    public Coroutine LoadFadeIn(float time)
    {
        return LoadFade(target: 0f, time);
    }
    

    private Coroutine LoadFade(float target, float time)
    {
        if (_currentlyActiveLoadFade != null)
        {
            StopCoroutine(_currentlyActiveLoadFade);
        }

        _currentlyActiveLoadFade = StartCoroutine(LoadFadeRoutine(target, time));
        return _currentlyActiveLoadFade; //Waits for coroutine to finish
    }

    private IEnumerator LoadFadeRoutine(float target, float time)
    {
        while (!Mathf.Approximately(_canvasGroup.alpha, target))//Alpha != 1.0
        {
            _canvasGroup.alpha = Mathf.MoveTowards(
                _canvasGroup.alpha,
                target,
                Time.deltaTime / time);

            yield return null;
        }
    }
}
