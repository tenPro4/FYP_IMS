using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Dtos.Account
{
    public class RefreshTokenDto
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Used { get; set; }
        public bool Invalidate { get; set; }
        public int AccountId { get; set; }
        public string CreatedByIp { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiryDate;
        public DateTime? Revoked { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;
    }
}
