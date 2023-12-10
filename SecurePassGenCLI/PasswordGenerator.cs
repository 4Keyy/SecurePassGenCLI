using SecurePassGenCLI.Enums;
using SecurePassGenCLI.Options;
using System.Security.Cryptography;

namespace SecurePassGenCLI
{
    public class PasswordGenerator
    {
        public string GeneratePassword(PasswordOptions options)
        {
            ValidateOptions(options);

            List<char> allChars = GetCharacters(options);

            if (allChars.Count == 0)
            {
                throw new ArgumentException("At least one character set must be selected for the password.");
            }

            Span<char> passwordChars = stackalloc char[options.Length];
            using var rng = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[4];

            for (int i = 0; i < options.Length; ++i)
            {
                rng.GetBytes(randomBytes);
                uint randomIndex = (uint)(BitConverter.ToUInt32(randomBytes, 0) % allChars.Count);
                passwordChars[i] = allChars[(int)randomIndex];
            }

            char[] pass = new char[options.Length];

            for (int i = 0; i < pass.Length; i++)
            {
                pass[i] = i < passwordChars.Length ? passwordChars[i] : '\0';

                if ((i + 1) % 5 == 0 && i != 0)
                {
                    pass[i] = '-';
                }
            }

            return new string(pass);
        }

        private static void ValidateOptions(PasswordOptions options)
        {
            if (options.Length <= 0)
            {
                throw new ArgumentException("Password length must be greater than 0.");
            }
        }

        private static List<char> GetCharacters(PasswordOptions options)
        {
            List<char> allChars = new();

            if (options.UseCustomCharacterSet)
            {
                allChars.AddRange(options.CustomCharacterSet);
            }
            else
            {
                int halfLength = options.Length / 2;
                int quarterLength = options.Length / 4;

                switch (options.Strength)
                {
                    case PasswordStrength.Low:
                        AddCharacters(allChars, options.Length, PasswordOptions.Digits);
                        break;
                    case PasswordStrength.Medium:
                        AddCharacters(allChars, halfLength, PasswordOptions.UppercaseLetters);
                        AddCharacters(allChars, halfLength, PasswordOptions.LowercaseLetters);
                        AddCharacters(allChars, quarterLength, PasswordOptions.Digits);
                        AddCharacters(allChars, quarterLength, PasswordOptions.SpecialCharacters);
                        break;
                    case PasswordStrength.High:
                        AddCharacters(allChars, halfLength, PasswordOptions.UppercaseLetters);
                        AddCharacters(allChars, halfLength, PasswordOptions.LowercaseLetters);
                        AddCharacters(allChars, quarterLength, PasswordOptions.Digits);
                        AddCharacters(allChars, quarterLength, PasswordOptions.SpecialCharacters);
                        AddRandomCharacters(allChars, quarterLength);
                        break;
                }
            }

            if (!options.AllowAmbiguousCharacters)
            {
                RemoveAmbiguousCharacters(allChars);
            }

            return allChars;
        }

        public static bool IsPasswordSecure(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            int minLength = 8;
            int minCharacterTypes = 3;

            if (password.Length < minLength)
            {
                return false;
            }

            int characterTypesCount = 0;
            if (ContainsUppercaseLetter(password)) characterTypesCount++;
            if (ContainsLowercaseLetter(password)) characterTypesCount++;
            if (ContainsDigit(password)) characterTypesCount++;
            if (ContainsSpecialCharacter(password)) characterTypesCount++;

            if (characterTypesCount < minCharacterTypes)
            {
                return false;
            }

            return true;
        }

        private static bool ContainsUppercaseLetter(string password)
        {
            return password.Any(char.IsUpper);
        }

        private static bool ContainsLowercaseLetter(string password)
        {
            return password.Any(char.IsLower);
        }

        private static bool ContainsDigit(string password)
        {
            return password.Any(char.IsDigit);
        }

        private static bool ContainsSpecialCharacter(string password)
        {
            const string specialCharacters = "!@#$%&*()=+[]?";
            return password.Intersect(specialCharacters).Any();
        }

        private static void AddCharacters(List<char> charList, int count, string characters)
        {
            for (int i = 0; i < count; i++)
            {
                int index = SecureRandom.GetRandomNumber(characters.Length);
                charList.Add(characters[index]);
            }
        }

        private static void AddRandomCharacters(List<char> charList, int count)
        {
            const string allCharacters = PasswordOptions.UppercaseLetters + PasswordOptions.LowercaseLetters +
                                         PasswordOptions.Digits + PasswordOptions.SpecialCharacters;

            AddCharacters(charList, count, allCharacters);
        }

        private static void RemoveAmbiguousCharacters(List<char> charList)
        {
            const string ambiguousCharacters = "0O1lI";
            charList.RemoveAll(c => ambiguousCharacters.Contains(c));
        }

        private class SecureRandom
        {
            private static readonly RandomNumberGenerator random = RandomNumberGenerator.Create();

            public static int GetRandomNumber(int maxValue)
            {
                byte[] randomNumber = new byte[4];
                random.GetBytes(randomNumber);
                int value = BitConverter.ToInt32(randomNumber, 0) & 0x7FFFFFFF;
                return value % maxValue;
            }
        }
    }
}
