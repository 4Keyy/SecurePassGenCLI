using SecurePassGenCLI.Enums;
using SecurePassGenCLI.Options;

namespace SecurePassGenCLI.Managers
{
    public class PasswordManager
    {
        private readonly PasswordGenerator passwordGenerator = new();

        public void Run()
        {
            Console.Clear();

            try
            {
                while (true)
                {
                    Console.WriteLine("PASSWORD GENERATOR\n");

                    Console.WriteLine("Choose password strength:");
                    foreach (PasswordStrength strength in Enum.GetValues(typeof(PasswordStrength)))
                    {
                        Console.WriteLine($"{(int)strength}. {strength}");
                    }

                    Console.Write("Your choice: ");

                    if (Enum.TryParse(Console.ReadLine(), out PasswordStrength selectedStrength) && Enum.IsDefined(typeof(PasswordStrength), selectedStrength))
                    {
                        int length = GetUserInput("Choose password length (8 min, 19 optimal): ", 8, 100);

                        var options = new PasswordOptions
                        {
                            Length = length,
                            Strength = selectedStrength
                        };

                        var generatedPassword = passwordGenerator.GeneratePassword(options);

                        Console.WriteLine($"Your generated password: {generatedPassword}");

                        Console.Write("\nGenerate another password (y/n)?: ");
                        var key = Console.ReadKey();
                        if (key.Key != ConsoleKey.Y)
                        {
                            Console.Clear();
                            break;
                        }

                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number for password strength.");
                        Console.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred. Please contact support. Error details: {ex.Message}");
            }
        }

        private static int GetUserInput(string prompt, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()!;
                if (int.TryParse(input, out int result) && result >= minValue && result <= maxValue)
                {
                    return result;
                }

                Console.WriteLine($"Invalid input. Please enter a value between {minValue} and {maxValue}.");
            }
        }
    }
}
