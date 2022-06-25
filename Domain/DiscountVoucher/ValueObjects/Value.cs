using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domain.ValueObjects.Money;

namespace Domain.DiscountVoucher.ValueObjects
{
    // Note DK: YAGNI & Is this correct inheritance relation?
    public class Value : MoneyValueObject
    {
        public Value() : base()
        {
        }

        public Value(decimal value) : base(value)
        {
        }

        public static Value CreateOrNull(decimal? value) => value is null ? null : new Value(value.Value);
    }
}