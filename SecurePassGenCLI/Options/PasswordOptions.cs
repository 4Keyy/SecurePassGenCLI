using SecurePassGenCLI.Enums;

namespace SecurePassGenCLI.Options
{
    public class PasswordOptions
    {
        public int Length { get; set; } = 12;
        public PasswordStrength Strength { get; set; } = PasswordStrength.Medium;
        public bool AllowAmbiguousCharacters { get; set; } = false;
        public bool UseCustomCharacterSet { get; set; } = false;
        public string CustomCharacterSet { get; set; } = "";

        public const string UppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string LowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
        public const string Digits = "0123456789";
        public const string SpecialCharacters = "!@#$%&*()=+[]?";
    }
}
