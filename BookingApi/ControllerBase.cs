using System;

namespace Ploeh.Samples.BookingApi
{
    public abstract class ControllerBase
    {
        // This signature is odd, since it specifically takes an int, and an
        // int only, as an argument. Normally, one would expect that the type
        // the argument is untyped as any object, or else that the method is
        // generically typed, and that the value is of the type T.
        // This specific type, though, is to catch type errors in the example
        // code, in lieu of unit tests.
        public IActionResult Ok(int value)
        {
            return new OkActionResult(value);
        }

        public IActionResult InternalServerError(string msg)
        {
            return new InternalServerErrorActionResult(msg);
        }
    }
}