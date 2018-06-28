using System.Collections;
using System.Collections.Generic;
using ETModel;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(UIPanelConfig))]
public class UIPanelConfigEditor : Editor
{

    private string[] Layers = new string[] { WindowLayer.Bottom, WindowLayer.Medium, WindowLayer.Top, WindowLayer.TopMost, WindowLayer.UIHiden };
    private UIPanelConfig uiPanelConfig;
    private int TargetIndex = 1;
    private void OnEnable()
    {
        uiPanelConfig = (UIPanelConfig)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("选择窗口层级!");
        TargetIndex = EditorGUILayout.Popup("窗口Layer:",TargetIndex, Layers);
        this.uiPanelConfig.WindowLayer = this.Layers[TargetIndex];
        EditorGUILayout.LabelField($"当前层级为:{this.uiPanelConfig.WindowLayer}");
        EditorGUILayout.EndVertical();
    

    }
}
