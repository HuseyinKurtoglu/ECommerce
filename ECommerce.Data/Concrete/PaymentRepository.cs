using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;

namespace ECommerce.DataAcces.Concrete.Dapper
{
    public class PaymentRepository : IPaymentRepository
    {
        // IDbConnection nesnesini saklar ve veritabanı işlemlerinde kullanılır.
        private readonly IDbConnection _dbConnection;

        // Constructor: Bağımlılık olarak verilen IDbConnection'ı alır ve _dbConnection değişkenine atar.
        public PaymentRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // Tüm ödemeleri getirir, ancak silinmiş ödemeleri hariç tutar.
        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            // SQL sorgusu: Silinmemiş ödemeleri seçer.
            var sql = "SELECT * FROM Payments WHERE IsDeleted = 0";
            // Dapper kullanarak SQL sorgusunu çalıştırır ve sonuçları döner.
            var payments = await _dbConnection.QueryAsync<Payment>(sql);
            return payments;
        }

        // Belirli bir ödeme bilgisini getirir, silinmiş ödemeleri hariç tutar.
        public async Task<Payment> GetPaymentByIdAsync(int paymentId)
        {
            // SQL sorgusu: Verilen PaymentId'ye sahip ve silinmemiş ödemeyi seçer.
            var sql = "SELECT * FROM Payments WHERE PaymentId = @PaymentId AND IsDeleted = 0";
            // Dapper kullanarak SQL sorgusunu çalıştırır ve sonucu döner. Eğer ödeme bulunamazsa, null döner.
            var payment = await _dbConnection.QueryFirstOrDefaultAsync<Payment>(sql, new { PaymentId = paymentId });
            return payment;
        }

        // Yeni bir ödeme ekler ve eklenen ödemenin ID'sini döner.
        public async Task<int> AddPaymentAsync(Payment payment)
        {
            // SQL sorgusu: Yeni ödeme bilgilerini Payments tablosuna ekler ve yeni eklenen ödemenin ID'sini döner.
            var sql = @"
                INSERT INTO Payments (OrderId, PaymentDate, Amount, PaymentMethod, CreatedDate, CreatedBy, IsActive, IsDeleted)
                VALUES (@OrderId, @PaymentDate, @Amount, @PaymentMethod, @CreatedDate, @CreatedBy, @IsActive, 0);
                SELECT CAST(SCOPE_IDENTITY() as int);";

            // Dapper kullanarak SQL sorgusunu çalıştırır ve eklenen ödemenin ID'sini döner.
            var paymentId = await _dbConnection.QuerySingleAsync<int>(sql, new
            {
                payment.OrderId,
                payment.PaymentDate,
                payment.Amount,
                payment.PaymentMethod,
                payment.CreatedDate,
                payment.CreatedBy,
                payment.IsActive
            });

            return paymentId;
        }

        // Var olan bir ödemeyi günceller. Silinmiş ödemeler güncellenemez.
        public async Task<int> UpdatePaymentAsync(int paymentId, Payment payment)
        {
            // SQL sorgusu: Belirtilen PaymentId'ye sahip ödemeyi günceller, ancak silinmiş ödemeler güncellenemez.
            var sql = @"
                UPDATE Payments
                SET OrderId = @OrderId,
                    PaymentDate = @PaymentDate,
                    Amount = @Amount,
                    PaymentMethod = @PaymentMethod,
                    UpdatedDate = @UpdatedDate,
                    UpdatedBy = @UpdatedBy,
                    IsActive = @IsActive
                WHERE PaymentId = @PaymentId AND IsDeleted = 0";

            // Payment nesnesine ID'sini atar.
            payment.PaymentId = paymentId;

            // Dapper kullanarak SQL sorgusunu çalıştırır ve etkilenen satır sayısını döner.
            var rowsAffected = await _dbConnection.ExecuteAsync(sql, new
            {
                payment.OrderId,
                payment.PaymentDate,
                payment.Amount,
                payment.PaymentMethod,
                payment.UpdatedDate,
                payment.UpdatedBy,
                payment.IsActive,
                payment.PaymentId
            });

            return rowsAffected;
        }

        // Belirli bir ödemeyi siler. Silme işlemi, ödemeyi 'IsDeleted' olarak işaretler.
        public async Task<int> DeletePaymentAsync(int paymentId, int deletedBy)
        {
            // SQL sorgusu: Belirtilen PaymentId'ye sahip ödemeyi silinmiş olarak işaretler.
            var sql = @"
                UPDATE Payments
                SET IsDeleted = 1,
                    DeletedDate = @DeletedDate,
                    DeletedBy = @DeletedBy
                WHERE PaymentId = @PaymentId";

            // Dapper kullanarak SQL sorgusunu çalıştırır ve etkilenen satır sayısını döner.
            var rowsAffected = await _dbConnection.ExecuteAsync(sql, new
            {
                DeletedDate = DateTime.UtcNow,
                DeletedBy = deletedBy,
                PaymentId = paymentId
            });

            return rowsAffected;
        }
    }
}
