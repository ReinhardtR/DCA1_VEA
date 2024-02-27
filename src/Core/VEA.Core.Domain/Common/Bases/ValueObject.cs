using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain;

public abstract class ValueObject<T>
{
        protected T Value;

        protected ValueObject(T value)
        {
            Value = value;
        }
        
}