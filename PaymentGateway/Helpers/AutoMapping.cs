using AutoMapper;
using Payments;
using Payments.DTO;
using Payments.DTO.Models;

namespace PaymentGateway.Helpers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            //CreateMap<PaymentState, PaymentStatusCode>()
            //    .ForMember(dest =>
            //            dest,
            //        opt => opt.MapFrom(src => PaymentUtility.ConvertPaymentState(src)));

            //CreateMap<PaymentState, PaymentStatusCode>()
            //    .ForMember(dest =>
            //            dest,
            //        opt => opt.MapFrom(src => src == PaymentState.Failed));

            //CreateMap<PaymentState, PaymentStatusCode>()
            //    .ForMember(dest =>
            //            dest == PaymentStatusCode.PaymentSuccess,
            //        opt => opt.MapFrom(src => src == PaymentState.Success));



            CreateMap<CreditCardInfo, CreditCardModel>()
             .ForMember(dest =>
                dest.ExpiryDateYear,
                opt => opt.MapFrom(src => src.ExpiryDate.Year))
                .ForMember(dest =>
                dest.ExpiryDateMonth,
                opt => opt.MapFrom(src => src.ExpiryDate.Month))
                .ForMember(dest =>
                dest.CardNo,
                opt => opt.MapFrom(src => PaymentUtility.MaskCreditCardNo(src.CardNo)))
                .ForMember(dest =>
                dest.CVV,
                opt => opt.MapFrom(src => src.CVV));


            CreateMap<Payment, PaymentModel>()
            .ForMember(dest =>
                dest.Amount,
                opt => opt.MapFrom(src => src.PaymentAmount.Amount))
            .ForMember(dest =>
                dest.CreditCard,
                opt => opt.MapFrom(src => src.CreditCardInfo))
            .ForMember(dest =>
                dest.CurrencyCode,
                opt => opt.MapFrom(src => src.PaymentAmount.CurrencyCode));




        }
    }
}
