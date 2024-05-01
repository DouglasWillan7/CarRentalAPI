using DW.Rental.Domain.Exception;
using DW.Rental.Domain.Repositories;
using DW.Rental.Shareable.Requests.Deliveryman;
using MediatR;
using OperationResult;

namespace DW.Rental.Domain.Handlers.Deliveryman.Commands;

public sealed class PutDeliverymanPhotoCommandHandler : IRequestHandler<PutDeliverymanPhotoRequest, Result>
{
    private readonly IDeliverymanRepository _deliverymanRepository;
    private readonly IPhotoRepository _photoRepository;

    public PutDeliverymanPhotoCommandHandler(IDeliverymanRepository deliverymanRepository, IPhotoRepository photoRepository)
    {
        _deliverymanRepository = deliverymanRepository;
        _photoRepository = photoRepository;
    }

    public async Task<Result> Handle(PutDeliverymanPhotoRequest putDeliverymanPhotoRequest, CancellationToken cancellationToken)
    {
        var deliveryMan = await _deliverymanRepository.GetAsync(putDeliverymanPhotoRequest.DeliverymanId, cancellationToken);

        if (deliveryMan == null)
            return Result.Error(new NotFoundException("Deliveryman not found"));

        var photoName = await _photoRepository.UploadPhoto(putDeliverymanPhotoRequest.Photo, putDeliverymanPhotoRequest.DeliverymanId.ToString() + putDeliverymanPhotoRequest.FileName);
        _ = await _deliverymanRepository.UpdateFotoAsync(deliveryMan.Id, photoName, cancellationToken);


        return Result.Success();
    }
}
