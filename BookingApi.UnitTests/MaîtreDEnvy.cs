namespace Ploeh.Samples.BookingApi.UnitTests
{
    public static class MaîtreDEnvy
    {
        public static MaîtreD WithCapacity(
            this MaîtreD maîtreD,
            int newCapacity)
        {
            return new MaîtreD(newCapacity);
        }
    }
}