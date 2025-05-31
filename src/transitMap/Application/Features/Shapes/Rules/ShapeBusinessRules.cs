using Application.Features.Shapes.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Shapes.Rules;

public class ShapeBusinessRules : BaseBusinessRules
{
    private readonly IShapeRepository _shapeRepository;
    private readonly ILocalizationService _localizationService;

    public ShapeBusinessRules(IShapeRepository shapeRepository, ILocalizationService localizationService)
    {
        _shapeRepository = shapeRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ShapesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ShapeShouldExistWhenSelected(Shape? shape)
    {
        if (shape == null)
            await throwBusinessException(ShapesBusinessMessages.ShapeNotExists);
    }

    public async Task ShapeIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Shape? shape = await _shapeRepository.GetAsync(
            predicate: s => s.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ShapeShouldExistWhenSelected(shape);
    }
}