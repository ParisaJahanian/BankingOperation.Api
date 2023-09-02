namespace BankingOperationsApi.ErrorHandling
{
    public class ErrorCodesProvider
    {
        public int SafeReponseCode { get; set; }
        public int OutReponseCode { get; set; }
        public string? SafeReponseMessage { get; set; }
        public string? SafeReponseMesageDecription { get; set; }
        public ErrorCodesProvider errorCodesResponseResult(string input) => input switch
        {

            "invalid.credentials" or "401" or "Unauthorized" => new ErrorCodesProvider()
            {
                SafeReponseCode = 401,
                SafeReponseMessage = "UnAuthorized",
                OutReponseCode = 401,
                SafeReponseMesageDecription = " توکن نامعتبر _ سطح دسترسی غیرمجاز است"
            },
            "handler.not_found" => new ErrorCodesProvider
            {
                SafeReponseCode = 402,
                SafeReponseMessage = "HandlerNotFound",
                OutReponseCode = 402,
                SafeReponseMesageDecription = "ساز و کاری یافت نشد"
            },
            "forbidden" => new ErrorCodesProvider
            {
                SafeReponseCode = 403,
                SafeReponseMessage = "Forbidden",
                OutReponseCode = 403,
                SafeReponseMesageDecription = "دسترسی غیرمجاز"
            },
            "Client.not_found" => new ErrorCodesProvider
            {
                SafeReponseCode = 404,
                SafeReponseMessage = "ClientNotFound",
                OutReponseCode = 404,
                SafeReponseMesageDecription = "سرویس گیرنده یافت نشد"
            },
            "secretKey.length_exceeded" => new ErrorCodesProvider
            {
                SafeReponseCode = 405,
                SafeReponseMessage = "SecretKeyExceeded",
                OutReponseCode = 403,
                SafeReponseMesageDecription = "طول کلید رمز پیش از حد مجاز است"
            },
            "apiKey.length_exceeded" => new ErrorCodesProvider
            {
                SafeReponseCode = 406,
                SafeReponseMessage = "ApiKeyExceeded",
                OutReponseCode = 403,
                SafeReponseMesageDecription = "طول کلید api بیش از حد مجاز است"
            },
            "apiKey.is_required" => new ErrorCodesProvider
            {
                SafeReponseCode = 407,
                SafeReponseMessage = "ApiKeyIsRequired",
                OutReponseCode = 403,
                SafeReponseMesageDecription = "کلید api الزامی است"
            },
            "secretKey.is_required" => new ErrorCodesProvider
            {
                SafeReponseCode = 408,
                SafeReponseMessage = "SecretKeyIsRequired",
                OutReponseCode = 403,
                SafeReponseMesageDecription = "کلید رمز الزامی است"
            },
            "conflict" => new ErrorCodesProvider
            {
                SafeReponseCode = 409,
                SafeReponseMessage = "Conflict",
                OutReponseCode = 403,
                SafeReponseMesageDecription = "تداخل در آدرس آی پی ها"
            },

            "token.is_required" => new ErrorCodesProvider
            {
                SafeReponseCode = 410,
                SafeReponseMessage = "TokenIsRequired",
                OutReponseCode = 401,
                SafeReponseMesageDecription = "توکن الزامی است"
            },
            "invalid.tokens_pair" => new ErrorCodesProvider
            {
                SafeReponseCode = 411,
                SafeReponseMessage = "TokenNotValid",
                OutReponseCode = 401,
                SafeReponseMesageDecription = "توکن نامعتبر است"
            },
            "refreshToken.not_valid" => new ErrorCodesProvider
            {
                SafeReponseCode = 412,
                SafeReponseMessage = "RefreshTokenNotValid",
                OutReponseCode = 401,
                SafeReponseMesageDecription = "توکن بازیابی نا معتبر است"
            },
            "daily_limit. reached" => new ErrorCodesProvider
            {
                SafeReponseCode = 413,
                SafeReponseMessage = "DailyLimitExceeded",
                OutReponseCode = 403,
                SafeReponseMesageDecription = "محدودیت روزانه در فراخوانی سرویس/شما با این آی پی مجاز به فراخوانی نمی باشید"
            },
            "Shahkar Mismatch" => new ErrorCodesProvider
            {
                SafeReponseCode = 414,
                SafeReponseMessage = "ShahkarIsNotMach",
                OutReponseCode = 414,
                SafeReponseMesageDecription = "مالکیت شماره همراه وارد شده مربوط به شخص دیگری می باشد"
            },
            "mobileNumber.not_valid" => new ErrorCodesProvider
            {
                SafeReponseCode = 415,
                SafeReponseMessage = "MobileNumberNotValid",
                OutReponseCode = 415,
                SafeReponseMesageDecription = "شماره تلفن همراه نامعتبر است"
            },
            "-1" or "nationalCode.not_valid" => new ErrorCodesProvider
            {
                SafeReponseCode = 416,
                SafeReponseMessage = "NationalCodeNotValid",
                OutReponseCode = 416,
                SafeReponseMesageDecription = "کدملی نامعتبر است"

            },
            "invalid.request_body" or "400" or "BadRequest" => new ErrorCodesProvider
            {
                SafeReponseCode = 417,
                SafeReponseMessage = "RequestBodyNotValid",
                OutReponseCode = 417,
                SafeReponseMesageDecription = "پارامترهای ورودی نامعتبر است"
            },
            "amount is null or zero" => new ErrorCodesProvider
            {
                SafeReponseCode = 418,
                SafeReponseMessage = "AmountIsNull/Zero",
                OutReponseCode = 418,
                SafeReponseMesageDecription = "فیلد مبلغ خالی است"
            },
            " terminal.id.is.empty" => new ErrorCodesProvider
            {
                SafeReponseCode = 419,
                SafeReponseMessage = "TerminalIsNull",
                OutReponseCode = 419,
                SafeReponseMesageDecription = "فیلد ترمینال خالی است"
            },
            "wallet.balance.not.sufficient" => new ErrorCodesProvider
            {
                SafeReponseCode = 420,
                SafeReponseMessage = "InsufficientWalletBalance",
                OutReponseCode = 420,
                SafeReponseMesageDecription = "موجودی کیف پول ناکافی است"
            },
            "no.asset.found" => new ErrorCodesProvider
            {
                SafeReponseCode = 421,
                SafeReponseMessage = "AssetPriceIsNull",
                OutReponseCode = 421,
                SafeReponseMesageDecription = "قیمت دارایی خالی است"
            },

            "ref.code.is.empty" => new ErrorCodesProvider
            {
                SafeReponseCode = 422,
                SafeReponseMessage = "RefrenceCodeIsNull",
                OutReponseCode = 422,
                SafeReponseMesageDecription = "فیلد کد ارجاع خالی است"
            },

            "currency.is.invalid" => new ErrorCodesProvider
            {
                SafeReponseCode = 423,
                SafeReponseMessage = "CurrencyNotValid",
                OutReponseCode = 423,
                SafeReponseMesageDecription = "ارز نامعتبر است"
            },

            "irt.currency.is.invalid" => new ErrorCodesProvider
            {
                SafeReponseCode = 424,
                SafeReponseMessage = "IRTCurrencyNotValid",
                OutReponseCode = 424,
                SafeReponseMesageDecription = "آی آر تی ارز نامعتبر است"
            },

            "terminal.not.found" => new ErrorCodesProvider
            {
                SafeReponseCode = 425,
                SafeReponseMessage = "TerminalNotFound",
                OutReponseCode = 425,
                SafeReponseMesageDecription = "ترمینال مورد نظر یافت نشد"
            },

            "coin amount request more than 1" => new ErrorCodesProvider
            {
                SafeReponseCode = 426,
                SafeReponseMessage = "CoinAmountRequestMoreThanOne",
                OutReponseCode = 426,
                SafeReponseMesageDecription = "درخواست مبلغ سکه بیشتر از یک می باشد"
            },

            "coin.number.is.not.one" => new ErrorCodesProvider
            {
                SafeReponseCode = 427,
                SafeReponseMessage = "CoinNumberRequestMoreThanOne",
                OutReponseCode = 427,
                SafeReponseMesageDecription = "درخواست تعداد سکه بیشتر از یک می باشد"
            },

            "user.not_found" => new ErrorCodesProvider
            {
                SafeReponseCode = 428,
                SafeReponseMessage = "UserNotFound",
                OutReponseCode = 428,
                SafeReponseMesageDecription = "کاربر یافت نشد"
            },
            "user.id.is.empty" => new ErrorCodesProvider
            {
                SafeReponseCode = 429,
                SafeReponseMessage = "UserIdIsNull",
                OutReponseCode = 429,
                SafeReponseMesageDecription = "شناسه کاربر خالی است"
            },
            "invalid.request.id" => new ErrorCodesProvider
            {
                SafeReponseCode = 430,
                SafeReponseMessage = "RequestIdNotValid",
                OutReponseCode = 430,
                SafeReponseMesageDecription = "شناسه درخواست نامعتبر است"
            },
            "transaction.not.found" => new ErrorCodesProvider
            {
                SafeReponseCode = 431,
                SafeReponseMessage = "TransactionNotFound",
                OutReponseCode = 431,
                SafeReponseMesageDecription = "تراکنش یافت نشد"
            },
            "wallet.irt.balance.not.sufficient" => new ErrorCodesProvider
            {
                SafeReponseCode = 432,
                SafeReponseMessage = "InsufficientIRTWalletBalance",
                OutReponseCode = 432,
                SafeReponseMesageDecription = "موجودی کیف پول آی آر تی ناکافی است"
            },
            "user.not.registered" => new ErrorCodesProvider
            {
                SafeReponseCode = 433,
                SafeReponseMessage = "UserNotRegistered",
                OutReponseCode = 433,
                SafeReponseMesageDecription = "کاربر ثبت نشده است"
            },
            "user.already.registered" => new ErrorCodesProvider
            {
                SafeReponseCode = 434,
                SafeReponseMessage = "UserAlreadyRegistered",
                OutReponseCode = 434,
                SafeReponseMesageDecription = "کاربر قبلا ثبت شده است"
            },
            "NationalCodeIsRequired" => new ErrorCodesProvider
            {
                SafeReponseCode = 435,
                SafeReponseMessage = "NationalCodeIsRequired",
                OutReponseCode = 416,
                SafeReponseMesageDecription = "کدملی اجباری است"
            },
            "user.is.inactive" => new ErrorCodesProvider
            {
                SafeReponseCode = 436,
                SafeReponseMessage = "UserNotActive",
                OutReponseCode = 436,
                SafeReponseMesageDecription = "کاربر غیرفعال است"
            },
            "admin.wallet.not.found" => new ErrorCodesProvider
            {
                SafeReponseCode = 437,
                SafeReponseMessage = "AdminWalletNotFound",
                OutReponseCode = 437,
                SafeReponseMesageDecription = "کیف پول ادمین یافت نشد"
            },
            "admin.wallet.balance.not.sufficient" => new ErrorCodesProvider
            {
                SafeReponseCode = 438,
                SafeReponseMessage = "InsufficientAdminWalletBalance",
                OutReponseCode = 438,
                SafeReponseMesageDecription = "موجودی کیف پول ادمین ناکافی است"
            },
            "balance.can.not.bigger" => new ErrorCodesProvider
            {
                SafeReponseCode = 439,
                SafeReponseMessage = "BalanceIsMax",
                OutReponseCode = 439,
                SafeReponseMesageDecription = "موجودی بیشتر از حدمجاز است"
            },
            "binance.order.validation.failed" => new ErrorCodesProvider
            {
                SafeReponseCode = 440,
                SafeReponseMessage = "BinanceOrderNotValid",
                OutReponseCode = 440,
                SafeReponseMesageDecription = "اعتبار سنجی سفارش باینانس ناموفق می باشد"
            },
            "asset.price.is.empty" => new ErrorCodesProvider
            {
                SafeReponseCode = 441,
                SafeReponseMessage = "AssetNotFound",
                OutReponseCode = 441,
                SafeReponseMesageDecription = "فیلد دارایی خالی می باشد"
            },
            "-100" or "-200" or "-300" or "-301" or "-302" or "-303" or "-304" or "-305" => new ErrorCodesProvider
            {
                SafeReponseCode = Convert.ToInt32(input),
                SafeReponseMessage = "CallServiceExeption",
                OutReponseCode = 403,
                SafeReponseMesageDecription = "خطا در عملیات سرویس های ناجی"
            },

            "-210" or "-310" => new ErrorCodesProvider
            {
                SafeReponseCode = Convert.ToInt32(input),
                SafeReponseMessage = "PlateNumberNotValid",
                OutReponseCode = 443,
                SafeReponseMesageDecription = "شماره پلاک صحیح نمی باشد"
            },
            "-307" => new ErrorCodesProvider
            {
                SafeReponseCode = Convert.ToInt32(input),
                SafeReponseMessage = "PlateNumberNotFound",
                OutReponseCode = 444,
                SafeReponseMesageDecription = "پلاکی برای فرد یافت نشد"
            },
            "-101" or "-103" or "-309" or "-311" => new ErrorCodesProvider
            {
                SafeReponseCode = Convert.ToInt32(input),
                SafeReponseMessage = "TrackingNoNotFound",
                OutReponseCode = 445,
                SafeReponseMesageDecription = "در سرویس استعلام اطلاعاتی یافت نشد"
            },
            "-308" or "-51" => new ErrorCodesProvider
            {
                SafeReponseCode = Convert.ToInt32(input),
                SafeReponseMessage = "TraceIdNotFound",
                OutReponseCode = 446,
                SafeReponseMesageDecription = " کد رهگیری تکراری است / کد رهگیری یافت نشد"
            },
            "-50" => new ErrorCodesProvider
            {
                SafeReponseCode = Convert.ToInt32(input),
                SafeReponseMessage = "InVaidIp",
                OutReponseCode = 403,
                SafeReponseMesageDecription = "شما با این آیپی مجاز به فراخوانی نمی باشید"
            },
            "502" => new ErrorCodesProvider
            {
                SafeReponseCode = 450,
                SafeReponseMessage = "PlateNo&NationalCodeNotMatched",
                OutReponseCode = 450,
                SafeReponseMesageDecription = "شماره پلاک خودرو با کدملی مطابقت ندارد"
            },
            "0" => new ErrorCodesProvider
            {
                SafeReponseCode = 448,
                SafeReponseMessage = "VerificationCodeNotValid",
                OutReponseCode = 448,
                SafeReponseMesageDecription = "پیامک ارسال نشد ، ارسال پیامک با مشکل مواجه شده است"
            },
            "MobileNumberIsRequired" => new ErrorCodesProvider
            {
                SafeReponseCode = 447,
                SafeReponseMessage = "MobileNumberIsRequired",
                OutReponseCode = 415,
                SafeReponseMesageDecription = "فیلد شماره تلفن همراه الزامی است"
            },
            "VerificationCodeNotValid" => new ErrorCodesProvider
            {
                SafeReponseCode = 448,
                SafeReponseMessage = "VerificationCodeNotValid",
                OutReponseCode = 448,
                SafeReponseMesageDecription = "کد اعتبار سنجی نامعتبر است"
            },
            "query_parameters.too_many" => new ErrorCodesProvider
            {
                SafeReponseCode = 449,
                SafeReponseMessage = "QueryParametersExceeded",
                OutReponseCode = 449,
                SafeReponseMesageDecription = "تعداد پارامترهای ارسالی بیشتر از حد مجاز است"
            },

            "Server.error" or "SERVER_ERROR" => new ErrorCodesProvider
            {
                SafeReponseCode = 500,
                SafeReponseMessage = "InternalServerError",
                OutReponseCode = 500,
                SafeReponseMesageDecription = "خطای داخلی سرور"
            },
            "NotImplemented" => new ErrorCodesProvider
            {
                SafeReponseCode = 501,
                SafeReponseMessage = "NotImplemented",
                OutReponseCode = 501,
                SafeReponseMesageDecription = "درخواست قابل شناسایی نیست"
            },
            "BadGateway" => new ErrorCodesProvider
            {
                SafeReponseCode = 502,
                SafeReponseMessage = "BadGateway",
                OutReponseCode = 502,
                SafeReponseMesageDecription = "سرور پاسخ نامعتبر دریافت می کند"
            },
            "providers.not_available" => new ErrorCodesProvider
            {
                SafeReponseCode = 503,
                SafeReponseMessage = "ServiceUnAvailable/ProvidersNotAvailable",
                OutReponseCode = 503,
                SafeReponseMesageDecription = "سرویس دهنده در دسترس نیست"
            },
            "GatewayTimeout" => new ErrorCodesProvider
            {
                SafeReponseCode = 504,
                SafeReponseMessage = "GatewayTimeout",
                OutReponseCode = 403,
                SafeReponseMesageDecription = "مدت زمان پاسخ گویی بیشتر از حد مجاز است"
            },
            "448" => new ErrorCodesProvider
            {
                SafeReponseCode = 448,
                SafeReponseMessage = "MismachVerifySmsCode",
                OutReponseCode = 448,
                SafeReponseMesageDecription = "کد اسمس نامعتبر می باشد"
            },
            _ => new ErrorCodesProvider
            {
                SafeReponseCode = 700,
                SafeReponseMessage = "کد خطای ناشناخته ارسال شده است",
                OutReponseCode = 700,
                SafeReponseMesageDecription = " خطای مورد نظر در لیست کد خطاهای رمزنگار سیف یافت نشد"

            },
            //  _ => throw new NotImplementedException()


        };

    }
}
