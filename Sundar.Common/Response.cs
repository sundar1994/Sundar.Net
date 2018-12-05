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
}
