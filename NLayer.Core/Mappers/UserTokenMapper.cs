using NLayer.Core.Entities;
using NLayer.Core.Models;

namespace NLayer.Core.Mappers
{
    public class UserTokenMapper
    {
        public static UserTokenViewModel ToViewModel(UserToken userToken)
        {
            return new UserTokenViewModel
            {
                UserId = userToken.UserId,
                Token = userToken.Token,
                RefreshToken = userToken.RefreshToken,
                ExpireTime = userToken.ExpiredAt
            };
        }
    }
}
