using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Test
{
    public class JsonTest : MonoBehaviour
    {
        private App m_App;

        private void Start()
        {
            m_App = new App();
            m_App = LoadByJson();
            Debug.Log(m_App.AppNum);
            //SaveByJson();
        }

        private void SaveByJson()
        {
            var filePath = Application.dataPath + "/Resources/JSON" + "/Test.json";
            var saveJsonStr = JsonConvert.SerializeObject(m_App);
            var sw = new StreamWriter(filePath);
            sw.Write(saveJsonStr);
            sw.Close();
        }

        private static App LoadByJson()
        {
            var appGo = new App();
            var filePath = Application.dataPath + "/Resources/JSON" + "/Test.json";
            if (!File.Exists(filePath)) return appGo;
            var sr = new StreamReader(filePath);
            var str = sr.ReadToEnd();
            sr.Close();
            appGo = JsonConvert.DeserializeObject<App>(str);
            return appGo;
        }
    }
    
}