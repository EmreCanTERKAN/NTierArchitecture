using MediatR;
using NTierArchitecture.Entities.Models;
using NTierArchitecture.Entities.Repositories;

namespace NTierArchitecture.Business.Features.Categories;
public sealed record DeleteCategoryByIdCommand(
    Guid Id) : IRequest;


internal sealed class DeleteCategoryByIdCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteCategoryByIdCommand>
{
    public async Task Handle(DeleteCategoryByIdCommand request, CancellationToken cancellationToken)
    {
        Category category = await categoryRepository.GetByIdAsync(p => p.Id == request.Id, cancellationToken);

        if (category is null)
        {
            throw new ArgumentNullException(nameof(category));
        }

        categoryRepository.Remove(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
         
    
}
