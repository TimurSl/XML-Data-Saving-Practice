using System.Xml.Serialization;

public class DataToSave
{
	public string Name { get; set; }
	public int Age { get; set; }
	public string Address { get; set; }
}

class Program
{
	static void Main(string[] args)
	{
		bool enabled = true;
		while (enabled)
		{
			Console.Write("What to you wanna do? (1 - load, 2 - save, 3 - exit): ");
			int choice = int.Parse(Console.ReadLine());
		
			switch (choice)
			{
				case 1:
					LoadData();
					break;
				case 2:
					SaveData();
					break;
				case 3:
					enabled = false;
					break;
				default:
					Console.WriteLine("Invalid choice!");
					break;
			}
		}
	}

	private static void SaveData()
	{
		DataToSave data = new DataToSave();
		data.Name = "John";
		data.Age = 25;
		data.Address = "Some address";
		
		XmlSerializer serializer = new XmlSerializer(typeof(DataToSave));
		using (FileStream stream = new FileStream("data.xml", FileMode.Create))
		{
			serializer.Serialize(stream, data);
		}
		
		Console.WriteLine("Data saved!");
	}

	private static void LoadData()
	{
		if (!File.Exists("data.xml"))
		{
			Console.WriteLine("File not found! Save data first!");
			return;
		}
		
		XmlSerializer serializer = new XmlSerializer(typeof(DataToSave));
		using (FileStream stream = new FileStream("data.xml", FileMode.Open))
		{
			DataToSave data = (DataToSave)serializer.Deserialize(stream);
			Console.WriteLine("Name: {0}", data.Name);
			Console.WriteLine("Age: {0}", data.Age);
			Console.WriteLine("Address: {0}", data.Address);
		}	
	}
}