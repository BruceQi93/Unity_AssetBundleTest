/************************
 * Title:
 * Function：
 * 	－ 加载AssetBundle
 * Used By：	
 * Author:	qwt
 * Date:	
 * Version:	1.0
 * Description(Record):
 *
************************/

using UnityEngine;
using System.Collections;

public class LoadAsset : MonoBehaviour {

    void Start()
    {
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        //打包后的资源所在的文件夹
        string assetPath = "file://" + Application.dataPath + "/AssetBundle/";
        //要加载的目标资源的名称
        string realAssetBundleName = "cube";
        //加载总的AssetBundle清单文件
        WWW wwwManifest = WWW.LoadFromCacheOrDownload(assetPath + "AssetBundle", 0);
        //等待资源下载完成
        yield return wwwManifest;
        if (wwwManifest.error == null)
        {
            //得到AssetBundle的总清单
            AssetBundle manifestBundle = wwwManifest.assetBundle;
            //通过清单得到Manifest文件，里面是各个资源之间的依赖关系
            AssetBundleManifest manifest = (AssetBundleManifest)manifestBundle.LoadAsset("AssetBundleManifest");
            //卸载
            manifestBundle.Unload(false);
            //得到目标资源的依赖关系列表，是依赖关系资源的名字
            string[] dps = manifest.GetAllDependencies(realAssetBundleName);
            //保存所有依赖资源
            AssetBundle[] abs = new AssetBundle[dps.Length];
            for (int i = 0; i < dps.Length; i++)
            {
                //各个依赖资源所在路径
                string dUrl = assetPath + dps[i];
                //根据路径下载资源
                WWW dwww = WWW.LoadFromCacheOrDownload(dUrl, 0);
                //等待下载完成
                yield return dwww;
                abs[i] = dwww.assetBundle;
            }
            //加载目标资源文件
            WWW wwwCube = WWW.LoadFromCacheOrDownload(assetPath + realAssetBundleName, 0);
            //等待下载
            yield return wwwCube;
            if (wwwCube.error == null)
            {
                //得到cube资源列表
                AssetBundle cubeBundle = wwwCube.assetBundle;
                //通过资源列表下载资源
                GameObject cube = cubeBundle.LoadAsset(realAssetBundleName) as GameObject;
                Instantiate(cube);
                //卸载资源
                cubeBundle.Unload(false);
            }
        }
    }
}
