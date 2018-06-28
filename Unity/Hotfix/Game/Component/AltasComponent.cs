using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [ObjectSystem]
    public class AltasComponentAwake: AwakeSystem<AltasComponent>
    {
        public override void Awake(AltasComponent self)
        {
            self.Awake();
        }
    }
    public class AltasComponent :Component
    {

        public readonly Dictionary<string,GameObject> TexNameObjDic = new Dictionary<string, GameObject>();
        public GameObject ParentAltas;

        public void Awake()
        {
            ParentAltas = GameObject.Find("Other");
            Log.Debug(ParentAltas.name);
        }

        public GameObject GetParentAltas => ParentAltas;

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="AltasName"></param>
        /// <param name="obj"></param>
        public void AddAltas(string AltasName, GameObject obj)
        {
            if (this.TexNameObjDic.ContainsKey(AltasName))
            {
                return;
            }
            if (obj==null)
            {
                return;
            }
            this.TexNameObjDic.Add(AltasName,obj);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="AltasName"></param>
        public void Remove(string AltasName)
        {
            GameObject obj = null;
            if (!this.TexNameObjDic.TryGetValue(AltasName, out obj))
            {
                return;
            }
            UnityEngine.Object.DestroyImmediate(obj);
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle($"{AltasName}");
            this.TexNameObjDic.Remove(AltasName);
        }

        /// <summary>
        /// 得到
        /// </summary>
        /// <param name="AltasName"></param>
        /// <returns></returns>
        public GameObject Get(string AltasName)
        {
            if (this.TexNameObjDic.ContainsKey(AltasName))
            {
                return null;
            }

            return TexNameObjDic[AltasName];
        }
    }
}
