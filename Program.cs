namespace ZooTask;
class Program
{
    static void Main(string[] args)
    {
        Zoo zoo = new Zoo();
        
        zoo.LookAnimals();
    }
}

public class Genders
{
    public const char Male = 'M';
    public const char Female = 'F';
}

class Animal
{
    private string _sound;
    
    public Animal(string name, string sound = "", char sex = Genders.Female)
    {
        Name = name;
        Sex = sex;
        _sound = sound;
    }
    
    public string Name { get; }
    public char Sex { get; }

    public void MakeSound()
    {
        if (_sound != string.Empty)
            Console.WriteLine($"{Name} говорит {_sound}!");
        else
            Console.WriteLine($"{Name} ничего не говорит.");
    }

    public Animal CreateNew(char sex)
    {
        return new Animal(Name, _sound, sex);
    }
}

class Enclosure
{
    private static int _nextId = 1;
    private List<Animal> _animals;
    private int _id;
    private int _malesCount;
    private int _femalesCount;
    
    public Enclosure(int capacity)
    {
        _id = _nextId++;
        _animals = new List<Animal>(capacity);
    }

    public int Capacity => _animals.Capacity;
    public string AnimalsName => GetAnimalsName();

    public void ShowDescription()
    {
        if (_animals.Count > 0)
        {
            var animal = _animals[0];
            
            Console.WriteLine($"Вольер №{_id}\n" +
                              $"Название животного: {animal.Name}\n" +
                              $"Всего животных: {_animals.Count}\n" +
                              $"Женских особей: {_femalesCount}\n" +
                              $"Мужских особей: {_malesCount}");
            
            animal.MakeSound();
        }
        else
        {
            Console.WriteLine("Вольер пуст.");
        }
    }

    private string GetAnimalsName()
    {
        string empty = "Пустой вольер";
        
        if (_animals.Count > 0)
            return _animals[0].Name;
        
        return empty;
    }

    public void AddAnimal(Animal animal)
    {
        _animals.Add(animal);

        if (animal.Sex == Genders.Male)
            _malesCount++;
        else if (animal.Sex == Genders.Female)
            _femalesCount++;
    }
}

class Zoo
{
    private Enclosure[] _enclosures;

    public Zoo()
    {
        _enclosures = new[]
        {
            new Enclosure(6),
            new Enclosure(3),
            new Enclosure(10),
            new Enclosure(2)
        };
        
        FillEnclosure(0, new Animal("Волк", "Ауф"));
        FillEnclosure(1, new Animal("Капибара"));
        FillEnclosure(2, new Animal("Лягушка", "Ква"));
    }

    public void LookAnimals()
    {
        const int ExitMenu = 0;

        bool isWork = true;

        while (isWork)
        {
            string userInput;
            
            Console.Clear();
            Console.WriteLine("Добро пожаловать в зоопарк!\n\n");
            
            ShowInfo();
            
            Console.Write("\nВведите номер вольера, чтобы к нему подойти.\n" +
                          $"{ExitMenu} - выйти из зоопарка\n\n" +
                          "> ");

            userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int inputNumber))
            {
                if (inputNumber == ExitMenu)
                    isWork = false;
                else if (inputNumber > ExitMenu && inputNumber <= _enclosures.Length)
                    _enclosures[inputNumber - 1].ShowDescription();
            }

            if (!isWork)
                continue;
            
            Console.WriteLine("\nДля продолжения нажмите любую кнопку...");
            Console.ReadKey();
        }
    }

    private void ShowInfo()
    {
        for (int i = 0; i < _enclosures.Length; i++)
            Console.WriteLine($"Вольер {i + 1}: {_enclosures[i].AnimalsName}");
    }
    
    private void FillEnclosure(int index, Animal animal)
    {
        Random random = new Random();
        char[] sexes = { Genders.Male, Genders.Female };

        Enclosure enclosure = _enclosures[index];

        for (int i = 0; i < enclosure.Capacity; i++)
        {
            char sex = sexes[random.Next(sexes.Length)];
            
            enclosure.AddAnimal(animal.CreateNew(sex));
        }
    }
}