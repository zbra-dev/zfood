using System;

namespace ZFood.Core.Validators
{
    public class ValidatorResult
    {
        public Exception Exception { get; set; }

        public bool Success => Exception == null;
    }
}
