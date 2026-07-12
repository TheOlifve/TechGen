namespace FileCopy;

class Program
{
    static void Main(string[] args)
    {
        BuildConfig p = new BuildConfig();

        try
        {
            Parser.Parse(args, p);
            CopyProcessor.Copy(p.SourceStream, p.DestinationStream, p.BufferSize);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            p.Dispose();
        }
    }
}
