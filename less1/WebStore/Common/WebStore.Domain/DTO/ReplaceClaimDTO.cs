using System.Security.Claims;

namespace WebStore.Domain.DTO
{
    public class ReplaceClaimDTO : UserDTO
    {
        /// <summary>
        /// Старое утверждение
        /// </summary>
        public Claim Claim { get; set; }
        /// <summary>
        /// Новое утверждение
        /// </summary>
        public Claim NewClaim { get; set; }
    }
}