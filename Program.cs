using System;

Console.WriteLine("Enter the first number:");
int input1 = int.Parse(Console.ReadLine());
Console.WriteLine("Enter the second number:");
int input2 = int.Parse(Console.ReadLine());
Console.WriteLine("Enter an operation (+, -, *, /):");
string operation = Console.ReadLine();

 /*switch(operation)
{
    case "+":
        Console.WriteLine($"Result: {input1 + input2}");
        break;
    case "-":
        Console.WriteLine($"Result: {input1 - input2}");
        break;
    case "*":
        Console.WriteLine($"Result: {input1 * input2}");
        break;
    case "/":
        if (input2 != 0)
        {
            Console.WriteLine($"Result: {input1 / input2}");
        }
        else
        {
            Console.WriteLine("Error: Division by zero is not allowed.");
        }
        break;
    default:
        Console.WriteLine("Error: Invalid operation.");
        break;
} */

if(operation == "+")
{
    Console.WriteLine($"Result: {input1 + input2}");
}
else if(operation == "-")
{
    Console.WriteLine($"Result: {input1 - input2}");
}
else if(operation == "*")
{
    Console.WriteLine($"Result: {input1 * input2}");
}
else if(operation == "/")
{
   
        Console.WriteLine($"Result: {input1 / input2}");
   
}
else
{
    Console.WriteLine("Error: Invalid operation.");
}
