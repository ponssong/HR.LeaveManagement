using HR.LeaveManagement.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Dtos.LeaveRequest
{
    public class ChangeLeaveRequestApprovalDto: BaseDto
    {
        public bool Approved { get; set; }
    }
}
