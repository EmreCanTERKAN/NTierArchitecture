using MediatR;
using NTierArchitecture.Entities.Models;
using NTierArchitecture.Entities.Repositories;

namespace NTierArchitecture.Business.Features.Products;
public sealed record CreateProductCommand(
    string Name,
    decimal Price,
    int Quantity,
    Guid CategoryId) : IRequest;

internal sealed class CreateProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateProductCommand>
{
    public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        bool isProductExist = await productRepository.AnyAsync(p => p.Name == request.Name,cancellationToken);

        if (isProductExist)
        {
            throw new ArgumentException("Bu daha önce oluşturulmuş");
        }

        Product product = new()
        {
            Name = request.Name,
            Price = request.Price,
            Quantity = request.Quantity,
            CategoryId = request.CategoryId
        };

        await productRepository.AddAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

