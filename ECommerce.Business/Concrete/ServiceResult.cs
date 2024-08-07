public class ServiceResult
{
    // İşlemin başarılı olup olmadığını belirten özellik.
    public bool Success { get; protected set; }

    // İşlemle ilgili açıklayıcı mesaj.
    public string Message { get; protected set; }

    // Başarıyla sonuçlanan bir işlem için yeni bir `ServiceResult` oluşturur.
    public static ServiceResult SuccessResult(string message) =>
        new ServiceResult { Success = true, Message = message };

    // Başarısız bir işlem için yeni bir `ServiceResult` oluşturur.
    public static ServiceResult FailureResult(string message) =>
        new ServiceResult { Success = false, Message = message };
}

public class ServiceResult<T> : ServiceResult
{
    // İşlem sonucunda döndürülen veri.
    public T Data { get; protected set; }

    // Başarıyla sonuçlanan bir işlem için yeni bir `ServiceResult<T>` oluşturur.
    public static ServiceResult<T> SuccessResult(T data, string message) =>
        new ServiceResult<T> { Success = true, Data = data, Message = message };

    // Başarısız bir işlem için yeni bir `ServiceResult<T>` oluşturur.
    public static ServiceResult<T> FailureResult(string message) =>
        new ServiceResult<T> { Success = false, Message = message };
}
