using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Application.AppEntry;

public interface ICommandHandler<TCommand>
{
    Task<Result> Handle(TCommand command);
}