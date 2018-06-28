
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
                resourcesComponent.LoadBundle($"{ type}.unity3d");
                GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset($"{type}.unity3d", $"{type}");
                GameObject obj = UnityEngine.Object.Instantiate(bundleGameObject);
                UI ui = ComponentFactory.Create<UI, GameObject>(obj);
                ui.AddUiComponent<UIRoom_Component>();
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
        }
    }
}
