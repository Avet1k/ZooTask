namespace ZooTask;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Another test SSH push");
    }
}

abstract class Animal
{
    public Animal(string name, string sex, string sound)
    {
        Name = name;
        Sex = sex;
        Sound = sound;
    }
    
    public string Name { get; }
    public string Sex { get; }
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
    public Wolf(string sex) : base("Волк", sex, "Ауф") { }
}

class Capybara : Animal
{
    public Capybara(string sex) : base("Капибара", sex, string.Empty) { }

    public override void MakeSound()
    {
        base.MakeSound();
        Console.WriteLine($"{Name} лишь тихо лежит и смотрит на вас.");
    }
}

class Frog : Animal
{
    public Frog(string sex) : base("Лягушка", sex, "Ква-ква") { }
}