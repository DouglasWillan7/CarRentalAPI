namespace DW.Rental.Bucket.Configs;

public class BucketSettings
{
    public string Endpoint { get; init; } = string.Empty;
    public int Port { get; init; }
    public string AccessKey { get; init; } = string.Empty;
    public string SecretKey { get; init; } = string.Empty;
    public string BucketName { get; init; } = string.Empty;
}
