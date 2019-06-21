using System;
using System.Collections.Generic;
using System.Text;

namespace FaceDlib.Core.Models.ViewModel
{
    public class FaceDatabaseModel
    {


        public sealed class GroupModel : VerifyBase
        {
            /// <summary>
            /// 用户组id
            /// </summary>
            public string group_id { get; set; }
        }


        public sealed class GroupListModel : VerifyBase
        {
            int _start = 0;
            /// <summary>
            /// 默认值0，起始序号
            /// </summary>
            public int start { get { return _start; } set { _start = value; } }

            int _length = 0;
            /// <summary>
            /// 返回数量，默认值100，最大值1000
            /// </summary>
            public int length { get { return _length == 0 ? 100 : _length; } set { _length = value; } }

        }


        public sealed class UserFaceModel : VerifyBase
        {
            public string image { get; set; }
            /// <summary>
            /// 图片类型
            /// </summary>
            public string image_type { get; set; }
            /// <summary>
            /// 用户组id
            /// </summary>
            public string group_id { get; set; }
            /// <summary>
            /// 用户id
            /// </summary>
            public string user_id { get; set; }
            /// <summary>
            /// 用户资料
            /// </summary>
            public string user_info { get; set; }

            string _quality_control = string.Empty;
            /// <summary>
            /// 图片质量控制
            /// NONE: 不进行控制
            /// LOW:较低的质量要求
            /// NORMAL: 一般的质量要求
            /// HIGH: 较高的质量要求
            /// 默认 NONE
            /// 若图片质量不满足要求，则返回结果中会提示质量检测失败
            /// </summary>
            public string quality_control
            {
                get
                {
                    return string.IsNullOrWhiteSpace(_quality_control) ? "NONE" : _quality_control;
                }
                set
                {
                    _quality_control = value;
                }
            }


            string _liveness_control = string.Empty;
            /// <summary>
            /// 活体检测控制
            /// NONE: 不进行控制
            /// LOW:较低的活体要求(高通过率 低攻击拒绝率)
            /// NORMAL: 一般的活体要求(平衡的攻击拒绝率, 通过率)
            /// HIGH: 较高的活体要求(高攻击拒绝率 低通过率)
            /// 默认NONE
            /// 若活体检测结果不满足要求，则返回结果中会提示活体检测失败
            /// </summary>
            public string liveness_control
            {
                get
                {
                    return string.IsNullOrWhiteSpace(_liveness_control) ? "NONE" : _liveness_control;
                }
                set
                {
                    _liveness_control = value;
                }
            }


        }


        public sealed class RemoveFaceModel : VerifyBase
        {
            /// <summary>
            /// 用户组id
            /// </summary>
            public string group_id { get; set; }
            /// <summary>
            /// 用户id
            /// </summary>
            public string user_id { get; set; }

            /// <summary>
            /// 需要删除的人脸图片token
            /// </summary>
            public string face_token { get; set; }
        }


        public sealed class GroupUserModel : VerifyBase
        {
            /// <summary>
            /// 用户组id
            /// </summary>
            public string group_id { get; set; }
            /// <summary>
            /// 用户id
            /// </summary>
            public string user_id { get; set; }
        }


        public sealed class GroupUserListModel : VerifyBase
        {
            /// <summary>
            /// 用户组id
            /// </summary>
            public string group_id { get; set; }

            int _start = 0;
            /// <summary>
            /// 默认值0，起始序号
            /// </summary>
            public int start { get { return _start; } set { _start = value; } }

            int _length = 0;
            /// <summary>
            /// 返回数量，默认值100，最大值1000
            /// </summary>
            public int length { get { return _length == 0 ? 100 : _length; } set { _length = value; } }

        }


        public sealed class CopyUserListModel : VerifyBase
        {
            /// <summary>
            /// 用户id
            /// </summary>
            public string user_id { get; set; }

            /// <summary>
            /// 从指定组里复制信息
            /// </summary>
            public string src_group_id { get; set; }

            /// <summary>
            /// 需要添加用户的组id
            /// </summary>
            public string dst_group_id { get; set; }


        }



        public sealed class UserList
        {
            public string api_user_info { get; set; }

            public string group_name { get; set; }
        }

    }

}
