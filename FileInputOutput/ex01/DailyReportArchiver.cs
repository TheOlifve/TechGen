using System.Text;

namespace ex01;

public static class DailyReportArchiver
{
    public static void CreateDailyReport(string report, string path)
    {
        string fileName = $"Report_{DateTime.Today.Date.Day}{DateTime.Today.Month}{DateTime.Today.Year}_{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}.txt";
        
        Console.WriteLine($"Archiving {fileName} in {path}");
        path += "/" + fileName;
        File.WriteAllText(path  +  fileName, report);
        
        if (ValidateReport(report, File.ReadAllText(path + fileName)))
            Console.WriteLine($"Saved OK");
        else
            Console.WriteLine("Failed");
    }

    private static bool ValidateReport(string currentReport, string reportFromFile)
    {
        if (currentReport != reportFromFile)
            return false;
        return true;
    }
}