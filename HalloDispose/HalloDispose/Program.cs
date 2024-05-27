Console.WriteLine("Hello, World!");

//mit using == kein finally nötig (c# 8)
try
{
    using var sw = new StreamWriter("aaaa.txt");

    sw.WriteLine("AAAAA");
    sw.WriteLine("BBBBB");
    throw new OutOfMemoryException();
    sw.WriteLine("CCCCC");
    sw.WriteLine("DDDDD");
    //sw.Dispose(); //->sw.Flush();sw.Close();
}
catch (Exception ex)
{
    Console.WriteLine($"PANIK: {ex.Message}");
}

//mit using == kein finally nötig
try
{
    using (var sw = new StreamWriter("aaaa.txt"))
    {

        sw.WriteLine("AAAAA");
        sw.WriteLine("BBBBB");
        throw new OutOfMemoryException();
        sw.WriteLine("CCCCC");
        sw.WriteLine("DDDDD");

    } //sw.Dispose(); //->sw.Flush();sw.Close();
}
catch (Exception ex)
{
    Console.WriteLine($"PANIK: {ex.Message}");
}

//ohne using, wird finally benötigt zum aufräumen
StreamWriter sw2 = null;
try
{
    sw2 = new StreamWriter("aaaa.txt");
    sw2.WriteLine("AAAAA");
    sw2.WriteLine("BBBBB");
    throw new OutOfMemoryException();
    sw2.WriteLine("CCCCC");
    sw2.WriteLine("DDDDD");
}
catch (Exception ex)
{
    Console.WriteLine($"PANIK: {ex.Message}");
}
finally
{
    sw2.Dispose(); //->sw.Flush();sw.Close();
}

