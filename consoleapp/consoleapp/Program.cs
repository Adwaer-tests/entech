

using consoleapp;

Console.WriteLine("Enter the matrix as a string (e.g., \"1,0,1;0,1,0\"): ");
string? input = Console.ReadLine();
do
{
    var validation = input.ValidateStringMatrix();
    if (validation != null)
    {
        Console.WriteLine($"Validation error: {validation}");
    }
    else
    {
        int result = input!.Calculate();
        Console.WriteLine($"Output: {result}");
    }

    input = Console.ReadLine();
} while (input?.ToUpperInvariant() != "EXIT");

Console.WriteLine("See you!");
