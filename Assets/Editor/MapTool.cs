using System.Collections.Generic;
using System.IO;
using System.Linq;
using Game;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    /// <summary>
    /// 扩展 MapMaker
    /// </summary>
    [CustomEditor(typeof(MapMaker))]
    public class MapTool : UnityEditor.Editor
    {
        private MapMaker m_MapMaker;

        //关卡文件列表
        private List<FileInfo> m_FileList = new List<FileInfo>();

        private string[] m_FileNameList;

        //当前关卡索引
        private int m_SelectIndex = -1;

        /// <summary>
        /// 拓展绘制 mapmaker面板
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (!Application.isPlaying) return;
            m_MapMaker=MapMaker.instance;
            EditorGUILayout.BeginHorizontal();
            m_FileNameList = GetNames(m_FileList);
            var currentIndex = EditorGUILayout.Popup(m_SelectIndex, m_FileNameList);
            if (currentIndex != m_SelectIndex) //选择对象发生改变
            {
                m_SelectIndex = currentIndex;
                //实例化地图
                m_MapMaker.InitMap();
                //加载当前level文件
                m_MapMaker.LoadLevelFile(
                    MapMaker.LoadLevelInfoFile(m_FileNameList[m_SelectIndex]));
            }

            if (GUILayout.Button("读取关卡列表"))
            {
                //读取关卡列表
                LoadLevelFiles();
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("恢复地图编辑器默认状态"))
            {
                m_MapMaker.RecoverTowerPoint();
            }

            if (GUILayout.Button("清除怪物路点"))
            {
                m_MapMaker.ClearMonsterPath();
            }

            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("保存当前关卡数据文件"))
            {
                m_MapMaker.SaveLevelFileByJson();
            }
        }

        private void LoadLevelFiles()
        {
            ClearList();
            m_FileList = GetLevelFiles();
            
        }

        private void ClearList()
        {
            m_FileList.Clear();
            m_SelectIndex = -1;
        }

        private static List<FileInfo> GetLevelFiles()
        {
            //读所有json
            var files = Directory.GetFiles(Application.dataPath + "/Resources/JSON/Level/",
                "*.json");

            return files.Select(t => new FileInfo(t)).ToList();
        }

        private static string[] GetNames(IEnumerable<FileInfo> fileInfos)
        {
            return fileInfos.Select(fileInfo => fileInfo.Name).ToArray();
        }
    }
}