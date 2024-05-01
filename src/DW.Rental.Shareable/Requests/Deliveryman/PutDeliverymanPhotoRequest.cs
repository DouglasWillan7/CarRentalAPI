using MediatR;
using OperationResult;

namespace DW.Rental.Shareable.Requests.Deliveryman;

public record PutDeliverymanPhotoRequest(Stream Photo,string FileName,  Guid DeliverymanId) : IRequest<Result>;
