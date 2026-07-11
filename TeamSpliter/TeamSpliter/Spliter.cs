namespace TeamSpliter;

static class Spliter
{
    private static void SplitSortedStudents(List<Student> sortedStudents, List<Student> studentsA, List<Student> studentsB)
    {
        int i = 0;
        
        for (; i < studentsA.Capacity; i++)
            studentsA.Add(sortedStudents[i]);
        for (int j = 0; j < studentsB.Capacity; j++)
            studentsB.Add(sortedStudents[j + i]);
    }

    private static List<Team> ChooseTeams(List<Student> studentsA, List<Student> studentsB)
    {
        List<Team> ret = new List<Team>(studentsA.Count);
        Random rnd = new Random(DateTime.Now.Millisecond);
        
        List<Student> pickedA = studentsA.OrderBy(s => rnd.Next()).Take(studentsA.Count).ToList();
        List<Student> pickedB = studentsB.OrderBy(s => rnd.Next()).Take(studentsB.Count).ToList();

        for (int i = 0; i < pickedA.Count; i++)
            ret.Add(new Team(pickedA[i], pickedB[i]));

        if (studentsA.Count != studentsB.Count)
            ret.Last().C = studentsB.Last();
        
        return ret;
    }
    
    public static void SplitToTeams(List<Student> students, out List<Team> teams)
    {
        List<Student> sortedStudents = students.OrderBy(student => student.Score).ToList();
        int studentsCount = sortedStudents.Count;
        
        List<Student> studentsA = new List<Student>(studentsCount / 2);
        List<Student> studentsB = new List<Student>((studentsCount - studentsCount / 2));

        SplitSortedStudents(sortedStudents, studentsA, studentsB);
        
        if (studentsA.Count == 0 || studentsB.Count == 0)
            throw new Exception("Nothing to split.");
        
        teams = ChooseTeams(studentsA, studentsB);
    }
}
