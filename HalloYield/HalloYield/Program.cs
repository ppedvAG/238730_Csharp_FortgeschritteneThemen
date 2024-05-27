// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

foreach (var z in GetZahlen())
{
    Console.WriteLine($"{z}");
}

await foreach (var z in GetZahlenAsync())
{
    Console.WriteLine($"{z}");
}

async IAsyncEnumerable<int> GetZahlenAsync()
{
    await Task.Delay(2000);
    yield return 12;
    await Task.Delay(2000);
    yield return -6;
    await Task.Delay(2000);
    yield return 900;
}

IEnumerable<int> GetZahlen()
{
    yield return 12;
    yield return -6;
    yield return 900;

    //var liste = new List<int>();
    //liste.Add(12);
    //liste.Add(-6);
    //liste.Add(900);
    //return liste;   
}