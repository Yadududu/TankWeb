using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.IO;
using System.Xml;


public class LoadABScene : MonoBehaviour {

    public static LoadABScene Get { get; private set; }
    public string sceneName;
    public string uri;
    public UnityEvent onLoadSuccess;
    public AssetBundleManifest manifest { get; private set; }
    public List<AssetBundle> assetBundles { get; private set; }//记录加载的AssetBundle,以便卸载
    public List<AssetBundle> assetBundleDepends { get; private set; }

    private LoadABScene() {
        Get = this;
    }
    void Start() {
        assetBundles = new List<AssetBundle>();
        assetBundleDepends = new List<AssetBundle>();
        StartCoroutine(GetWWWURI());
        DontDestroyOnLoad(this);
    }
    //读取AssetBundle地址
    IEnumerator GetWWWURI() {
        //获取地址,各个平台uri地址都不同,所以要用System.uri转换
        var _uri = new System.Uri(Path.Combine(Application.streamingAssetsPath, "Paths.xml"));
        UnityWebRequest request = UnityWebRequest.Get(_uri.AbsoluteUri);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError) {
            Debug.Log(request.error);
        } else {
            //StringReader sr = new StringReader(request.downloadHandler.text);
            //string result = sr.ReadToEnd();
            //sr.Close();
            //Debug.Log(result);

            //用XML读取文件
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(request.downloadHandler.text);
            XmlNodeList nodeList = xmlDoc.FirstChild.ChildNodes;
            foreach (XmlNode node in nodeList) {
                if (node.Name == "AssetBundles") {
                    XmlNodeList nodes = node.ChildNodes;
                    foreach (XmlNode n in nodes) {
                        if (n.Name == "path") uri = n.InnerText;
                        if (n.Name == "scene") sceneName = n.InnerText;
                    }
                }
            }

            if (!string.IsNullOrEmpty(uri)) {
                StartCoroutine(LoadAssetBundle());
            } else {
                Debug.Log("找不到该地址");
            }
        }
        request.Dispose();
    }
    //加载AssetBundle
    IEnumerator LoadAssetBundle() {
        //string uri = "http://localhost/AssetBundles/WebGL/";
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(uri + "WebGL");
        yield return request.SendWebRequest();
        AssetBundle abManifest = DownloadHandlerAssetBundle.GetContent(request);
        assetBundles.Add(abManifest);

        //网络加载依赖
        manifest = abManifest.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        //先加载依赖
        string[] depends = manifest.GetAllDependencies("scenes/" + sceneName);
        foreach (string name in depends) {
            //Debug.Log(name);
            UnityWebRequest requestDepend = UnityWebRequestAssetBundle.GetAssetBundle(uri + name);
            yield return requestDepend.SendWebRequest();
            AssetBundle abDepend = DownloadHandlerAssetBundle.GetContent(requestDepend);
            assetBundleDepends.Add(abDepend);
        }
        //再加载场景
        //foreach (string name in manifest.GetAllAssetBundles()) {
        //    Debug.Log(name);
        //}
        UnityWebRequest assets = UnityWebRequestAssetBundle.GetAssetBundle(uri + "scenes/" + sceneName);
        yield return assets.SendWebRequest();
        AssetBundle abAssets = DownloadHandlerAssetBundle.GetContent(assets);
        assetBundles.Add(abAssets);
        //foreach (string ab in assetBundleDepends[0].AllAssetNames()) {
        //    Debug.Log(ab);
        //}
        SceneManager.LoadScene(sceneName);
        onLoadSuccess.Invoke();
    }
}
