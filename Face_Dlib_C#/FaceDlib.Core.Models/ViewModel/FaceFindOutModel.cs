using System;
using System.Collections.Generic;
using System.Text;

namespace FaceDlib.Core.Models.ViewModel
{
    public class FaceFindOutModel
    {
        public class User
        {
            /// <summary>
            /// 用户所属的group_id
            /// </summary>
            public string GroupId { get; set; }
            /// <summary>
            /// 用户的user_id
            /// </summary>
            public string UserId { get; set; }
            /// <summary>
            /// 注册用户时携带的user_info
            /// </summary>
            public string UserInfo { get; set; }
            /// <summary>
            /// 用户的匹配得分
            /// </summary>
            public float Score { get; set; }

        }

    }


}
