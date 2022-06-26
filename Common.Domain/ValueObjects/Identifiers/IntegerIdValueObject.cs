using System;

namespace Common.Domain.ValueObjects.Identifiers
{
    public abstract class IntegerIdValueObject : IntegerValueObject, IId
    {
        protected IntegerIdValueObject(int id) : base(id)
        {
        }

        public object GetIdValue() => Value;
    }
}

