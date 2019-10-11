using Quake.Dto;
using Quake.Toolkit.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quake.Service.Gift
{
    public interface IGiftService
    {
        PrimitiveResponse BuyGift(int userId, int giftCount);
        EntityResponse<UserDto> AsAGift(int userId, List<UserDto> model);
        PrimitiveResponse GiftPopupShown(int userId);
    }
}
