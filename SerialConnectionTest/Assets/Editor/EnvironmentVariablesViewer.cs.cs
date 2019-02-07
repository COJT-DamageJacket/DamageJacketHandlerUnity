namespace EnvironmentVariablesViewer
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEditor;
    using UnityEditorInternal;

    /// <summary>
    /// 環境変数を表示するEditorWindow
    /// </summary>
    public class EnvironmentVariablesViewer : EditorWindow
    {
        const float LabelWidth = 180f;
        const float LabelSpace = 32f;
        private List<ReorderableList> reorderableLists;
        private Vector2 scrollPos = Vector2.zero;


        [MenuItem("Tools/Environment Variables Viewer")]
        static void Open()
        {
            GetWindow<EnvironmentVariablesViewer>();
        }

        void OnGUI()
        {
            GUILayout.Space(1f);
            if (this.reorderableLists == null)
            {
                this.reorderableLists = new List<ReorderableList>();
                this.reorderableLists.Add(CreateReorderableList("環境変数(Process)", EnvironmentVariableTarget.Process));
                this.reorderableLists.Add(CreateReorderableList("環境変数(User)", EnvironmentVariableTarget.User));
                this.reorderableLists.Add(CreateReorderableList("環境変数(Machine)", EnvironmentVariableTarget.Machine));
            }

            this.scrollPos = EditorGUILayout.BeginScrollView(this.scrollPos);
            foreach (var list in this.reorderableLists)
            {
                list.DoLayoutList();
            }
            EditorGUILayout.EndScrollView();
        }

        /// <summary>
        /// ReorderableListの作成
        /// </summary>
        static ReorderableList CreateReorderableList(string headerText, EnvironmentVariableTarget target)
        {
            var list = new List<EnvironmentVariable>();
            foreach (System.Collections.DictionaryEntry item in Environment.GetEnvironmentVariables(target)) // ユーザー環境変数
            {
                list.Add(new EnvironmentVariable { Key = item.Key, Value = item.Value });
            }

            var reorderableList = new ReorderableList(list, typeof(EnvironmentVariable));

            // ヘッダー
            Rect headerRect = default(Rect);
            reorderableList.drawHeaderCallback = (rect) =>
            {
                headerRect = rect;
                EditorGUI.LabelField(rect, headerText);
            };

            // フッター
            reorderableList.drawFooterCallback = (rect) =>
            {
                rect.y = headerRect.y + 3f;
                ReorderableList.defaultBehaviours.DrawFooter(rect, reorderableList);
            };

            // リスト要素
            reorderableList.drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                rect.y += 3f;
                rect.height -= 6f;

                var kRect = new Rect(rect); // key用ラベル
                kRect.width = LabelWidth;

                var vRect = new Rect(rect); // Value用ラベル
                vRect.width = vRect.width - kRect.width - LabelSpace;
                vRect.x += kRect.width + LabelSpace;

                EditorGUI.TextField(kRect, (string)list[index].Key);
                EditorGUI.TextField(vRect, (string)list[index].Value);
            };
            return reorderableList;
        }

        private class EnvironmentVariable
        {
            public object Key;
            public object Value;
        }
    }
}