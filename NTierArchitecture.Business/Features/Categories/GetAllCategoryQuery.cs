using MediatR;
using Microsoft.EntityFrameworkCore;
using NTierArchitecture.Entities.Models;
using NTierArchitecture.Entities.Repositories;

namespace NTierArchitecture.Business.Features.Categories;
public sealed record GetAllCategoryQuery() : IRequest<List<Category>>;

internal sealed class GetAllCategoryQueryHandler(
    ICategoryRepository categoryRepository) : IRequestHandler<GetAllCategoryQuery, List<Category>>
{
    public async Task<List<Category>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
       List<Category> categories = await categoryRepository
            .GetAll()
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

        return categories;
    }
}
