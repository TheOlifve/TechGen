namespace TeamSpliter;

class Program
{
    private static void PrintTeams(List<Team> teams)
    {
        int cnt = 0;
        
        foreach (Team team in teams)
        {
            cnt++;
            Console.Write($"Team[{cnt}] - {team.A.Name} {team.A.Surname}, {team.B.Name} {team.B.Surname}");
            if (team.C != null)
                Console.WriteLine($", {team.C.Name} {team.C.Surname}");
            else
                Console.WriteLine();
        }
    }
    
    static void Main(string[] args)
    {
        List<Student> students;
        List<Team> teams;

        try
        {
            Parser.Parse("StudentsList.txt", out students);
            Spliter.SplitToTeams(students, out teams);
            
            PrintTeams(teams);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
    }
}