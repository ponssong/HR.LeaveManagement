using HR.LeaveManagement.Application.Dtos.Common;
using HR.LeaveManagement.Application.Dtos.LeaveType;
using HR.LeaveManagement.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Dtos.LeaveRequest
{
    public class LeaveRequestListDto: BaseDto
    {
        public Employee Employee { get; set; }
        public string RequestingEmployeeId { get; set; }
        public LeaveTypeDto LeaveType { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? Approved { get; set; }

    }
}
