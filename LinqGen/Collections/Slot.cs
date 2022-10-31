namespace Cathei.LinqGen.Hidden
{
    public struct Slot<T>
    {
        internal int hashCode;      // Lower 31 bits of hash code, -1 if unused
        internal int next;          // Index of next entry, -1 if last
        internal T value;
    }
}
