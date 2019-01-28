using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace DanceFlow.Testing
{
    public class WebFileGrabberTester : MonoBehaviour
    {
        public Shader s;
        public string targetimageurl;
        public Image icon;

        [AutomaticEditorButton]
        public string RequestImage = "Request";

        public void Request()
        {
            icon.enabled = true;
            WebFileGrabber.instance.AddImageCallback(targetimageurl, ShaderCallback);
        }

        public void ShaderCallback(Texture t)
        {
            icon.material = new Material(s);
            icon.material.mainTexture = t;
        }
    }
}