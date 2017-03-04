using System;
using Ultralpha;
using UnityEngine;

public class DefaultListItemDemo : MonoBehaviour
{
    [SerializeField] private ListView listView;

    private void Awake()
    {
        if (!listView)
        {
            listView = UITool.CreateDefaultListView();
            listView.transform.SetParent(transform, false);
        }
    }

    private void Start()
    {
        Player[] players =
        {
            new Player {ID = 10001, IP = "192.168.10.1", Name = "Areal"},
            new Player {ID = 10002, IP = "192.168.10.2", Name = "Balala"},
            new Player {ID = 10003, IP = "192.168.10.3", Name = "Cici"},
            new Player {ID = 10004, IP = "192.168.10.4", Name = "Dogei"},
            new Player {ID = 10005, IP = "192.168.10.5", Name = "Effin"},
            new Player {ID = 10006, IP = "192.168.10.6", Name = "Fol"},
            new Player {ID = 10007, IP = "192.168.10.7", Name = "Gri"},
            new Player {ID = 10008, IP = "192.168.10.8", Name = "Hsun"},
            new Player {ID = 10009, IP = "192.168.10.9", Name = "Ikla"},
            new Player {ID = 10010, IP = "192.168.10.10", Name = "Jojo"}
        };
        listView.reorderable = true;
        listView.selectable = true;
        listView.SelectionChanged += ListViewOnSelectionChanged;
        listView.Header = UITool.CreateDefaultHeader(Color.yellow, Color.black, "ID", "IP", "Name");
        listView.DataSource = players;
        //listView.DataSource = new object[] { 1, 0.618f, 0x11, '&', "This is lame!", false, DateTime.Now, players[0], players };
    }

    private void ListViewOnSelectionChanged()
    {
        Debug.Log(listView.SelectedItem.Data);
    }
}