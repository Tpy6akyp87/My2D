using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class saveTime : MonoBehaviour
{
    public  Item item;
    private CharSCR player;
    private void Start()
    {
        player = FindObjectOfType<CharSCR>();
    }

    [ContextMenu("Save")]
    public void SaveField()
    {
        File.WriteAllText(Application.dataPath + "/data/saveTime.json", JsonUtility.ToJson(item));
    }
    [System.Serializable]
    public class Item
    {
        public float Time;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            item.Time = player.time;
            SaveField();
        }
    }
}
