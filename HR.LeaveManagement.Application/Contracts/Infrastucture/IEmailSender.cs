﻿using HR.LeaveManagement.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Infrastucture
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(Email email);
    }
}
