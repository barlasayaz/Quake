using Quake.Dto;
using Quake.Dto.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quake.Dto
{
    public class UserInfoDto : BaseDto
    {
        public string DeviceOS { get; set; }
        public string DeviceModel { get; set; }
        public string DeviceOSVersion { get; set; }
    }
}
