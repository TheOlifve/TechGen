namespace ex02;

public static class InboxScanner
{
    public static void Scan(string directoryPath)
    {
        try
        {
            Directory.CreateDirectory(directoryPath);
            int total = 0;
            
            foreach (var file in Directory.EnumerateFiles(directoryPath, "*", SearchOption.TopDirectoryOnly))
            {
                Console.WriteLine($"{Path.GetFileName(file)} - {new FileInfo(file).Length} bytes");
                total++;
            }
            
            Console.WriteLine($"Total files: {total}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}