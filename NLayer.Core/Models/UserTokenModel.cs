﻿namespace NLayer.Core.Models
{
    public class UserTokenViewModel
    {
        public int UserId { get; set; }

        public string Token { get; set; } = null!;

        public string RefreshToken { get; set; } = null!;

        public DateTime ExpireTime { get; set; }

    }
}
