using HR.LeaveManagement.Application.Dtos.Common;
using HR.LeaveManagement.Application.Dtos.LeaveType;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Dtos.LeaveRequest
{
    public class UpdateLeaveRequestDto : BaseDto, ILeaveRequestDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int LeaveTypeId { get; set; }
        public string RequestComments { get; set; }
        public bool Cancelled { get; set; }
    }
}
