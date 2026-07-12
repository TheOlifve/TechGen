namespace FileCopy;

class BuildConfig:IDisposable, IAsyncDisposable
{
    public FileStream SourceStream { get; set; }
    public FileStream DestinationStream { get; set; }

    public long BufferSize { get; set; } = 4;

    public void Determinate(string type, string value)
    {
        switch (type)
        {
            case "--source":
                SourceStream = new FileStream(value, FileMode.Open, FileAccess.Read, FileShare.None);
                break;
            case "--destination":
                DestinationStream = new FileStream(value, FileMode.Create, FileAccess.Write, FileShare.None);
                break;
            case "--buff":
                BufferSize = int.Parse(value);
                break;
            default:
                throw new ArgumentException("Invalid parameter type");
        }
    }

    public void Dispose()
    {
        SourceStream.Dispose();
        DestinationStream.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await SourceStream.DisposeAsync();
        await DestinationStream.DisposeAsync();
    }
}