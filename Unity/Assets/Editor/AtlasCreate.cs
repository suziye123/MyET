using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class AtlasCreate
{
    //用来保存图片中的设置信息 比如用RGBA32压缩格式等
    public class TextureImporterSettings
    {
        public bool isReadable;//纹理信息在内存中是否可读
        public TextureImporterFormat textureFormat;//纹理的格式

        public TextureImporterSettings(bool isReadable, TextureImporterFormat textureFormat)
        {
            this.isReadable = isReadable;
            this.textureFormat = textureFormat;
        }
    }
    //用来保存单个图片的信息
    public class SpriteInfo
    {
        public string name;//图片的名字
        public Vector4 spriteBorder;//图片的包围盒(如果有的话)
        public Vector2 spritePivot;//图片包围盒中的中心轴(如果有的话)
        public float width;
        public float height;

        public SpriteInfo(string name, Vector4 border, Vector2 pivot, float w, float h)
        {
            this.name = name;
            spriteBorder = border;
            spritePivot = pivot;
            width = w;
            height = h;
        }
    }
    static float matAtlasSize = 2048;//最大图集尺寸
    static float padding = 1;//每两个图片之间用多少像素来隔开
    private static List<SpriteInfo> spriteList = new List<SpriteInfo>();
    [MenuItem("Assets/AtlasCreate")]
    static public void Init()
    {
        string assetPath;
        //根据我们的选择来获取选中物体的信息
        Object[] objs = Selection.GetFiltered(typeof(Texture), SelectionMode.DeepAssets);
        //判断图片命名的合法性
        for (int i = 0; i < objs.Length; i++)
        {
            Object obj = objs[i];
            if (obj.name.StartsWith(" ") || obj.name.EndsWith(" "))
            {
                string newName = obj.name.TrimStart(' ').TrimEnd(' ');
                Debug.Log(string.Format("rename texture'name old name : {0}, new name {1}", obj.name, newName));
                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(obj), newName);
            }
        }
        Texture2D[] texs = new Texture2D[objs.Length];//用来保存objs中的物体
        if (texs.Length <= 0)
        {
            Debug.Log("请先选择要合并的小图或小图的目录");
            return;
        }


        for (var i = 0; i < objs.Length; i++)
        {
            texs[i] = objs[i] as Texture2D;
            assetPath = AssetDatabase.GetAssetPath(texs[i]);
            AssetDatabase.ImportAsset(assetPath);//重新把图片导入内存，理论上unity工程中的资源在用到的时候，Unity会自动导入到内存，但有的时候却没有自动导入，为了以防万一，我们手动导入一次
        }

        //得到图片的设置信息
        TextureImporterSettings[] originalSets = GatherSettings(texs);

        //根据我们的需求 设置图片的一些信息.
        for (int i = 0; i < texs.Length; i++)
        {
            SetupTexture(texs[i], true, TextureImporterFormat.RGBA32);
        }
        //最终打成的图集路径，包括名字
        assetPath = "Assets/NN/Atlas.png";
        string outputPath = Application.dataPath + "/../" + assetPath;
        //主要的打图集代码
        PackAndOutputSprites(texs, assetPath, outputPath);
        //打出图集后在Unity选中它
        EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath(assetPath, typeof(Texture)));
    }
    //得到图片的设置信息
    static public TextureImporterSettings[] GatherSettings(Texture2D[] texs)
    {
        TextureImporterSettings[] sets = new TextureImporterSettings[texs.Length];
        for (var i = 0; i < texs.Length; i++)
        {
            var tex = texs[i];
            var assetPath = AssetDatabase.GetAssetPath(tex);
            TextureImporter imp = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            sets[i] = new TextureImporterSettings(imp.isReadable, imp.textureFormat);
            //如果图片由包围盒的话 记录包围盒信息
            if (imp.textureType == TextureImporterType.Sprite && imp.spriteBorder != Vector4.zero)
            {
                var spriteInfo = new SpriteInfo(tex.name, imp.spriteBorder, imp.spritePivot, tex.width, tex.height);
                spriteList.Add(spriteInfo);
            }
        }
        return sets;
    }
    //根据我们的需求 设置图片的一些信息.
    static public void SetupTexture(Texture2D tex, bool isReadable, TextureImporterFormat textureFormat)
    {
        var assetPath = AssetDatabase.GetAssetPath(tex);
        TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
        importer.isReadable = isReadable;//图片是否可读取它的内存信息
        importer.textureFormat = textureFormat;//图片的格式
        importer.mipmapEnabled = false;//是否生成mipmap文件
        importer.npotScale = TextureImporterNPOTScale.None;//用于非二次幂纹理的缩放模式
        importer.SaveAndReimport();//刷新图片
    }

    static public void PackAndOutputSprites(Texture2D[] texs, string atlasAssetPath, string outputPath)
    {
        Texture2D atlas = new Texture2D(1, 1);
        Rect[] rs = atlas.PackTextures(texs, (int)padding, (int)matAtlasSize);//添加多个图片到一个图集中,返回值是每个图片在图集(大图片)中的U坐标等信息
        // 把图集写入到磁盘文件，最终在磁盘上会有一个图片生成，这个图片包含了很多小图片
        File.WriteAllBytes(outputPath, atlas.EncodeToPNG());
        RefreshAsset(atlasAssetPath);//刷新图片

        //记录图片的名字，只是用于输出日志用;
        StringBuilder names = new StringBuilder();
        //SpriteMetaData结构可以让我们编辑图片的一些信息,想图片的name,包围盒border,在图集中的区域rect等
        SpriteMetaData[] sheet = new SpriteMetaData[rs.Length];
        for (var i = 0; i < sheet.Length; i++)
        {
            SpriteMetaData meta = new SpriteMetaData();
            meta.name = texs[i].name;
            meta.rect = rs[i];//这里的rect记录的是单个图片在图集中的uv坐标值
            //因为rect最终需要记录单个图片在大图片图集中所在的区域rect，所以我们做如下的处理
            meta.rect.Set(
                meta.rect.x * atlas.width,
                meta.rect.y * atlas.height,
                meta.rect.width * atlas.width,
                meta.rect.height * atlas.height
            );
            //如果图片有包围盒信息的话
            var spriteInfo = GetSpriteMetaData(meta.name);
            if (spriteInfo != null)
            {
                meta.border = spriteInfo.spriteBorder;
                meta.pivot = spriteInfo.spritePivot;
            }
            sheet[i] = meta;
            //打印日志用
            names.Append(meta.name);
            if (i < sheet.Length - 1)
                names.Append(",");
        }

        //设置图集的信息
        TextureImporter imp = TextureImporter.GetAtPath(atlasAssetPath) as TextureImporter;
        imp.textureType = TextureImporterType.Sprite;//图集的类型
        imp.textureFormat = TextureImporterFormat.AutomaticCompressed;//图集的格式
        imp.spriteImportMode = SpriteImportMode.Multiple;//Multiple表示我们这个大图片(图集)中包含很多小图片
        imp.mipmapEnabled = false;//是否开启mipmap
        imp.spritesheet = sheet;//设置图集中小图片的信息(每个图片所在的区域rect等)
        // 保存并刷新
        imp.SaveAndReimport();
        spriteList.Clear();
        //输出日志
        Debug.Log("Atlas create ok. " + names.ToString());
    }
    //刷新图片
    static public void RefreshAsset(string assetPath)
    {
        AssetDatabase.Refresh();
        AssetDatabase.ImportAsset(assetPath);
    }
    //得到图片的信息
    static public SpriteInfo GetSpriteMetaData(string texName)
    {
        for (int i = 0; i < spriteList.Count; i++)
        {
            if (spriteList[i].name == texName)
            {
                return spriteList[i];
            }
        }
        //Debug.Log("Can not find texture metadata : " + texName);
        return null;
    }

}
