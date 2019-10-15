using System.ComponentModel;

namespace WrightWayRestaurant.Model.Enums
{
    public enum ResultStatusEnums
    {
        [Description("很抱歉！系统内部错误，请联系管理员！")]
        Error = -100,

        [Description("操作失败！")]
        Fail = -1,

        [Description("操作成功！")]
        Success = 0,

        [Description("抱歉,你不具有当前操作的权限！")]
        PermissionDenied = 10,

        [Description("验证未通过！")]
        ValidationNotPass = 20,

        [Description("登陆超时,请重新登陆！")]
        NoSession = -200
    }
}
