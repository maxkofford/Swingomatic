using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// Grabs files from the web.
/// Allows dynamicly loading images to make the app launch fast then load images later.
/// </summary>
public class WebFileGrabber : MonoBehaviour
{
    public delegate void WebImageCallback(Texture2D webTexture);

    public static WebFileGrabber instance;

    public class ImageTodo
    {
        public string url;
        public WebImageCallback callback;
    }

    public void AddImageCallback(string url, WebImageCallback callback )
    {
        imageTodos.Enqueue(new ImageTodo() {url = url,callback = callback });
    }

    private Queue<ImageTodo> imageTodos = new Queue<ImageTodo>();

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        this.transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    
    }


    private bool isGrabbing = false;
    private IEnumerator GrabTexture(ImageTodo currentTodo)
    {
        Texture2D myTexture = new Texture2D(128, 128, TextureFormat.DXT1, false);

        if (currentTodo.url != null && currentTodo.url.Length > 1)
        {
            
                UnityWebRequest www = UnityWebRequestTexture.GetTexture(currentTodo.url);
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                    currentTodo.callback(myTexture);
                    isGrabbing = false;

                }
  
        }
        else
        {
            isGrabbing = false;
        }
    }
    private void Update()
    {
        if (imageTodos.Count > 0 && isGrabbing == false)
        {
            ImageTodo currentTodo = imageTodos.Dequeue();
            isGrabbing = true;
            StartCoroutine(GrabTexture(currentTodo));
        }
	}
}
