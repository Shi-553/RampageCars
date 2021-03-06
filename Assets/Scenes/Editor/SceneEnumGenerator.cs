using UnityEngine;
using System.Reflection;
using EditorScripts;
using UnityEditor;
using System.Linq;
using System.IO;
using System;
using System.Text;
using System.Collections.Generic;

namespace EditorScripts
{
    // 参考 https://github.com/risaki-masa/unity_sample/blob/master/Scripts/SceneTypeScriptCreator.cs
    [InitializeOnLoad]
    public static class SceneEnumGenerator
    {
        private const string MENU_ITEM_NAME = "Tools/Scene Type/Generate";
        private const string SCRIPT_FILE_NAME = "C#/SceneType.cs";
        static SceneEnumGenerator()
        {
            EditorBuildSettings.sceneListChanged += () => SceneListChanged(false);
        }

        private static bool CanCreate() => !EditorApplication.isPlaying && !EditorApplication.isCompiling;


        [MenuItem(MENU_ITEM_NAME)]
        private static void SceneListChangedFromMenu()
        {
            SceneListChanged(true);
        }

        /// <summary>
        /// シーンリストを変更した時の処理
        /// </summary>
        private static void SceneListChanged(bool isRefresh)
        {
            if (!CanCreate()) return;


            var thisAssets = AssetDatabase.FindAssets($"t:Script {nameof(SceneEnumGenerator)}");
            if (thisAssets.Length == 0)
            {
                return;
            }
            var thisAssetPath = AssetDatabase.GUIDToAssetPath(thisAssets[0]);
            var thisDirectory = Path.GetDirectoryName(thisAssetPath);
            var parentDirectory = Directory.GetParent(thisDirectory).FullName;
            var scriptPath = Path.Combine(parentDirectory, SCRIPT_FILE_NAME);

            var scriptString = CreateScriptString();

            File.WriteAllText(scriptPath, scriptString, Encoding.UTF8);
            if (isRefresh)
            {
                AssetDatabase.Refresh();
            }
            else
            {
                EditorApplication.playModeStateChanged += ChangedToRefresh;
            }
            Debug.Log("SceneTypeを生成しました。");
        }

        private static void ChangedToRefresh(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                AssetDatabase.Refresh();
                EditorApplication.playModeStateChanged -= ChangedToRefresh;
            }
        }

        private static string CreateScriptString()
        {
            var sceneNamesOriginal = EditorBuildSettings.scenes
                .Select(scene => Path.GetFileNameWithoutExtension(scene.path))
                .Distinct()
                .ToList();

            var sceneNames = new List<string>(sceneNamesOriginal);

            var builder = new StringBuilder()
                .AppendLine("/// <summary>")
                .AppendLine("/// シーンの種類を管理する列挙型")
                .AppendLine("/// <summary>")
                .AppendLine("public enum SceneType")
                .AppendLine("{");

            List<int> indexs = new();
            
            foreach (string type in typeof(SceneType).GetEnumNames())
            {
                if (sceneNames.Remove(type))
                {
                    int oldIndex = (int)Enum.Parse<SceneType>(type);

                    builder.AppendLine($"    {type} = {oldIndex},{Environment.NewLine}");

                    indexs.Add(oldIndex);
                }
            }

            int index = indexs.Max()+1;

            foreach (var name in sceneNames)
            {
                builder.AppendLine($"    {name} = {index},{Environment.NewLine}");
                index++;
            }

            builder.AppendLine("}");

            builder.AppendLine("public static class SceneTypeExtension");
            builder.AppendLine("{");
            builder.AppendLine("   public static int GetBuildIndex(this SceneType type)");
            builder.AppendLine("   {");

            builder.AppendLine("      return type switch                         ");
            builder.AppendLine("      {                                          ");


            for(int i=0;i< sceneNamesOriginal.Count;i++)
            {
                builder.AppendLine($"          SceneType.{sceneNamesOriginal[i]} => {i},              ");
            }

            builder.AppendLine("          _ => 0,                                ");
            builder.AppendLine("      };                                         ");
            builder.AppendLine("   }");
            builder.AppendLine("}");


            return builder.ToString();
        }
    }

}
