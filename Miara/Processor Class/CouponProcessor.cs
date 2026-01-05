using System;
using System.Data.SqlClient;

namespace Miara.Services
{
    public class CouponProcessor
    {
        private readonly string _connectionString;

        public CouponProcessor(string connectionString)
        {
            _connectionString = connectionString;
        }

        public decimal RedeemCoupon(
            string couponCode,
            int saleId,
            string sessionId,
            int emid,
            string device,
            out string message)
        {
            decimal discountToApply = 0;
            message = "";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        // 1️⃣ Check if a coupon was already used for this sale
                        SqlCommand saleCheckCmd = new SqlCommand(@"
                            SELECT COUNT(1)
                            FROM CouponRedemptions
                            WHERE SaleID = @saleId
                        ", conn, tran);

                        saleCheckCmd.Parameters.AddWithValue("@saleId", saleId);
                        int alreadyUsed = (int)saleCheckCmd.ExecuteScalar();
                        if (alreadyUsed > 0)
                        {
                            message = "Only one coupon is allowed per sale.";
                            return 0;
                        }
                        using (SqlCommand cmd = new SqlCommand(@"
        SELECT DiscountAmount, IsPercentage, IsActive, ExpiryDate
        FROM Coupons
        WHERE CouponCode = @code
    ", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@code", couponCode);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (!reader.Read())
                                {
                                    message = "Invalid coupon code.";
                                    return 0;
                                }

                                decimal discountValue = reader.GetDecimal(0);
                                bool isPercentage = reader.GetBoolean(1);
                                bool isActive = reader.GetBoolean(2);
                                DateTime expiry = reader.GetDateTime(3);

                                if (!isActive)
                                {
                                    message = "This coupon has already been used.";
                                    return 0;
                                }

                                if (DateTime.Now > expiry)
                                {
                                    message = "This coupon has expired.";
                                    return 0;
                                }

                                discountToApply = isPercentage ? discountValue / 100 : discountValue;
                                message = isPercentage
                                    ? $"Coupon applied: {discountValue}% off"
                                    : $"Coupon applied: R{discountValue} off";
                            } // reader automatically closed here
                        }

                        // 3️⃣ Insert redemption record
                        SqlCommand insertCmd = new SqlCommand(@"
                            INSERT INTO CouponRedemptions
                            (CouponCode, SaleID, DiscountApplied)
                            VALUES
                            (@code, @saleId, @discount)
                        ", conn, tran);

                        insertCmd.Parameters.AddWithValue("@code", couponCode);
                        insertCmd.Parameters.AddWithValue("@saleId", saleId);
                        insertCmd.Parameters.AddWithValue("@discount", discountToApply);
                        insertCmd.ExecuteNonQuery();

                        // 4️⃣ Deactivate coupon and store session/device info
                        SqlCommand updateCmd = new SqlCommand(@"
                            UPDATE Coupons
                            SET
                                IsActive = 0,
                                SessionID = @sessionID,
                                EMID = @emid,
                                Device = @device,
                                SaleID = @saleId
                            WHERE CouponCode = @code
                              AND IsActive = 1
                        ", conn, tran);

                        updateCmd.Parameters.AddWithValue("@sessionID", sessionId);
                        updateCmd.Parameters.AddWithValue("@emid", emid);
                        updateCmd.Parameters.AddWithValue("@device", device);
                        updateCmd.Parameters.AddWithValue("@saleId", saleId);
                        updateCmd.Parameters.AddWithValue("@code", couponCode);

                        int rows = updateCmd.ExecuteNonQuery();
                        if (rows == 0)
                            throw new Exception("Coupon was already redeemed.");

                        // 5️⃣ Commit transaction
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        message = "Coupon redemption failed: " + ex.Message;
                        return 0;
                    }
                }
            }

            return discountToApply;
        }
    }
}
