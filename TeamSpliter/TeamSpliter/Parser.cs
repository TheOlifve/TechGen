using System.Security.Cryptography;

namespace TeamSpliter;
static class Parser
{
    public static void Parse(string path, out List<Student> studentsList)
    {
        string? line = null;
        studentsList = new List<Student>();
        StreamReader stream = new StreamReader(path);

        while (true)
        {
            try
            {
                line = stream.ReadLine();
            }
            catch (Exception ex)
            {
                stream.Dispose();
                throw;
            }

            if (line == null)
                break;

            string[] split = line.Split(' ');
            switch (split.Length)
            {
                case 2:
                    studentsList.Add(new Student(split[0], split[1]));
                    break;
                case 3:
                    studentsList.Add(new Student(split[0], split[1], int.Parse(split[2])));
                    break;
                default:
                    Console.WriteLine($"Invalid format for line - [{line}]. Usage 'Name Surname Score(optional)'.");
                    break;
            }
        }
        stream.Dispose();
    }
}
