using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Application.AppEntry;

public interface ICommandHandler<in TCommand, TResult>
{
    Task<Result<TResult>> Handle(TCommand command);
}

public interface ICommandHandler<in TCommand>
{
    Task<Result> Handle(TCommand command);
}