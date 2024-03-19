using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Application.AppEntry;

public interface ICommandHandler
{
    Task<Result> Handle(ICommand command);
}