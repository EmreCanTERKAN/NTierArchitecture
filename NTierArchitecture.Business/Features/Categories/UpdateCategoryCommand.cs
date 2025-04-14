using MediatR;
using NTierArchitecture.Entities.Models;
using NTierArchitecture.Entities.Repositories;

namespace NTierArchitecture.Business.Features.Categories;
public sealed record UpdateCategoryCommand(
    Guid Id,
    string Name) : IRequest;

internal sealed class UpdateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateCategoryCommand>
{
    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category category = await categoryRepository.GetByIdAsync(p => p.Name == request.Name,cancellationToken);

        if (category is null)
        {
            throw new ArgumentException("Kategori bulunamadı");
        }

        if (category.Name != request.Name)
        {
            var isCategoryNameExist = await categoryRepository.AnyAsync(p => p.Name == request.Name, cancellationToken);

            if (isCategoryNameExist)
            {
                throw new ArgumentException("Bu kategori daha önce oluşturulmuş!");
            }
        }
        category.Name = request.Name;

        await unitOfWork.SaveChangesAsync(cancellationToken);

    }
}
