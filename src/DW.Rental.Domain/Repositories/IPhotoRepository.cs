namespace DW.Rental.Domain.Repositories;

public interface IPhotoRepository
{
    Task<string> UploadPhoto(Stream photoStream, string photoName);
}
