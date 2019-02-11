using System;
using System.Runtime.Serialization;

namespace ZFood.Web.DTO
{
    [Serializable]
    public class ErrorResponseDTO
    {
        [DataMember(Name = "Message")]
        public string Message { get; private set; }

        public ErrorResponseDTO(string message)
        {
            Message = message;
        }
    }
}
