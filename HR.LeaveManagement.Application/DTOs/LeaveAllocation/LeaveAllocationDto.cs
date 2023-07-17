using HR.LeaveManagement.Application.Dtos.Common;
using HR.LeaveManagement.Application.Dtos.LeaveType;
using HR.LeaveManagement.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Dtos.LeaveAllocation
{
    public class LeaveAllocationDto : BaseDto
    {
        public int NumberOfDays { get; set; }
        public LeaveTypeDto LeaveType { get; set; }
        public Employee Employee { get; set; }
        public string EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
