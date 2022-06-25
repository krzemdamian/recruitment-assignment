using System;
using Common.Domain.ValueObjects.Identifiers;

namespace Common.Domain.User.ValueObjects
{
    public class UserId : GuidIdValueObject
    {
        public static UserId Void { get; } = new( Guid.Empty );

        public UserId() : base()
        {
        }

        public UserId(Guid id) : base(id)
        {
        }

        // Note DK: Null object pattern can be used.
        public static UserId CreateOrNull(Guid? id) => id is null ? Void : new UserId(id.Value);
    }
}
