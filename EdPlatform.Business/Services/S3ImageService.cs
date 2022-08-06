using System.Net;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;

namespace EdPlatform.Business.Services;

public class S3ImageService : IImageService
{
    private readonly string _bucketName;
    private readonly AmazonS3Client _awsS3Client;

    public S3ImageService(IConfiguration configuration)
    {
        _bucketName = configuration["AWS:BucketName"];
        _awsS3Client = new(
            configuration["AWS:AccessID"],
            configuration["AWS:SecretName"],
            RegionEndpoint.GetBySystemName(configuration["AWS:Region"]));
    }

    public async Task<bool> UploadFileAsync(byte[] file, string name, string contentType)
    {
        try
        {
            using var fileStream = new MemoryStream(file);

            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = fileStream,
                Key = name,
                BucketName = _bucketName,
                ContentType = contentType,
                CannedACL = S3CannedACL.PublicRead
            };

            TransferUtility fileTransferUtility = new(_awsS3Client);

            await fileTransferUtility.UploadAsync(uploadRequest);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<(byte[]?, string)> DownloadFileAsync(string file)
    {
        MemoryStream? ms = null;

        GetObjectRequest getObjectRequest = new()
        {
            BucketName = _bucketName,
            Key = file
        };

        try
        {
            using var response = await _awsS3Client.GetObjectAsync(getObjectRequest);

            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                using (ms = new())
                {
                    await response.ResponseStream.CopyToAsync(ms);
                }
            }


            if (ms is null || ms.ToArray().Length < 1)
                return (null, "");

            return (ms.ToArray(), response.Headers.ContentType);
        }
        catch (Exception)
        {
            return (null, "");
        }
    }

    public async Task<bool> DeleteFileAsync(string fileName)
    {
        if (!IsFileExists(fileName))
            return false;

        return await DeleteFile(fileName);
    }

    private async Task<bool> DeleteFile(string fileName)
    {
        DeleteObjectRequest request = new()
        {
            BucketName = _bucketName,
            Key = fileName
        };

        var response = await _awsS3Client.DeleteObjectAsync(request);
        return response.HttpStatusCode == HttpStatusCode.NoContent;
    }

    public bool IsFileExists(string fileName)
    {
        try
        {
            GetObjectMetadataRequest request = new()
            {
                BucketName = _bucketName,
                Key = fileName
            };

            var response = _awsS3Client.GetObjectMetadataAsync(request).Result;

            return true;
        }
        catch (Exception ex)
        {
            if (ex.InnerException != null && ex.InnerException is AmazonS3Exception awsEx)
            {
                if (string.Equals(awsEx.ErrorCode, "NoSuchBucket"))
                    return false;

                else if (string.Equals(awsEx.ErrorCode, "NotFound"))
                    return false;
            }

            throw;
        }
    }
}