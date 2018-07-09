
using System;
using ETModel;
using UnityEngine;


namespace ETHotfix
{
    [UIFactory(UIType.UIRoom)]
    public class UIRoom_Factory : IUIFactory
    {
        public UI Create(Scene scene, string type, GameObject parent)
        {
            try
            {
                Debug.Log("UIRoom_Factory");
                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                AltasComponent altasComponent=Game.Scene.GetComponent<AltasComponent>();
                resourcesComponent.LoadBundle($"{ type}.unity3d");
                GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset($"{type}.unity3d", $"{type}");
                GameObject obj = UnityEngine.Object.Instantiate(bundleGameObject);
                UI ui = ComponentFactory.Create<UI, GameObject>(obj);
                ui.AddUiComponent<UIRoom_Component>();

                //加载扑克图集
                resourcesComponent.LoadBundle($"{AltasType.PukeAltas}.unity3d");
                GameObject AltasGameObject = (GameObject)resourcesComponent.GetAsset($"{AltasType.PukeAltas}.unity3d", $"{AltasType.PukeAltas}");
                GameObject Altas = UnityEngine.Object.Instantiate(AltasGameObject, altasComponent.ParentAltas.transform);
                altasComponent.AddAltas(AltasType.PukeAltas, Altas);


                //加载字体图集
                resourcesComponent.LoadBundle($"{AltasType.FontAltas}.unity3d");
                GameObject FontAltasGameObject = (GameObject)resourcesComponent.GetAsset($"{AltasType.FontAltas}.unity3d", $"{AltasType.FontAltas}");
                GameObject FontAltas = UnityEngine.Object.Instantiate(FontAltasGameObject, altasComponent.ParentAltas.transform);
                altasComponent.AddAltas(AltasType.FontAltas, FontAltas);

                return ui;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return null;
            }
        }
        public void Remove(string type)
        {
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle($"{type}.unity3d");
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle($"{AltasType.PukeAltas}.unity3d");
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle($"{AltasType.FontAltas}.unity3d");
        }
    }
}
