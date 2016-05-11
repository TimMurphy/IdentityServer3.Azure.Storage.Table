using System;
using IdentityServer3.Core.Models;
using OpenMagic;
using OpenMagic.Extensions;

namespace IdentityServer3.Azure.Storage.Table.Specifications.Helpers
{
    public class DummyFactory : Dummy
    {
        public DummyFactory()
        {
            ValueFactories.Add(typeof(ScopeType), () => Enum.Parse(typeof(ScopeType), typeof(ScopeType).GetEnumNames().RandomItem()));
            ValueFactories.Add(typeof(DateTimeOffset?),() => DummyNullableDateTimeOffset());
        }

        private static DateTimeOffset? DummyNullableDateTimeOffset()
        {
            return RandomBoolean.Next() ? (DateTimeOffset?) null : new DateTimeOffset(RandomDateTime.Next());
        }
    }
}