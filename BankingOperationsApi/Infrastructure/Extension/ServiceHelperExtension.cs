﻿using System.Text.Json.Serialization;
using System.Text.Json;
using BankingOperationsApi.ErrorHandling;
using BankingOperationsApi.Models;
using Microsoft.OpenApi.Extensions;
using Microsoft.AspNetCore.Hosting;

namespace BankingOperationsApi.Infrastructure.Extension
{
    public static class ServiceHelperExtension
    {
        public static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            ReadCommentHandling = JsonCommentHandling.Skip,
            IgnoreNullValues = true
        };
        public static void AddCommonHeader(this HttpRequestMessage request)
        {
            request.Headers.Add("Accept", "application/json");
        }

        public static void AddFaraboomCommonHeader(this HttpRequestMessage request, FaraboomOptions options)
        {
            request.Headers.Add("App-Key", options.AppKey);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Client-Ip-Address", "127.0.0.1");
            request.Headers.Add("Client-Platform-Type", "WEB");
            request.Headers.Add("Client-Device-Id", options.DeviceId);
            request.Headers.Add("Client-User-Id", "09120000000");
            request.Headers.Add("Client-User-Agent", $"Apsan/Phoenix{typeof(StartupBase).Assembly.GetName().Version}");
        }
        //public static FormUrlEncodedContent CarTollsFormUrlEncodedContent(string UserName, string Password)
        //{
        //    var result = new Dictionary<string, string>
        //    {
        //        {"UserName", UserName},
        //        {"Password", Password},
        //    };
        //    var formUrlEncodedContent = new FormUrlEncodedContent(result);
        //    return formUrlEncodedContent;
        //}
        public static T GenerateApiErrorResponse<T>(ErrorCodesProvider errorCode) where T : ErrorResult, new()
        {
            return new T
            {
                StatusCode = errorCode.OutReponseCode.ToString(),
                IsSuccess = false,
                ResultMessage = errorCode?.SafeReponseMesageDecription,

            };
        }

        public static T GenerateErrorMethodResponse<T>(ErrorCode errorCode, string RequestId) where T : OutputModel, new()
        {
            return new T
            {
                StatusCode = errorCode.ToString(),
                RequestId = RequestId,
                Content = errorCode.GetDisplayName(),
            };
        }

    }
}
