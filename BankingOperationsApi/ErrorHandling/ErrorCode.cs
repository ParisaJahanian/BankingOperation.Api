using System.ComponentModel;

namespace BankingOperationsApi.ErrorHandling
{
    public enum ErrorCode
    {
        [Description("عملیات با موفقیت انجام شد")]
        Success = 0,

        [Description("خطایی در سرور رخ داده است")]
        ServerError = 1,

        [Description("پارامتر های ارسالی معتبر نیستند")]
        BadRequest = 2,

        [Description("یافت نشد")]
        NotFound = 3,

        [Description("خطای داخلی .")]
        InternalError = 1001,

        [Description("خطای داخلی در دیتابیس.")]
        InternalDBError = 1002,

        [Description("خطای داخلی در سریالایز مدل ")]
        InternalSerializeError = 1003,

        [Description("شناسه درخواست تکراری است.")]
        DuplicateRequestId = 1004,

        [Description("پارامتر ورودی نامعتبر است.")]
        InputNotValid = 1005,

        [Description("پارامتر کوکی نامعتبر است.")]
        CookieNotValid = 1006,

        [Description("خطای داخلی در کانکشن دیتابیس.")]
        InternalDBConnectionError = 1007,

        [Description("خطا در فراخوانی سرویس های ساتنای فرابوم.")]
        SatnaTransferApiError = 1008,

        [Description("خطا در به روز رسانی توکنهای ساتنای فرابوم.")]
        SatnaTransferTokenApiError = 1009,

        [Description("توکن برای فراخوانی سرویس های ساتنا یافت نشد.")]
        SatnaTokenApiError = 1010,

        [Description("خطا در فراخوانی سرویس های پایای فرابوم.")]
        PayaTransferApiError = 1011,

        [Description("توکن برای فراخوانی سرویس های پایا یافت نشد.")]
        PayaTransferTokenApiError = 1012,
    }
}

