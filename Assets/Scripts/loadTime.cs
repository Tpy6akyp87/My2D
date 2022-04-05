using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class loadTime : MonoBehaviour
{
    public Item item;
    [ContextMenu("Load")]
    public void LoadField()
    {
        item = JsonUtility.FromJson<Item>(File.ReadAllText(Application.dataPath + "/data/saveTime.json"));
    }
    [System.Serializable]
    public class Item
    {
        public float Time;
    }
}