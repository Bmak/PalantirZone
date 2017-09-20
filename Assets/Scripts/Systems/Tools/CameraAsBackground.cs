using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AspectRatioFitter))]
public class CameraAsBackground : MonoBehaviour
{
    private RawImage image;
    //private WebCamTexture cam;
    private AspectRatioFitter arf;

    void Start()
    {
        arf = GetComponent<AspectRatioFitter>();
        image = GetComponent<RawImage>();

#if UNITY_EDITOR
        return;
#endif

#if UNITY_ANDROID || UNITY_IPHONE
#pragma warning disable 162
        /*
        cam = new WebCamTexture(Screen.width, Screen.height);
        image.material.mainTexture = cam;
        //image.texture = _blur.FastBlur(image.texture as Texture2D, _blur.radius, _blur.iterations);
        cam.Play();
        */
#pragma warning restore 162
#endif
    }

    void Update()
    {
#if UNITY_EDITOR
        return;
#endif
#pragma warning disable 162
        /*
        if (cam.width < 100)
        {
            return;
        }

        float cwneeded = -cam.videoRotationAngle;
        if (cam.videoVerticallyMirrored)
        {
            cwneeded += 180f;
        }
        image.rectTransform.localEulerAngles = new Vector3(0f, 0f, cwneeded);

        float videoRatio = (float)cam.width / (float)cam.height;
        arf.aspectRatio = videoRatio;

        if (cam.videoVerticallyMirrored)
        {
            image.uvRect = new Rect(1, 0, -1, 1);
        }
        else
        {
            image.uvRect = new Rect(0, 0, 1, 1);
        }
        */
#pragma warning restore 162
    }

    private void OnDestroy()
    {
        /*
        if (cam)
        {
            cam.Stop();
        }
        */
    }
}
