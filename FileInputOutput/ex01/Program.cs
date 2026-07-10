using System.Text;

namespace ex01;

class Program
{
    static void Main(string[] args)
    {
        var absolutePath = Path.GetFullPath(System.Environment.CurrentDirectory);
        StringBuilder report = new StringBuilder();
        report.Append($"Daily Report - {DateTime.Now.ToShortDateString()}\n");
        report.Append("Team completed 8 tasks today, with 2 carried over to tomorrow.\n");
        report.Append("No blockers were reported. All systems running normally.\n");
        report.Append("Next check-in scheduled for 9:00 AM tomorrow.\n");
        
        DailyReportArchiver.CreateDailyReport(report.ToString(), absolutePath);
    }
}