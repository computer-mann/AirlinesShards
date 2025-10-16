using ErrorOr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airlines.Common.Extensions
{
    public static class MethodExtensions
    {
        public static Error GetError<TValue>(this ErrorOr<TValue> error)
        {
            return error.FirstError;
        }
    }
}
