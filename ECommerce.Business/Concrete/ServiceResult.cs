using System.Net;

namespace ECommerce.Business
{
    public class ServiceResult
    {
        public bool Success { get; set; } // İşlemin başarılı olup olmadığını belirtir
        public string Message { get; set; } // İşlem hakkında bilgi veren mesaj
        public HttpStatusCode StatusCode { get; set; } // HTTP yanıt kodu

        // Başarı durumunda kullanılacak ServiceResult nesnesi oluşturur
        public static ServiceResult SuccessResult(string message, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ServiceResult { Success = true, Message = message, StatusCode = statusCode };
        }

        // Başarısızlık durumunda kullanılacak ServiceResult nesnesi oluşturur
        public static ServiceResult FailureResult(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult { Success = false, Message = message, StatusCode = statusCode };
        }
    }

    // Genel ServiceResult sınıfından türetilmiş ve veri taşıyan versiyonu
    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; set; } // İşlem sonucunda döndürülecek veri

        // Başarı durumunda kullanılacak ServiceResult<T> nesnesi oluşturur
        public static ServiceResult<T> SuccessResult(T data, string message, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ServiceResult<T> { Data = data, Success = true, Message = message, StatusCode = statusCode };
        }

        // Başarısızlık durumunda kullanılacak ServiceResult<T> nesnesi oluşturur
        public static ServiceResult<T> FailureResult(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T> { Success = false, Message = message, StatusCode = statusCode };
        }
    }
}
