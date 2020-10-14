using System;

namespace Payments.DTO.Models
{
    public class ErrorResponseModel
    {
        public ErrorResponseModel(string errorDescription)
        {
            ErrorDescription = errorDescription;
        }

        public string ErrorDescription { get; }
    }
}