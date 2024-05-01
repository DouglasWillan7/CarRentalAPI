using DW.Rental.Bucket.Configs;
using DW.Rental.Domain.Repositories;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace DW.Rental.Bucket.Repositories;

public class PhotoRepository : IPhotoRepository
{
    private readonly IMinioClient _minioClient;
    private readonly string _bucketName;


    public PhotoRepository(IOptions<BucketSettings> bucketSettings)
    {
        _minioClient = new MinioClient()
                    .WithEndpoint(bucketSettings.Value.Endpoint, bucketSettings.Value.Port)
                    .WithCredentials(bucketSettings.Value.AccessKey, bucketSettings.Value.SecretKey)
                    .Build();

        _bucketName = bucketSettings.Value.BucketName;  
    }

    public async Task<string> UploadPhoto(Stream stream, string photoName)
    {
        await CreateBucket();

        var putObjectArgs = new PutObjectArgs().WithBucket(_bucketName).WithStreamData(stream).WithObject(photoName).WithObjectSize(stream.Length);
        var returnUpload = await _minioClient.PutObjectAsync(putObjectArgs);

        return returnUpload.ObjectName;
    }


    private async Task CreateBucket()
    {
        bool isExist = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucketName));
        if (!isExist)
            await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName));
    }
}
