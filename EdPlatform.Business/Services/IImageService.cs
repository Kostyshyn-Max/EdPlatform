namespace EdPlatform.Business.Services;

public interface IImageService
{
    Task<(byte[]?, string)> DownloadFileAsync(string file);

    Task<bool> UploadFileAsync(byte[] fileStream, string name, string contentType);

    Task<bool> DeleteFileAsync(string fileName);
}