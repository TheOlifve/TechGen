using System.Text;

namespace ex04;

public class LogProcessor
{
    public void Create()
    {
        StreamWriter writer = new StreamWriter("log.txt", false, Encoding.UTF8);
        
        writer.WriteLine("Log 1");
        writer.WriteLine("Log 2");
        writer.WriteLine("Ошибка Лог 3");
        writer.WriteLine("Log 4");
        writer.WriteLine("Error Log 5");
        writer.Flush();

        StreamReader reader = new StreamReader("log.txt", Encoding.UTF8);
        
        int errorCount = 0;
        
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            if (line.Contains("Error") || line.Contains("Ошибка"))
                errorCount++;
        }
        
        Console.WriteLine($"{errorCount} errors detected");
        
        reader.Dispose();
        writer.Dispose();
    }
}