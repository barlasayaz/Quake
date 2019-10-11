using Quake.Dto.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Quake.Dto
{
    public class UserDto : BaseDto
    {
        public string PushNotificationId { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsPhoneNumberVerified { get; set; }
        public bool IsPremium { get; set; }
        public virtual ICollection<UserAlarmDto> UserAlarmsIds { get; set; }
        public virtual ICollection<GroupDto> Groups { get; set; }
        public string LastLocation { get; set; }
        public DateTime LastLocationTime { get; set; }
        public int RefId { get; set; }
        public int GiftCount { get; set; }
        public int GiftPendingCount { get; set; }
        public bool Gift_Activate { get; set; }
        public bool Gift_Popup { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool DoesUserBuyIt { get; set; }

        public FriendshipStatus FriendshipStatus { get; set; }
        public DateTime AnswerDate { get; set; }

        public UserInfoDto UserInfo { get; set; }
    }
}
