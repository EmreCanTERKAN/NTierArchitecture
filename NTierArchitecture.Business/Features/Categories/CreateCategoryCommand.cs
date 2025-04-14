using FluentValidation;
using MediatR;
using NTierArchitecture.Entities.Models;
using NTierArchitecture.Entities.Repositories;

namespace NTierArchitecture.Business.Features.Categories;
public sealed record CreateCategoryCommand(
    string Name) : IRequest;

internal sealed class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("isim boş olamaz");
    }
}

internal sealed class CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateCategoryCommand>
{
    public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var isCategoryNameExist = await categoryRepository.AnyAsync(p => p.Name == request.Name, cancellationToken);

        if (isCategoryNameExist)
        {
            throw new ArgumentException("Bu kategori daha önce oluşturulmuş!");
        }

        Category category = new()
        {
            Name = request.Name
        };

        await categoryRepository.AddAsync(category, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
