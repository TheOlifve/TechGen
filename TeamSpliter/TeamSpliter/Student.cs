namespace TeamSpliter;

class Student
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Score { get; set; }
    
    public Student(string name, string surname, int score = 100)
    {
        Name = name;
        Surname = surname;
        Score = score;
    }
}