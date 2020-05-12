using System;
using System.Collections.Generic;
using System.Text;

namespace BOTAuthentication.Enums
{

    public enum UserStatusEnum
    {
        Approve = 1,
        WaitingForApproval,
        Block,
        NoPermission
    }
}
