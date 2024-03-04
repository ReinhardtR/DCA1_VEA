namespace VEA.Core.Domain.Common.Bases;

public abstract class ValueObject<T>
{
        public T Value;

        protected ValueObject(T value)
        {
            Value = value;
        }
        
}