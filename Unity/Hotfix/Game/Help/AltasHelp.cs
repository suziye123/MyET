using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
     public static class AltasHelp
    {
        /// <summary>
        /// 得到对应的图集
        /// </summary>
        /// <param name="AltasName"></param>
        /// <returns></returns>
        public static GameObject GetPukeAltas()
        {
            return Game.Scene.GetComponent<AltasComponent>().Get($"{AltasType.PukeAltas}");
        }

        /// <summary>
        /// 创建图集 图集对应的父物体
        /// </summary>
        public static GameObject GetAltasParent => Game.Scene.GetComponent<AltasComponent>().GetParentAltas;
    }
}
