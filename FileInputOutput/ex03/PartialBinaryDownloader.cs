using System.Reflection.Emit;

namespace ex03;

public static class PartialBinaryDownloader
{
    static string _absolutePath = Path.GetFullPath(System.Environment.CurrentDirectory);

    // public static bool Verification(string filename, byte[] chunkA, byte[] chunkB, int chunkOffset)
    // {
    //     string filePath = _absolutePath + "/" + filename;
    //     FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
    //     byte[] bufferA = new byte[1024];
    //     byte[] bufferB = new byte[1024];
    //     
    //     fs.Seek(0, SeekOrigin.Begin);
    //     fs.Read(bufferA, 0, 1024);
    //     fs.Seek(1024, SeekOrigin.Begin);
    //     fs.Read(bufferB, 0, chunkB.Length);
    //
    //     if (bufferA.SequenceEqual(chunkA) && bufferB.SequenceEqual(chunkB))
    //         return true;
    //     return false;
    // }
    
    public static void DownloadChunk(string filename, byte[] chunk, int chunkOffset)
    {
        string filePath = _absolutePath + "/" + filename;
        FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
        
        fs.Seek(chunkOffset, SeekOrigin.Begin);
        fs.Write(chunk, 0, chunk.Length);
        
        fs.Dispose();
    }
}