namespace FileCopy;

static class Parser
{
    public static void Parse(string[] args, BuildConfig p)
    {
        switch (args.Length)
        {
            case 4:
                p.Determinate(args[0], args[1]);
                p.Determinate(args[2], args[3]);
                break;
            case 6:
                p.Determinate(args[0], args[1]);
                p.Determinate(args[2], args[3]);
                p.Determinate(args[4], args[5]);
                break;
            default:
                throw new ArgumentException("Invalid parameter count");
        }
    }
}