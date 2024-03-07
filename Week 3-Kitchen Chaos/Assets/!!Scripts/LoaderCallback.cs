using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool _isFirstFrame = true;

    private void Update()
    {
        if(_isFirstFrame)
        {
            _isFirstFrame = false;

            Loader.LoaderCallback();
        }
    }
}
