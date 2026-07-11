namespace TeamSpliter;

class Team
{
    public Student A { get; set; }
    public Student B { get; set; }
    public Student? C { get; set; } = null;
    
    public Team(Student a, Student b, Student? c = null)
    {
        A = a;
        B = b;
        C = c;
    }
}