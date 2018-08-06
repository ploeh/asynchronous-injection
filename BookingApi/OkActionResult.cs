namespace Ploeh.Samples.BookingApi
{
    public class OkActionResult : IActionResult
    {
        public OkActionResult(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }
}