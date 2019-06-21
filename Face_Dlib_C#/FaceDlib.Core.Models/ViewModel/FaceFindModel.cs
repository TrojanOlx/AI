using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FaceDlib.Core.Models.ViewModel
{
    public class FaceFindModel
    {

        public class Search:VerifyBase {

            /// <summary>
            /// 图片文件
            /// </summary>
            [Required(ErrorMessage = "图片不能为空")]
            public string image { get; set; }
            /// <summary>
            /// 图片类型
            /// </summary>
            [Required(ErrorMessage = "图片类型不能为空")]
            public string image_type { get; set; }

            /// <summary>
            /// 图片类型
            /// </summary>
            [Required(ErrorMessage = "用户ID不能为空")]
            public string user_id { get; set; }
            /// <summary>
            /// 图片类型
            /// </summary>
            [Required(ErrorMessage = "分组ID不能为空")]
            public string group_id_list { get; set; }
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
            /// <summary>
            /// 图片类型
            /// </summary>
            [Required(ErrorMessage = "返回的用户数量不能为0")]
            public int max_user_num { get; set; }
        }

        public class MultiSearch:VerifyBase {
            /// <summary>
            /// 图片文件
            /// </summary>
            [Required(ErrorMessage = "图片不能为空")]
            public string image { get; set; }
            /// <summary>
            /// 图片类型
            /// </summary>
            [Required(ErrorMessage = "图片类型不能为空")]
            public string image_type { get; set; }

            /// <summary>
            /// 图片类型
            /// </summary>
            [Required(ErrorMessage = "分组ID不能为空")]
            public string group_id_list { get; set; }


            /// <summary>
            /// 匹配阀值
            /// </summary>
            public double match_threshold { get; set; }




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
            /// <summary>
            /// 图片类型
            /// </summary>
            [Required(ErrorMessage = "返回的用户数量不能为0")]
            public int max_user_num { get; set; }

        }

    }


}
