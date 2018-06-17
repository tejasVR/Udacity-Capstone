using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectFade : MonoBehaviour {

    //private static GameObject _player;
    //public static TextMeshPro _textObj;
    //public static Image _imageObj;
    //public static GameObject _player;

    //public bool _isTextObj;
    //public bool _isImageObj;

    //[Tooltip("The distance from the player at which text will gradually fade out. Keep in mind that the text will fade depending on this distance value")]
    //public static float _objFadeOutWithDistance;

    //[Tooltip("The time in seconds in which the text will start to fade out")]
    //public float _objFadeOutTime;

    //public float _fadeSpeed;

    // Use this for initialization
    private void Start()
    {
        
    }

    //public static void Text

    public static void ImageFadeWithDistance(Image imageObj, float distanceToFade, bool deactivateAfterZeroAlpha)
    {
        //print("calling image fade distance method");

        //if (_player == null)
        //{
        //    GetPlayer();
        //}

        var distance = Vector3.Distance(imageObj.gameObject.transform.position, PlayerScript._playerEye.transform.transform.position);

        //print(distance);

        if (distance < distanceToFade)
        {
            //print("trying to fade image");
            
            var tempColor = imageObj.color;
            //print(tempColor.a);
            //tempColor.a = ((distanceToFade - distance) / distanceToFade) * .5f;
            tempColor.a = ((distanceToFade - distance) / distanceToFade);
            imageObj.color = tempColor;
        }
        else
        {
            if (deactivateAfterZeroAlpha)
            {
                imageObj.gameObject.SetActive(false);
                print("Deactivating Image object:" + imageObj.gameObject.name);
            }
        }


    }

    public static void TextFadeWithDistance(TextMeshPro textObj, float distanceToFade, bool deactivateAfterZeroAlpha)
    {
        //if (_player == null)
        //{
        //    GetPlayer();
        //}

        var distance = Vector3.Distance(textObj.gameObject.transform.position, PlayerScript._playerEye.transform.transform.position);
        if (distance < distanceToFade)
        {
            textObj.alpha = ((distance) / distanceToFade);   
        } else
        {
            if (deactivateAfterZeroAlpha)
            {
                textObj.gameObject.SetActive(false);
            }
        }
    }

    public static void ImageFadeOut(Image imageObj, float fadeSpeed, bool deactivateImageAfterZeroAlpha)
    {
        //if (_player == null)
        //{
        //    GetPlayer();
        //}

        var tempColor = imageObj.color;
        tempColor.a -= Time.deltaTime * fadeSpeed;
        imageObj.color = tempColor;

        if (tempColor.a <= 0 && deactivateImageAfterZeroAlpha)
        {
            imageObj.gameObject.SetActive(false);
        }
      
    }

    public static void TextFadeOut(TextMeshPro textObj, float fadeSpeed, bool deactivateTextAfterZeroAlpha)
    {
        //if (_player == null)
        //{
        //    GetPlayer();
        //}

        //print("text is fading");

        textObj.alpha -= Time.deltaTime * fadeSpeed;

        if (textObj.alpha <= 0 && deactivateTextAfterZeroAlpha)
        {
            textObj.gameObject.SetActive(false);
        }
    }


    public static void TextFadeIn(TextMeshPro textObj, float alphaTarget, float fadeSpeed)//, bool deactivateTextAfterTargetAlpha)
    {
        //if (_player == null)
        //{
        //    GetPlayer();
        //}

        //print("text is fading");

        if (textObj.alpha < alphaTarget)
            textObj.alpha += Time.deltaTime * fadeSpeed;

        //if (textObj.alpha >= alphaTarget && deactivateTextAfterTargetAlpha)
        //{
        //    textObj.gameObject.SetActive(false);
        //}
    }


    //public static void GetPlayer()
    //{
    //    _player = GameObject.Find("Camera (eye)");
    //    print("Getting player: " + _player.gameObject.name);
    //}

}
