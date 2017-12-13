using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AssetBundles;

public class OneCardUI : MonoBehaviour
{
    private ReferenceCollector rc;
    
    private RawImage imgBK;

    private Text Lab_Name;

    private CardData curData;
    public CardData CurData
    {
        get {
            return curData;
        }
        set {
            curData = value;
            ShowData();
        }
    }

    private void Awake()
    {
        rc = gameObject.GetComponent<ReferenceCollector>();
        if (rc != null)
        {
            imgBK = rc.Get<RawImage>("ImgBK",false);
            Lab_Name = rc.Get<Text>("LabName",false);
            
            ShowData();
        }
        else
        {
            Debuger.LogError("onecardUI 引用没有");
        }
    }

    void ShowData()
    {
        if (Lab_Name != null && curData != null)
        {
//            StartCoroutine(AddUiToCanvas((Texture texture) =>
//            {
//                imgBK.texture = texture;
//            }));
            StartCoroutine(AddUiToCanvas(imgBK));

            Lab_Name.text = curData.CardName;
        }
    }
    public delegate void EndGetSp(Texture sp);
//    IEnumerator AddUiToCanvas(EndGetSp endCall)
    IEnumerator AddUiToCanvas(RawImage img)
    {
        AssetBundleLoadAssetOperation request =
            AssetBundleManager.LoadAssetAsync("cardimg.unity3d", curData.Race+"_"+curData.ImgID, typeof(Texture));
        if (request == null)
            yield break;
        yield return StartCoroutine(request);
        Texture sp = request.GetAsset<Texture>();
//        img.sprite = sp;

        img.texture = sp;
//        if (endCall != null)
//        {
//            endCall(sp);
//        }
    }
}