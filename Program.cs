namespace ZooTask;
class Program
{
    static void Main(string[] args)
    {
        Zoo zoo = new Zoo();
        
        zoo.LookAnimals();
    }
}

abstract class Animal
{
    private string _sound;
    
    protected Animal(string name, char sex, string sound = "")
    {
        Name = name;
        Sex = sex;
        _sound = sound;
    }
    
    public string Name { get; }
    public char Sex { get; }

    public virtual void MakeSound()
    {
        if (_sound != string.Empty)
            Console.WriteLine($"{Name} говорит {_sound}!");
        else
            Console.WriteLine($"{Name} ничего не говорит.");
    }

    public abstract Animal CreateNew(char sex);
}

class Wolf : Animal
{
    public Wolf(char sex = 'M') : base("Волк", sex, "Ауф") { }
    
    public override Animal CreateNew(char sex)
    {
        return new Wolf(sex: sex);
    }
}

class Capybara : Animal
{
    public Capybara(char sex = 'M') : base("Капибара", sex) { }

    public override Animal CreateNew(char sex)
    {
        return new Capybara(sex);
    }
}

class Frog : Animal
{
    public Frog(char sex = 'M') : base("Лягушка", sex, "Ква-ква") { }
    
    public override Animal CreateNew(char sex)
    {
        return new Frog(sex);
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

    public void ShowDescription()
    {
        if (_animals.Count > 0)
        {
            var animal = _animals[0];
            
            Console.WriteLine($"Вальер №{_id}\n" +
                              $"Название животного: {animal.Name}\n" +
                              $"Всего животных: {_animals.Count}\n" +
                              $"Женских особей: {_femalesCount}\n" +
                              $"Мужских особей: {_malesCount}");
            
            animal.MakeSound();
        }
        else
        {
            Console.WriteLine("Вальер пуст.");
        }
    }

    public void AddAnimal(Animal animal)
    {
        char maleSymbol = 'M';
        char femaleSymbol = 'F';
        
        _animals.Add(animal);

        if (animal.Sex == maleSymbol)
            _malesCount++;
        else if (animal.Sex == femaleSymbol)
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
        
        FillEnclosure(0, new Wolf());
        FillEnclosure(1, new Capybara());
        FillEnclosure(2, new Frog());
    }

    public void LookAnimals()
    {
        const int FirstEnclosureMenu = 1;
        const int SecondEnclosureMenu = 2;
        const int ThrirdEnclosureMenu = 3;
        const int FourthEnclosureMenu = 4;
        const int ExitMenu = 0;

        bool isWork = true;

        while (isWork)
        {
            char userInput;
            
            Console.Clear();
            Console.WriteLine("Добро пожаловать в зоопарк!\n\n" +
                              "Выберите вальер, чтобы к нему подойти:\n" +
                              $"{FirstEnclosureMenu} - первый вольер\n" +
                              $"{SecondEnclosureMenu} - второй вольер\n" +
                              $"{ThrirdEnclosureMenu} - третий вольер\n" +
                              $"{FourthEnclosureMenu} - четвёртый вольер\n" +
                              $"{ExitMenu} - выйти из зоопарка\n");

            userInput = Console.ReadKey(true).KeyChar;

            if (int.TryParse(userInput.ToString(), out int inputNumber))
                if (inputNumber >= FirstEnclosureMenu && inputNumber <= FourthEnclosureMenu)
                    _enclosures[inputNumber - 1].ShowDescription();
                else if (inputNumber == ExitMenu)
                    isWork = false;

            if (!isWork)
                continue;
            
            Console.WriteLine("\nДля продолжения нажмите любую кнопку...");
            Console.ReadKey();
        }
    }
    
    private void FillEnclosure(int index, Animal animal)
    {
        Random random = new Random();
        char[] sexes = new[] { 'M', 'F' };

        Enclosure enclosure = _enclosures[index];

        for (int i = 0; i < enclosure.Capacity; i++)
        {
            char sex = sexes[random.Next(sexes.Length)];
            
            enclosure.AddAnimal(animal.CreateNew(sex));
        }
    }
}