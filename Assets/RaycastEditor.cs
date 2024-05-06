using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(Raycast))] // Alterado para o nome do seu script, Raycast
public class RaycastEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Raycast raycast = (Raycast)target;
        //raycast.ambientColor = EditorGUILayout.ColorField("Ambient Color", raycast.ambientColor);

        if (DrawDefaultInspector())
        {
            if (raycast.autoUpdate)
            {
                raycast.RenderButtonClicked(); // Alterado para chamar diretamente a função RenderScene()
            }
        }

        if (GUILayout.Button("Render")) // Alterado o texto do botão para "Render" em vez de "Generate"
        {
            raycast.RenderButtonClicked(); // Alterado para chamar diretamente a função RenderScene()
        }
    }
}
