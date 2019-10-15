using WrightWayRestaurant.Framework.Utility;
using WrightWayRestaurant.Model.Enums;

namespace WrightWayRestaurant.Model.Common
{
    public class ResultData<T> where T : new()
    {
        public ResultData()
        {
        }
        public ResultData(ResultStatusEnums resultStatusEnums)
        {
            Code = (int)resultStatusEnums;
            Message = EnumUtility.GetEnumDescription(resultStatusEnums);
            Data = new T();
        }

        public ResultData(ResultStatusEnums resultStatusEnums, string message)
        {
            Code = (int)resultStatusEnums;
            Message = message;
            Data = new T();
        }

        /// <summary>
        /// 返回的状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 返回的消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回对象
        /// </summary>
        public T Data { get; set; }
    }
    public class ResultData
    {
        public ResultData()
        {
        }

        public ResultData(ResultStatusEnums resultStatusEnums)
        {
            Code = (int)resultStatusEnums;
            Message = EnumUtility.GetEnumDescription(resultStatusEnums);
        }

        public ResultData(ResultStatusEnums resultStatusEnums, string message)
        {
            Code = (int)resultStatusEnums;
            Message = message;
        }

        /// <summary>
        /// 返回的状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 返回的消息
        /// </summary>
        public string Message { get; set; }

        public string Data { get; set; }
    }
}
