using System;
using System.IO;
using UnityEngine;

namespace ManagerScript
{
    public class LocalDataManager : MonoBehaviour
    {
        public static int MaxClearedChapter { get; private set; }

        public static void ClearChapter(int chapter)
        {
            MaxClearedChapter = Mathf.Max(MaxClearedChapter, chapter);
            Save("MaxClearedChapter.data", i => i.ToString(), MaxClearedChapter);
        }

        private void Awake()
        {
            MaxClearedChapter = Load("MaxClearedChapter.data", int.Parse, -1);
        }

        private static void Save<T>(string path, Func<T, string> converter, T value)
        {
            try
            {
                File.WriteAllText(Application.persistentDataPath + "/" + path,converter(value));
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private static T Load<T>(string path, Func<string, T> converter, T defaultValue)
        {
            try
            {
                return converter(File.ReadAllText(Application.persistentDataPath + "/" + path));
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}
