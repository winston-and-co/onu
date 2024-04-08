// https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/how-to-define-value-equality-for-a-type
// https://stackoverflow.com/questions/52854129/unity-doesnt-serialize-int-field
using System;

/* I literally made this solely because int? can't be serialized so it doesn't
 * show in the Unity inspector lol. It should be mostly hot-swappable with
 * regular int?, but I had to change instances of "v ?? x" with "v.OrIfNull(x)"
 */
[Serializable]
public class NullableInt : IEquatable<NullableInt>, IComparable<NullableInt>
{
    public int Value;
    public bool IsNull;

    public int OrIfNull(int v)
    {
        if (IsNull) return v;
        return Value;
    }

    public override bool Equals(object obj) => obj is NullableInt other && this.Equals(other);

    public bool Equals(NullableInt v)
    {
        if (IsNull && v.IsNull) return true; // if both null, equal
        return Value == v.Value && IsNull == v.IsNull;
    }

    public bool Equals(int v)
    {
        if (IsNull) return false;
        return Value == v;
    }

    public int CompareTo(NullableInt other)
    {
        if (other == null) return IsNull ? 0 : 1;
        if (IsNull && other.IsNull) return 0;
        if (!IsNull && other.IsNull) return 1;
        if (IsNull && !other.IsNull) return -1;
        return Value.CompareTo(other.Value);
    }

    public int CompareTo(int other)
    {
        if (IsNull) return -1;
        return Value.CompareTo(other);
    }

    public override string ToString()
    {
        if (IsNull) return "Null";
        return Value.ToString();
    }

    public override int GetHashCode() => (Value, IsNull).GetHashCode();

    public static bool operator ==(NullableInt lhs, NullableInt rhs) => lhs.Equals(rhs);
    public static bool operator !=(NullableInt lhs, NullableInt rhs) => !(lhs == rhs);
    public static bool operator ==(NullableInt lhs, int rhs) => lhs.Equals(rhs);
    public static bool operator !=(NullableInt lhs, int rhs) => !(lhs == rhs);
    public static bool operator ==(int lhs, NullableInt rhs) => rhs.Equals(lhs);
    public static bool operator !=(int lhs, NullableInt rhs) => !(lhs == rhs);

    public static bool operator <(NullableInt lhs, NullableInt rhs) => lhs.CompareTo(rhs) < 0;
    public static bool operator >(NullableInt lhs, NullableInt rhs) => lhs.CompareTo(rhs) > 0;
    public static bool operator <(NullableInt lhs, int rhs) => lhs.CompareTo(rhs) < 0;
    public static bool operator >(NullableInt lhs, int rhs) => lhs.CompareTo(rhs) > 0;
    public static bool operator <(int lhs, NullableInt rhs) => rhs.CompareTo(lhs) > 0;
    public static bool operator >(int lhs, NullableInt rhs) => rhs.CompareTo(lhs) < 0;
    public static bool operator <=(NullableInt lhs, NullableInt rhs) => lhs.CompareTo(rhs) <= 0;
    public static bool operator >=(NullableInt lhs, NullableInt rhs) => lhs.CompareTo(rhs) >= 0;
    public static bool operator <=(NullableInt lhs, int rhs) => lhs.CompareTo(rhs) <= 0;
    public static bool operator >=(NullableInt lhs, int rhs) => lhs.CompareTo(rhs) >= 0;
    public static bool operator <=(int lhs, NullableInt rhs) => rhs.CompareTo(lhs) >= 0;
    public static bool operator >=(int lhs, NullableInt rhs) => rhs.CompareTo(lhs) <= 0;
}