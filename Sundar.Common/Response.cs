using System;

namespace Sundar.Common
{
    public class Response
    {
        /// <summary>
        /// 操作消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 操作状态码，1为正常
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 数据集
        /// </summary>
        public object Data { get; set; }

        public Response()
        {
            Code = 1;
            Message = "操作成功";
        }
    }

    /// <summary>
    /// WEBAPI通用返回泛型基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T> : Response
    {
        /// <summary>
        /// 回传的结果
        /// </summary>
        public T Result { get; set; }
    }
}
