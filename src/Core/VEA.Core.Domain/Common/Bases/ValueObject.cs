namespace VEA.Core.Domain.Common.Bases;

public abstract class ValueObject<T>
{
        protected T Value;

        protected ValueObject(T value)
        {
            Value = value;
        }
        
}