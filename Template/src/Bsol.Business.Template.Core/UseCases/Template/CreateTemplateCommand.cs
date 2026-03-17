using Ardalis.Result;
using Ardalis.SharedKernel;


namespace Bsol.Business.Template.Core.UseCases.Template;

public record CreateTemplateCommand(string Name, int PokemonId) : ICommand<Result<Guid>>
{
}
