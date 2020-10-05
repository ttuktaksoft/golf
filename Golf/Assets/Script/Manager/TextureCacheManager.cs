using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureCacheManager : MonoBehaviour
{
    public static TextureCacheManager _instance = null;
    public static TextureCacheManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TextureCacheManager>() as TextureCacheManager;
            }
            return _instance;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public List<string> ImageLoadReadyUrl = new List<string>();
    public bool ImageLoadProgress = false;
    private Dictionary<string, Texture2D> imageCache = new Dictionary<string, Texture2D>();
    private Dictionary<string, WWW> requestCache = new Dictionary<string, WWW>();
    public void init()
    {
        
    }
    public void AddLoadImageURL(string url)
    {
        if (url == "")
            return;

        ImageLoadReadyUrl.Add(url);
    }
    public void LoadImage()
    {
        ImageLoadProgress = true;
        StartCoroutine(LoadTexture());
    }

    private IEnumerator LoadTexture()
    {
        Debug.Log("LoadTexture Start");
        ImageLoadProgress = true;
        TKManager.Instance.ShowLoading();
        for (int i = 0; i < ImageLoadReadyUrl.Count; i++)
        {
            yield return this.LoadTexture(ImageLoadReadyUrl[i]);
        }
        TKManager.Instance.HideLoading();
        Debug.Log("LoadTexture End");
        ImageLoadProgress = false;
        ImageLoadReadyUrl.Clear();
    }

    private IEnumerator LoadTexture(string url)
    {
        if (!imageCache.ContainsKey(url))
        {
            int retryTimes = 3; // Number of time to retry if we get a web error
            WWW request;
            do
            {
                --retryTimes;
                if (!this.requestCache.ContainsKey(url))
                {
                    // Create a new web request and cache is so any additional
                    // calls with the same url share the same request.
                    this.requestCache[url] = new WWW(url);
                }

                request = this.requestCache[url];
                yield return request;

                // Remove this request from the cache if it is the first to finish
                if (this.requestCache.ContainsKey(url) && this.requestCache[url] == request)
                {
                    this.requestCache.Remove(url);
                }
            } while (request.error != null && retryTimes >= 0);

            // If there are no errors add this is the first to finish,
            // then add the texture to the texture cache.
            if (request.error == null && !this.imageCache.ContainsKey(url))
            {
                imageCache.Add(url, request.texture);
            }
        }

        //if (callback != null)
        //{
        //    // By the time we get here there is either a valid image in the cache
        //    // or we were not able to get the requested image.
        //    Texture2D texture = null;
        //    imageCache.TryGetValue(url, out texture);
        //    callback();
        //}
    }

    public Texture2D GetTexture(string url)
    {
        if (imageCache.ContainsKey(url))
            return imageCache[url];

        var imgSprite = (Sprite)Resources.Load("logo_2", typeof(Sprite));
        return imgSprite.texture;
    }
}
