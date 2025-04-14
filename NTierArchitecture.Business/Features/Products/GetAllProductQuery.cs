using MediatR;
using Microsoft.EntityFrameworkCore;
using NTierArchitecture.Entities.Models;
using NTierArchitecture.Entities.Repositories;

namespace NTierArchitecture.Business.Features.Products;
public sealed record GetAllProductQuery() : IRequest<List<Product>>;

internal sealed class GetAllProductQueryHandler(
    IProductRepository productRepository) : IRequestHandler<GetAllProductQuery, List<Product>>
{
    public async Task<List<Product>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        List<Product> products = await productRepository
            .GetAll()
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);
        return products;
    }
}
