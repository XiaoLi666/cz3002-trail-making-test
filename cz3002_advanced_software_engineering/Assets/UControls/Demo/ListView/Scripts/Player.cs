using System.ComponentModel;

public class Player : INotifyPropertyChanged
{
    private string _ip;
    public int ID { get; set; }
    public string Name { get; set; }

    public string IP
    {
        get { return _ip; }
        set
        {
            _ip = value;
            OnPropertyChanged("IP");
        }
    }
	public int AA { get; set; }

    //[ExcludeBinding]
    //public Info Info { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;


    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }

    public override string ToString()
    {
		return string.Format("ID: {0}, IP: {2}, Name: {1}, AA: {3}", ID, Name, IP, AA);
    }
}

//public class Info
//{
//    public int age;
//    public bool gender;
//
//    public string Desc
//    {
//        get { return "Description"; }
//    }
//
//    public Info(int age, bool gender)
//    {
//        this.age = age;
//        this.gender = gender;
//    }
//
//}