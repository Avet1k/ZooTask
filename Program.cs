namespace ZooTask;
class Program
{
    static void Main(string[] args)
    {
        
    }
}

abstract class Animal
{
    public Animal(string name, char sex, string sound)
    {
        Name = name;
        Sex = sex;
        Sound = sound;
    }
    
    public string Name { get; }
    public char Sex { get; }
    public string Sound { get; }

    public virtual void MakeSound()
    {
        if (Sound != string.Empty)
            Console.WriteLine($"{Name} говорит {Sound}!");
        else
            Console.WriteLine($"{Name} ничего не говорит.");
    }
}

class Wolf : Animal
{
    public Wolf(char sex) : base("Волк", sex, "Ауф") { }
}

class Capybara : Animal
{
    public Capybara(char sex) : base("Капибара", sex, string.Empty) { }

    public override void MakeSound()
    {
        base.MakeSound();
        Console.WriteLine($"{Name} лишь тихо лежит и смотрит.");
    }
}

class Frog : Animal
{
    public Frog(char sex) : base("Лягушка", sex, "Ква-ква") { }
}

class Enclosure
{
    private List<Animal> _animals;
    private string _description;
    private int _malesCount;
    private int _femalesCount;
    
    public Enclosure(int capacity, string description)
    {
        _animals = new List<Animal>(capacity);
        _description = description;
    }

    public void ShowDescription()
    {
        Console.WriteLine();  //TODO
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
        _enclosures = new Enclosure[4];
    }
}