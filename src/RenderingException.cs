using System;

namespace PlantUml.Net
{
    public class RenderingException : Exception
    {
        public string Code { get; }

        public RenderingException(string code, string error) 
            : base(error)
        {
            Code = code;
        }
    }
}
