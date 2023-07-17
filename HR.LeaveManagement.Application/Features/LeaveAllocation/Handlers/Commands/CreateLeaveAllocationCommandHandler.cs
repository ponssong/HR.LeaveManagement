using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Responses;
using System.Linq;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Handlers.Commands
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, BaseCommandResponse>
    {
        //private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        //private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLeaveAllocationCommandHandler(
            //ILeaveAllocationRepository leaveAllocationRepository,
            //ILeaveTypeRepository leaveTypeRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService)
        {
            //_leaveTypeRepository = leaveTypeRepository; 
            //_leaveAllocationRepository = leaveAllocationRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveAllocationDtoValidator(_unitOfWork.LeaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else 
            {

                var leaveType = await _unitOfWork.LeaveTypeRepository.Get(request.LeaveAllocationDto.LeaveTypeId);
                var employees = await _userService.GetEmployees();
                var period = DateTime.Now.Year;
                var allocations = new List<HR.LeaveManagement.Domain.LeaveAllocation>();
                foreach (var emp in employees)
                {
                    if (await _unitOfWork.LeaveAllocationRepository.AllocationExists(emp.Id, leaveType.Id, period))
                        continue;
                    allocations.Add(new HR.LeaveManagement.Domain.LeaveAllocation
                    {
                        EmployeeId = emp.Id,
                        LeaveTypeId = leaveType.Id,
                        NumberOfDays = leaveType.DefaultDays,
                        Period = period
                    });
                }

                await _unitOfWork.LeaveAllocationRepository.AddAllocations(allocations);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Creation Successful";
            }

            return response;
        }
    }
}
