namespace FileCopy;

class CopyProgressMonitor
{
    private DateTime _startTime;
    private DateTime _chunkStartTime;

    private FileStream _sourceStream;
    private FileStream _destinationStream;

    private long _bufferSize;
    private long _totalChunks;

    public CopyProgressMonitor(FileStream sourceStream, FileStream destinationStream, long bufferSize)
    {
        _sourceStream = sourceStream;
        _destinationStream = destinationStream;

        _startTime = DateTime.Now;
        _chunkStartTime = DateTime.Now;

        _bufferSize = bufferSize;
        _totalChunks = sourceStream.Length / bufferSize;
    }

    public void StartCopy()
    {
        Console.WriteLine($"Starting file copy: {_sourceStream.Name} -> {_destinationStream.Name}");
        _chunkStartTime = DateTime.Now;
        _startTime = DateTime.Now;
    }

    public void ChunkStart()
    {
        _chunkStartTime = DateTime.Now;
    }

    public void ChunkEnd()
    {
        TimeSpan oneChunkTime = DateTime.Now - _chunkStartTime;

        long remainingBytes = _sourceStream.Length - _destinationStream.Length;
        long remainingChunks = remainingBytes / _bufferSize;

        double progress = (double)_destinationStream.Length / _sourceStream.Length * 100;

        if ((int)progress % 5 == 0)
            Console.WriteLine(
                $"Copying {_sourceStream.Name} -> {_destinationStream.Name} | " +
                $"{progress:F0}% ({_destinationStream.Length}/{_sourceStream.Length} bytes) | " +
                $"ETA: {remainingChunks * oneChunkTime.TotalMilliseconds:F0} ms");
    }

    public void StopCopy()
    {
        TimeSpan totalTime = DateTime.Now - _startTime;

        Console.WriteLine(
            $"File copy completed successfully in {totalTime.TotalMilliseconds:F0} ms. " +
            $"Copied {_sourceStream.Name} -> {_destinationStream.Name}");
    }
}