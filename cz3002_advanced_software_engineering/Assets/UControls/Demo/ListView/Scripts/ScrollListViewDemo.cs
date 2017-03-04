using Ultralpha;
using UnityEngine;

public class ScrollListViewDemo : MonoBehaviour {

    [SerializeField]
    private ListView listView;

    private void Start()
    {
        Player[] players =
        {
            new Player {ID = 10001, IP = "192.168.10.1", Name = "Areal", AA = 1},
			new Player {ID = 10002, IP = "192.168.10.2", Name = "Balala", AA = 1},
			new Player {ID = 10003, IP = "192.168.10.3", Name = "Cici", AA = 1},
			new Player {ID = 10004, IP = "192.168.10.4", Name = "Dogei", AA = 1},
			new Player {ID = 10005, IP = "192.168.10.5", Name = "Effin", AA = 1},
			new Player {ID = 10006, IP = "192.168.10.6", Name = "Fol", AA = 1},
			new Player {ID = 10007, IP = "192.168.10.7", Name = "Gri", AA = 1},
			new Player {ID = 10008, IP = "192.168.10.8", Name = "Hsun", AA = 1},
			new Player {ID = 10009, IP = "192.168.10.9", Name = "Ikla", AA = 1},
			new Player {ID = 10010, IP = "192.168.10.10", Name = "Jojo0", AA = 1},
			new Player {ID = 10011, IP = "192.168.10.11", Name = "Jojo1", AA = 1},
			new Player {ID = 10012, IP = "192.168.10.12", Name = "Jojo2", AA = 1},
			new Player {ID = 10013, IP = "192.168.10.13", Name = "Jojo3", AA = 1},
			new Player {ID = 10014, IP = "192.168.10.14", Name = "Jojo4", AA = 1},
			new Player {ID = 10015, IP = "192.168.10.15", Name = "Jojo5", AA = 1},
			new Player {ID = 10016, IP = "192.168.10.16", Name = "Jojo6", AA = 1},
			new Player {ID = 10017, IP = "192.168.10.17", Name = "Jojo7", AA = 1},
			new Player {ID = 10018, IP = "192.168.10.18", Name = "Jojo9", AA = 1},
			new Player {ID = 10019, IP = "192.168.10.19", Name = "Jojo10", AA = 1},
			new Player {ID = 10020, IP = "192.168.10.20", Name = "Jojo11", AA = 1},
			new Player {ID = 10021, IP = "192.168.10.21", Name = "Jojo12", AA = 1},
			new Player {ID = 10022, IP = "192.168.10.22", Name = "Jojo13", AA = 1},
			new Player {ID = 10023, IP = "192.168.10.23", Name = "Jojo14", AA = 1},
			new Player {ID = 10024, IP = "192.168.10.24", Name = "Jojo15", AA = 1},
			new Player {ID = 10025, IP = "192.168.10.25", Name = "Jojo16", AA = 1},
			new Player {ID = 10026, IP = "192.168.10.26", Name = "Jojo17", AA = 1},
			new Player {ID = 10027, IP = "192.168.10.27", Name = "Jojo18", AA = 1},
			new Player {ID = 10028, IP = "192.168.10.28", Name = "Jojo19", AA = 1},
			new Player {ID = 10029, IP = "192.168.10.29", Name = "Jojo20", AA = 1},
			new Player {ID = 10030, IP = "192.168.10.30", Name = "Jojo21", AA = 1}
        };
        listView.reorderable = true;
        listView.selectable = true;
        listView.Header = UITool.CreateDefaultHeader(Color.yellow, Color.black, "ID", "IP", "Name", "AA");
        listView.DataSource = players;
    }
}
