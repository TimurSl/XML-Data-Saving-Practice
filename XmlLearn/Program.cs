using System.Xml.Serialization;

public class DataToSave
{
	public string Name { get; set; }
	public int Age { get; set; }
	public string Address { get; set; }
	
	public List<string> Friends { get; set; } = new List<string>();

	public Dictionary<string, SomeStructure> SomeData { get; set; } = new Dictionary<string, SomeStructure> ();
}

public struct SomeStructure
{
	public string Name { get; set; }
	public int Age { get; set; }
	
	public SomeStructure(string name, int age)
	{
		Name = name;
		Age = age;
	}
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
		
		data.Friends.Add("Friend 1");
		data.Friends.Add("Friend 2");
		data.Friends.Add("Friend 3");
		
		data.SomeData.Add("Key 1", new SomeStructure("Name 1", 1));
		data.SomeData.Add("Key 2", new SomeStructure("Name 2", 2));
		data.SomeData.Add("Key 3", new SomeStructure("Name 3", 3));
		
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
			Console.WriteLine("Friends: {0}", string.Join(", ", data.Friends));
		}	
	}
}