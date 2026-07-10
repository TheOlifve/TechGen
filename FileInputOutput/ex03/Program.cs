namespace ex03;

class Program
{
    static void Main(string[] args)
    {
        byte[] blockA = new byte[]
        {
            34, 35, 36, 35, 38,
            43, 42, 41, 40, 39
        };

        byte[] blockB = new byte[]
        {
            101, 102, 103, 104, 105,
            106, 107, 108, 109, 110
        };
        
        var fileName = "test.txt";
        
        PartialBinaryDownloader.DownloadChunk(fileName, blockA, 0);
        PartialBinaryDownloader.DownloadChunk(fileName, blockB, 1024);
        
        Console.WriteLine(PartialBinaryDownloader.Verification(fileName, blockA, blockB,1024));
    }
}