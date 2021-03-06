﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FaceDlib.Core.Models.Common
{
    public enum RequestEnum
    {
        Succeed = 200,

        Failed = 401,

        没有接口权限 = 6,
        请求总量超限额 = 19,

        没有预付费次数未开通余额付款 = 21,
        预付费次数为0且余额不足 = 22,




        操作日志添加失败 = 110000,

        签名错误 = 100001,
        必要参数未传入 = 222001,
        服务端请求失败 = 222001,
        图片中没有人脸 = 222102,
        无法解析人脸 = 222103,
        Base64图片格式错误 = 222104,
        从图片的url下载图片失败 = 222105,



        未找到匹配的用户 = 222207,
        face_token格式错误 = 222208,
        face_token不存在 = 222209,
        人脸图片添加失败 = 222300,
        获取人脸图片失败 = 222301,
        质量控制项错误 = 222302,
        活体控制项错误 = 222303,
        活体检测未通过 = 222304,
        缓存处理失败 = 222305,
        数据存储处理失败 = 222306,
        接口初始化失败 = 222307,
        图片尺寸太大 = 222308,
        正在清理该用户组的数据 = 222309,
        公安服务连接失败 = 222310,


        数据重复 = 223101,
        数据不存在 = 223102,





    }
}
