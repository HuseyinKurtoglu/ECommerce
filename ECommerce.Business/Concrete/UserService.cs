using System;
using ECommerce.Business;
using ECommerce.DataAcces.Absract;
using ECommerce.Entities;
using FluentValidation;
using System.Net;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<User> _userValidator;

    public UserService(IUserRepository userRepository, IValidator<User> userValidator)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
    }

    // Kullanıcı ID'sine göre kullanıcıyı getirir.
    public ServiceResult<User> GetUserById(int userId)
    {
        try
        {
            // Kullanıcı ID'sinin geçerli bir pozitif sayı olup olmadığını kontrol eder.
            if (userId <= 0)
            {
                return ServiceResult<User>.FailureResult("Lütfen geçerli bir kullanıcı ID'si giriniz.", HttpStatusCode.Conflict);
            }

            // Kullanıcıyı repository'den getirir.
            var user = _userRepository.GetUserById(userId);
            if (user == null)
            {
                // Kullanıcı bulunamazsa hata mesajı döner.
                return ServiceResult<User>.FailureResult("Kullanıcı bulunamadı.", HttpStatusCode.NotFound);
            }
            // Kullanıcı başarıyla bulunursa başarı mesajı ile birlikte kullanıcıyı döner.
            return ServiceResult<User>.SuccessResult(user, "Kullanıcı başarıyla getirildi.", HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            // Beklenmeyen bir hata oluşursa hata mesajı döner.
            return ServiceResult<User>.FailureResult($"Kullanıcı getirilirken bir hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
        }
    }

    // Yeni bir kullanıcı ekler.
    public ServiceResult<User> AddUser(User user)
    {
        try
        {
            // Kullanıcıyı doğrulamak için validator kullanır.
            var validationResult = _userValidator.Validate(user);
            if (!validationResult.IsValid)
            {
                // Doğrulama hataları varsa hata mesajlarını döner.
                return ServiceResult<User>.FailureResult(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)), HttpStatusCode.BadRequest);
            }

            // Kullanıcıyı repository'e ekler.
            _userRepository.AddUser(user);
            // Kullanıcı başarıyla eklenirse başarı mesajı ile birlikte kullanıcıyı döner.
            return ServiceResult<User>.SuccessResult(user, "Kullanıcı başarıyla eklendi.", HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            // Beklenmeyen bir hata oluşursa hata mesajı döner.
            return ServiceResult<User>.FailureResult($"Kullanıcı eklenirken bir hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
        }
    }

    // Mevcut bir kullanıcıyı günceller.
    public ServiceResult<User> UpdateUser(int userId, User user)
    {
        try
        {
            // Kullanıcı ID'sinin geçerli bir pozitif sayı olup olmadığını kontrol eder.
            if (userId <= 0)
            {
                return ServiceResult<User>.FailureResult("Lütfen geçerli bir kullanıcı ID'si giriniz.", HttpStatusCode.Conflict);
            }

            // Güncellenmek istenen kullanıcıyı repository'den alır.
            var existingUser = _userRepository.GetUserById(userId);
            if (existingUser == null)
            {
                // Kullanıcı bulunamazsa hata mesajı döner.
                return ServiceResult<User>.FailureResult("Kullanıcı bulunamadı.", HttpStatusCode.NotFound);
            }

            // Kullanıcıyı doğrulamak için validator kullanır.
            var validationResult = _userValidator.Validate(user);
            if (!validationResult.IsValid)
            {
                // Doğrulama hataları varsa hata mesajlarını döner.
                return ServiceResult<User>.FailureResult(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)), HttpStatusCode.BadRequest);
            }

            // Kullanıcının bilgilerini günceller.
            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.UpdatedDate = DateTime.UtcNow;

            // Güncellenmiş kullanıcıyı repository'de günceller.
            _userRepository.UpdateUser(existingUser);
            // Kullanıcı başarıyla güncellenirse başarı mesajı ile birlikte kullanıcıyı döner.
            return ServiceResult<User>.SuccessResult(existingUser, "Kullanıcı başarıyla güncellendi.", HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            // Beklenmeyen bir hata oluşursa hata mesajı döner.
            return ServiceResult<User>.FailureResult($"Kullanıcı güncellenirken bir hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
        }
    }
}
