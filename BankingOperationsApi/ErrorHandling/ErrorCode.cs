using System.ComponentModel;

namespace BankingOperationsApi.ErrorHandling
{
    public enum ErrorCode
    {
        [Description("خطای داخلی .")]
        InternalError = 1001,

        [Description("خطای داخلی در دیتابیس.")]
        InternalDBError = 1002,

        [Description("خطای داخلی در سریالایز مدل ")]
        InternalSerializeError = 1003,

        [Description("شناسه درخواست تکراری است.")]
        DuplicateRequestId = 1040,

        [Description("پارامتر ورودی نامعتبر است.")]
        InputNotValid = 1050,

        [Description("پارامتر کوکی نامعتبر است.")]
        CookieNotValid = 1060,

        [Description("خطای داخلی در کانکشن دیتابیس.")]
        InternalDBConnectionError = 1012,

        [Description("خطا در فراخوانی سرویس های ساتنای فرابوم.")]
        SatnaTransferApiError = 1070,

        [Description("عملیات با موفقیت انجام شد")]
        Success = 0,

        [Description("خطایی در سرور رخ داده است")]
        ServerError = 1,

        [Description("پارامتر های ارسالی معتبر نیستند")]
        BadRequest = 2,

        [Description("یافت نشد")]
        NotFound = 3,
    }
}

