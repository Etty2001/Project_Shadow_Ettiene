using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotCapture : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            StartCoroutine(Capture());

    }

    private IEnumerator Capture()
    {
        yield return new WaitForEndOfFrame();

        zzTransparencyCapture.captureScreenshot("ItemScreenshot.png");

        Debug.Log("IMAGE CAPTURED");
    }
}
