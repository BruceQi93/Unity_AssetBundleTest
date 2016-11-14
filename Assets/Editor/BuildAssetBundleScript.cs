/************************
 * Title:
 * Function：
 * 	－ 打包Assetbundle
 * Used By：	
 * Author:	qwt
 * Date:	
 * Version:	1.0
 * Description(Record):
 *
************************/

using UnityEngine;
using System.Collections;
using UnityEditor;

public class BuildAssetBundleScript : MonoBehaviour {

    [MenuItem("AssetBundle/Build")]
    static void BuildAssetBundle()
    {
        BuildPipeline.BuildAssetBundles(Application.dataPath + "/AssetBundle");
    }
}
