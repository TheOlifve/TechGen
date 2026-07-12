namespace FileCopy;

static class CopyProcessor
{
    public static void Copy(FileStream sourceStream, FileStream destinationStream, long bufferSize)
    {
        CopyProgressMonitor monitor = new CopyProgressMonitor(sourceStream, destinationStream,  bufferSize);
        byte[] buffer = new byte[bufferSize];
            
        monitor.StartCopy();
        while (true)
        {
            monitor.ChunkStart();
         
            int bytesRead = sourceStream.Read(buffer, 0, buffer.Length);
            
            if (bytesRead <= 0)
                break;
            
            destinationStream.Write(buffer, 0, bytesRead);
            destinationStream.Flush();
            
            monitor.ChunkEnd();
        }
        monitor.StopCopy();
    }
}