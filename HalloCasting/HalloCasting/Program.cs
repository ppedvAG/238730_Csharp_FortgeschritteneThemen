Console.WriteLine("Hello, World!");

var fred = new Person() { Id = 1, Name = "Fred" };

ZeigeDings(fred);
ZeigeDings(new Tier() { Name = "Bello" });


void ZeigeDings(object dings)
{
    //1. Casting + type check (aaaalt) / .net Framework 1.1 style
    if (dings is Person)
    {
        Person dingsAlsPerson = (Person)dings; //casting
        Console.WriteLine(dingsAlsPerson.Name);
    }

    //2. Boxing (alt) / .net Framework 2.0
    Person dingAlsPersonBoxing = dings as Person;
    if (dingAlsPersonBoxing != null)
    {
        Console.WriteLine(dingAlsPersonBoxing.Name);
    }

    //3. Pattern machting (cool und neu) ab .net framework 4.5
    if (dings is Person person)
    {
        Console.WriteLine(person.Name);
    }
}

class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
}

class Tier
{
    public string Name { get; set; }

}