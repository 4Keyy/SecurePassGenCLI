## 🛡 SecurePassGenCLI Documentation

### Introduction

SecurePassGenCLI is a comprehensive command-line tool crafted to provide users with the ability to generate secure and tailored passwords. This project embodies best practices in security, code structure, and adaptability. Whether you require a simple password or a highly complex one, SecurePassGenCLI is equipped to meet your needs.

### 🚀 Installation

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/4Keyy/SecurePassGenCLI.git
   ```

2. **Open in IDE:**
   Open the project in a C# IDE such as Visual Studio or Visual Studio Code.

3. **Build and Run:**
   Build the project to generate the executable, then run it from the command line.
   ```bash
   dotnet build
   dotnet run
   ```

Ensure you have the [.NET SDK](https://dotnet.microsoft.com/download) installed.

### Usage

Upon running SecurePassGenCLI, the user is prompted with an interactive menu to customize the password generation process. Follow the on-screen instructions to specify password strength, length, and other options.

```bash
dotnet SecurePassGenCLI.dll
```

### Project Structure

The project follows a modular structure:

- **Managers:**
  - **PasswordManager:** Orchestrates the password generation process. The `Run` method guides the user through the generation steps.

- **Options:**
  - **PasswordOptions:** Defines configurable options for password generation, including length, strength, and custom character sets.

- **PasswordGenerator:**
  - **PasswordGenerator:** Generates passwords based on specified options. It utilizes a cryptographically secure random number generator.

- **Program:**
  - **Program:** Serves as the entry point for the application. The `Main` method creates an instance of `PasswordManager` and invokes its `Run` method.

### Documentation

#### PasswordManager Class

The `PasswordManager` class guides users through the password generation process.

```csharp
public void Run()
```

The `Run` method prompts users to choose password strength and length. It handles user input, generates passwords, and provides a user-friendly experience.

#### PasswordOptions Class

The `PasswordOptions` class defines options for password generation.

Key properties:

- `Length`: Specifies the length of the generated password.
- `Strength`: Represents the password strength.
- `AllowAmbiguousCharacters`: Indicates if ambiguous characters are allowed.
- `UseCustomCharacterSet`: Indicates if a custom character set is used.
- `CustomCharacterSet`: Stores the custom character set if used.

#### PasswordGenerator Class

The `PasswordGenerator` class generates passwords based on specified options.

Key methods:

- `GeneratePassword(PasswordOptions options)`: Generates a password using a cryptographically secure random number generator.
- `IsPasswordSecure(string password)`: Checks if a password meets security criteria.

**Algorithm for Password Generation:**

- A random byte array is generated using `RNGCryptoServiceProvider`.
- The bytes are converted into an integer and used to index characters from the selected character set.
- The process is repeated for each character in the password length.

**Security Criteria Checking:**

- Passwords must meet a minimum length requirement.
- At least three of the following criteria must be met: uppercase letters, lowercase letters, digits, special characters.

#### Program Class

The `Program` class is the entry point for the application.

```csharp
public static void Main()
```

The `Main` method creates an instance of `PasswordManager` and invokes its `Run` method.

### Technologies Used

- **C#:** The primary programming language.
- **.NET Core:** The cross-platform framework for building applications.

### References

- [.NET Documentation](https://docs.microsoft.com/en-us/dotnet/)

Feel free to explore the codebase, customize settings, and contribute to the project! If you encounter issues or have suggestions, open an [issue](#repository-issues-link) on the repository.

Happy password generating with SecurePassGenCLI! 🎉