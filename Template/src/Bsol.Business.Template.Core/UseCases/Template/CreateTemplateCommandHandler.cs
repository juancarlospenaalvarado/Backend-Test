using Ardalis.Result;
using Ardalis.SharedKernel;
using Bsol.Business.Template.Core.Interfaces.Services;
using Bsol.Business.Template.Core.TemplateAggregate.Specifications;


namespace Bsol.Business.Template.Core.UseCases.Template;

public class CreateTemplateCommandHandler(SharedKernel.Interfaces.IRepository<TemplateAggregate.Template> _repository,
    IPokeApiService _pokeApiService) : ICommandHandler<CreateTemplateCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var pokemonId = request.PokemonId == 0 ? 1 : request.PokemonId;
            var spec = new TemplateByNameSpec(request.Name);
            var resultQuery = await _repository.ListAsync(spec, cancellationToken);
            var responsePokeApi = await _pokeApiService.EncounterMethod(pokemonId, cancellationToken);

            if (responsePokeApi.IsFailed)
            {
                return Result.Error(responsePokeApi.Errors[0].Message);
            }
            if (resultQuery.Count > 0)
            {
                return Result.Error("Ya existe una Template con el mismo nombre");
            }

            var newTemplate = new TemplateAggregate.Template(request.Name);
            var resultCreate = await _repository.AddAsync(newTemplate, cancellationToken);

            if (resultCreate == null) return Result.Error("No se puedo registrar el Template");

            return newTemplate.Id;
        }
        catch (Exception ex)
        {

            return Result.Error(ex.Message);
        }
    }
}
