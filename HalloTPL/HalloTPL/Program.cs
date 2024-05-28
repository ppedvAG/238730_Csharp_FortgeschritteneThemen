Console.WriteLine("Hello, World!");

var t1 = new Task(() =>
{
    Console.WriteLine("T1 gestartet");
    throw new OutOfMemoryException();
    Thread.Sleep(3000);
    Console.WriteLine("T1 fertig");
});
t1.ContinueWith(t => Console.WriteLine("T1 Continue")); // immer
t1.ContinueWith(t => Console.WriteLine("T1 OK"), TaskContinuationOptions.OnlyOnRanToCompletion); // ok
t1.ContinueWith(t => Console.WriteLine($"T1 ERROR {t.Exception.InnerException.Message}"), TaskContinuationOptions.OnlyOnFaulted); // ERROR


var t2 = new Task<long>(() =>
{
    Console.WriteLine("T2 gestartet");
    Thread.Sleep(2000);
    Console.WriteLine("T2 fertig");
    return 93276234432;
});

t1.Start();
t2.Start();
//t2.Result -> t2.Wait();
Console.WriteLine($"T2 Result: {t2.Result}");
//t1.Wait();
//Task.WaitAll(t1,t2);

//Parallel.Invoke(Zähle, Zähle, Zähle,()=> Zähle());
//Parallel.For(0,
//             100_000,
//             //new ParallelOptions() { MaxDegreeOfParallelism = 4 },
//             i => Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i:000}"));

//var texte = new List<string>();
//texte.Where(x => x.StartsWith("b")).AsParallel();

Console.WriteLine("Ende");
Console.ReadLine();


void Zähle()
{
    for (int i = 0; i < 20; i++)
    {
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i:000}");
    }
}