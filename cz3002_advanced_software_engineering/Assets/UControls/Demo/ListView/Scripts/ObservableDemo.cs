using Ultralpha;
using UnityEngine;
using UnityEngine.UI;

public class ObservableDemo : MonoBehaviour
{
    private ObservableList<Player> players = new ObservableList<Player>();

    [SerializeField] private ListView listView;

    [SerializeField] private InputField id;
    [SerializeField] private InputField playerName;
    [SerializeField] private InputField ip;

    private void Awake()
    {
        if (!listView)
        {
            listView = UITool.CreateDefaultListView();
            listView.transform.SetParent(transform, false);
        }
        listView.selectable = true;
        listView.Header = UITool.CreateDefaultHeader(Color.cyan, Color.black, "ID", "Name", "IP");
        listView.DataSource = players;
    }

    public void AddPlayer()
    {
        if (!string.IsNullOrEmpty(id.text) && !string.IsNullOrEmpty(playerName.text) && !string.IsNullOrEmpty(ip.text))
            players.Add(new Player {ID = int.Parse(id.text), Name = playerName.text, IP = ip.text});
    }

    public void RemovePlayer()
    {
        if (!listView.SelectedItem)
            return;

        var player = listView.SelectedItem.Data as Player;
        if (player != null)
            players.Remove(player);
    }

    public void ModifyPlayer()
    {
        if(!listView.SelectedItem)
            return;

        var player = listView.SelectedItem.Data as Player;
        if (player != null)
        {
            if (!string.IsNullOrEmpty(id.text))
                player.ID = int.Parse(id.text);
            if (!string.IsNullOrEmpty(playerName.text))
                player.Name = playerName.text;
            if (!string.IsNullOrEmpty(ip.text))
                player.IP = ip.text;
        }
    }

}