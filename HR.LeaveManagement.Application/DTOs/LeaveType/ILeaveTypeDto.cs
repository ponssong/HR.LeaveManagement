﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTOs.LeaveType
{
    public interface ILeaveTypeDto
    {
        string Name { get; set; }
        int DefaultDays { get; set; }
    }
}
