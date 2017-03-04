using System.ComponentModel;

public class PlayerRecordUnit : INotifyPropertyChanged {
	public string m_date;
	public string m_type;
	public string m_timeUsed;
	public string m_errorRate;

	public PlayerRecordUnit(string date, string type, string time_used, string error_rate) {
		m_date = date;
		m_type = type;
		m_timeUsed = time_used;
		m_errorRate = error_rate;
	}

	public event PropertyChangedEventHandler PropertyChanged;
	protected virtual void OnPropertyChanged(string propertyName) {
		PropertyChangedEventHandler handler = PropertyChanged;
		if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
	}

	public override string ToString()
	{
		return string.Format("Date: {0}, Type: {1}, Time Used: {2}, Error Rate: {3}", m_date, m_type, m_timeUsed, m_errorRate);
	}
}