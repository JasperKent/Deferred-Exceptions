using ForcingExceptions;

LibraryAccessor.Prepopulate();

try
{
    //await TestAsync();
    TestYield();
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

async Task TestAsync()
{
    using var accessor = new LibraryAccessor();

    Console.WriteLine("First, we form the query.\n");

    var books = await accessor.ByDate(1950, 1940);

    Console.WriteLine("We formed the query. Now let's execute it.\n");

    foreach (var b in books)
        Console.WriteLine(b);
}

void TestYield()
{
    Console.WriteLine("First, we request the series.\n");

    var series = Fibonacci.Sequence(1);

    Console.WriteLine("Now we've got the series, we iterate through it.\n");

    foreach (var item in series)
        Console.WriteLine(item);
}