﻿using HR.LeaveManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Domain
{
    public class LeaveAllocation : BaseDomainEntity
    {
        //[Key]
        //public int Id { get; set; }
        public int NumberOfDays { get; set; }
        //public DateTime DateCreated { get; set; }
        //[ForeignKey("EmployeeId")]
        //public Employee Employee { get; set; }
        public string EmployeeId { get; set; }
        //[ForeignKey("LeaveTypeId")]
        public LeaveType LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
