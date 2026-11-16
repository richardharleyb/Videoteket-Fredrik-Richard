namespace Videoteket.src.UI;

internal static class ConsoleInputReader
{
  private static string ReadLine(string prompt)
  {
    Console.Write(prompt);
    string? text = Console.ReadLine();

    if (text == null)
    {
      return "";
    }

    return text.Trim();
  }

  public static int ReadIntInRange(string prompt, int min, int max)
  {
    while (true)
    {
      string text = ReadLine(prompt);

      if (int.TryParse(text, out int number) && number >= min && number <= max)
      {
        return number;
      }

      Console.WriteLine($"Please enter a number between {min} and {max}.");
    }
  }

  public static string ReadNonEmptyText(string prompt)
  {
    while (true)
    {
      string text = ReadLine(prompt);

      if (text != "")
      {
        return text;
      }

      Console.WriteLine("This field cannot be empty.");
    }
  }

  public static bool ReadYesOrNo(string prompt)
  {
    while (true)
    {
      string text = ReadLine(prompt + " (y/n): ");

      if (text == "y" || text == "Y")
      {
        return true;
      }

      if (text == "n" || text == "N")
      {
        return false;
      }

      Console.WriteLine("Please answer y or n.");
    }
  }

  public static void WaitForAnyKey()
  {
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
  }
}
