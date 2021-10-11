using EntityFramework.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityFramework.Entities
{
    public partial class RefreshToken : IBaseEntity
    {
        [Key]
        public string Id { get; set; }
        public string Token { get; set; }
        public string JwtId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CreatedByIp { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiryDate;
        public DateTime? Revoked { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;

        public int AccountId { get; set; }
        [ForeignKey(nameof(AccountId))]
        public MasterAccount MasterAccount { get; set; }
    }
}
