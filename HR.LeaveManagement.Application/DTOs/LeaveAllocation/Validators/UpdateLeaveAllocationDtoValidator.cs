using FluentValidation;
using HR.LeaveManagement.Application.Dtos.LeaveAllocation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators
{
    public class UpdateLeaveAllocationDtoValidator : AbstractValidator<UpdateLeaveAllocationDto>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationbRepository;

        public UpdateLeaveAllocationDtoValidator(ILeaveAllocationRepository leaveAllocationbRepository)
        {
            _leaveAllocationbRepository = leaveAllocationbRepository;
            Include(new ILeaveAllocationDtoValidator(_leaveAllocationbRepository));

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
