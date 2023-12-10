using SecurePassGenCLI.Managers;

namespace SecurePassGenCLI
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                var passwordManager = new PasswordManager();
                passwordManager.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred. Please contact support.\n{ex}");
            }
        }
    }
}
