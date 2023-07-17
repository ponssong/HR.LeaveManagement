using FluentValidation;
using HR.LeaveManagement.Application.Dtos.LeaveAllocation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators
{
    public class CreateLeaveAllocationDtoValidator : AbstractValidator<CreateLeaveAllocationDto>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationbRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveAllocationDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            //Include(new ILeaveAllocationDtoValidator(_leaveAllocationbRepository));
            _leaveTypeRepository = leaveTypeRepository;

            RuleFor(p => p.LeaveTypeId)
                .GreaterThanOrEqualTo(0)
                .MustAsync(async (id, token) =>
                {
                    var leaveTypesExists = await _leaveTypeRepository.Exists(id);
                    return leaveTypesExists;

                })
                .WithMessage("{PropertyName} does not exist");
        }
    }
}
