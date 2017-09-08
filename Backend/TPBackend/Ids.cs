using System;

namespace TPBackend
{
    public struct CurrencyId : IEquatable<CurrencyId>
    {
        public CurrencyId(int idValue)
        {
            IdValue = idValue;
        }

        public int IdValue { get; }

        public bool Equals(CurrencyId other)
        {
            return IdValue == other.IdValue;
        }

        public override int GetHashCode()
        {
            return IdValue.GetHashCode();
        }
    }

    public struct CompanyId : IEquatable<CompanyId>
    {
        public CompanyId(int idValue)
        {
            IdValue = idValue;
        }

        public int IdValue { get; }

        public bool Equals(CompanyId other)
        {
            return IdValue == other.IdValue;
        }

        public override int GetHashCode()
        {
            return IdValue.GetHashCode();
        }
    }
}
