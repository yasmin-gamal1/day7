using System;
using System.IO;
using System.Linq;

class FileOperations
{
    private static string filePath = "data.txt";

    public static void WriteToFile()
    {
        try
        {
            Console.WriteLine("Enter text to write to the file:");
            string content = Console.ReadLine();

            
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(content);
            }
            Console.WriteLine("Content written to file successfully.\n");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Error: You don't have permission to write to the file.");
        }
        catch (IOException ioEx)
        {
            Console.WriteLine($"IO Error: {ioEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected Error: {ex.Message}");
        }
    }

    
    public static void ReadFromFile()
    {
        try
        {
            if (File.Exists(filePath))
            {
                Console.WriteLine("\nFile Contents:");
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
            }
            else
            {
                throw new FileNotFoundException("File not found.");
            }
        }
        catch (FileNotFoundException fnfEx)
        {
            Console.WriteLine($"Error: {fnfEx.Message}");
        }
        catch (IOException ioEx)
        {
            Console.WriteLine($"IO Error: {ioEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected Error: {ex.Message}");
        }
    }

    
    public static void SearchInFile()
    {
        try
        {
            if (File.Exists(filePath))
            {
                Console.WriteLine("\nEnter the text to search:");
                string searchText = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(searchText))
                {
                    throw new ArgumentException("Search text cannot be empty.");
                }

                string[] lines = File.ReadAllLines(filePath);
                var searchResults = lines.Where(line => line.Contains(searchText)).ToList();

                if (searchResults.Count > 0)
                {
                    Console.WriteLine("Found the following matching lines:");
                    foreach (var line in searchResults)
                    {
                        Console.WriteLine(line);
                    }
                }
                else
                {
                    Console.WriteLine("No matching text found.");
                }
            }
            else
            {
                throw new FileNotFoundException("File not found.");
            }
        }
        catch (ArgumentException argEx)
        {
            Console.WriteLine($"Input Error: {argEx.Message}");
        }
        catch (FileNotFoundException fnfEx)
        {
            Console.WriteLine($"Error: {fnfEx.Message}");
        }
        catch (IOException ioEx)
        {
            Console.WriteLine($"IO Error: {ioEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected Error: {ex.Message}");
        }
    }

    
    public static void UpdateInFile()
    {
        try
        {
            if (File.Exists(filePath))
            {
                Console.WriteLine("\nEnter the text to update:");
                string oldText = Console.ReadLine();

                Console.WriteLine("Enter the new text:");
                string newText = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(oldText) || string.IsNullOrWhiteSpace(newText))
                {
                    throw new ArgumentException("Text for updating cannot be empty.");
                }

                string[] lines = File.ReadAllLines(filePath);
                bool isUpdated = false;

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var line in lines)
                    {
                        if (line.Contains(oldText))
                        {
                            writer.WriteLine(line.Replace(oldText, newText));
                            isUpdated = true;
                        }
                        else
                        {
                            writer.WriteLine(line);
                        }
                    }
                }

                if (isUpdated)
                {
                    Console.WriteLine("Content updated successfully.\n");
                }
                else
                {
                    Console.WriteLine("Text not found to update.\n");
                }
            }
            else
            {
                throw new FileNotFoundException("File not found.");
            }
        }
        catch (ArgumentException argEx)
        {
            Console.WriteLine($"Input Error: {argEx.Message}");
        }
        catch (FileNotFoundException fnfEx)
        {
            Console.WriteLine($"Error: {fnfEx.Message}");
        }
        catch (IOException ioEx)
        {
            Console.WriteLine($"IO Error: {ioEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected Error: {ex.Message}");
        }
    }

    
    public static void DeleteFile()
    {
        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine("File deleted successfully.\n");
            }
            else
            {
                throw new FileNotFoundException("File not found.");
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Error: You don't have permission to delete the file.");
        }
        catch (FileNotFoundException fnfEx)
        {
            Console.WriteLine($"Error: {fnfEx.Message}");
        }
        catch (IOException ioEx)
        {
            Console.WriteLine($"IO Error: {ioEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected Error: {ex.Message}");
        }
    }

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Choose an operation:");
            Console.WriteLine("1. Write to File");
            Console.WriteLine("2. Read from File");
            Console.WriteLine("3. Search in File");
            Console.WriteLine("4. Update in File");
            Console.WriteLine("5. Delete File");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice (1-6): ");

            int choice;
            try
            {
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    throw new FormatException("Invalid input! Please enter a valid number.");
                }

                switch (choice)
                {
                    case 1:
                        WriteToFile();
                        break;
                    case 2:
                        ReadFromFile();
                        break;
                    case 3:
                        SearchInFile();
                        break;
                    case 4:
                        UpdateInFile();
                        break;
                    case 5:
                        DeleteFile();
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Invalid choice! Please choose a number between 1 and 6.");
                        break;
                }
            }
            catch (FormatException formatEx)
            {
                Console.WriteLine($"Input Error: {formatEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }
        }
    }
}
