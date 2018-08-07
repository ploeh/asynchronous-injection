namespace Ploeh.Samples.BookingApi
{
    public class InternalServerErrorActionResult : IActionResult
    {
        public InternalServerErrorActionResult(string msg)
        {
            Msg = msg;
        }

        public string Msg { get; }
    }
}