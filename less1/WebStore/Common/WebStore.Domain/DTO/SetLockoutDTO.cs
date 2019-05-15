using System;

namespace WebStore.Domain.DTO
{
    /// <summary>
    /// Время блокировки пользователя
    /// </summary>
    public class SetLockoutDTO : UserDTO
    {
        /// <summary>
        /// Временное смещение блокировки пользователя
        /// </summary>
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}