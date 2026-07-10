using System.Reflection.Emit;

namespace ex03;

public static class PartialBinaryDownloader
{
    static string _absolutePath = Path.GetFullPath(System.Environment.CurrentDirectory);
    
    public static void DownloadChunk(string filename, byte[] chunk, int chunkOffset)
    {
        string filePath = _absolutePath + "/" + filename;
        FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        
        fs.Seek(chunkOffset, SeekOrigin.Begin);
        fs.Write(chunk, 0, chunk.Length);
        
        fs.Close();
    }
}