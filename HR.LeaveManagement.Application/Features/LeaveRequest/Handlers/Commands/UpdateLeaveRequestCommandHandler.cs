using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequest.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Handlers.Commands
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        //private readonly ILeaveRequestRepository _leaveRequestRepository;
        //private readonly ILeaveTypeRepository _leaveTypeRepository;
        //private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLeaveRequestCommandHandler(
            //ILeaveRequestRepository leaveRequestRepository,
            // ILeaveTypeRepository leaveTypeRepository,
            // ILeaveAllocationRepository leaveAllocationRepository,
            IUnitOfWork unitOfWork,
             IMapper mapper)
        {
            //_leaveRequestRepository = leaveRequestRepository;
            //_leaveTypeRepository = leaveTypeRepository;
            //this._leaveAllocationRepository = leaveAllocationRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _unitOfWork.LeaveRequestRepository.Get(request.Id);

            if (leaveRequest == null)
                throw new NotFoundException(nameof(leaveRequest), request.LeaveRequestDto.Id);

            if (request.LeaveRequestDto != null)
            {
                var validator = new UpdateLeaveRequestDtoValidator(_unitOfWork.LeaveTypeRepository);
                var validationResult = await validator.ValidateAsync(request.LeaveRequestDto);
                if (validationResult.IsValid == false)
                    throw new ValidationException(validationResult);
                _mapper.Map(request.LeaveRequestDto, leaveRequest);

                await _unitOfWork.LeaveRequestRepository.Update(leaveRequest);
                await _unitOfWork.Save();
            }
            else if (request.ChangeLeaveRequestApprovalDto != null)
            {
                await _unitOfWork.LeaveRequestRepository.ChangeApprovalStatus(leaveRequest, request.ChangeLeaveRequestApprovalDto.Approved);
                if (request.ChangeLeaveRequestApprovalDto.Approved)
                {
                    var allocation = await _unitOfWork.LeaveAllocationRepository.GetUserAllocations(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
                    int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;

                    allocation.NumberOfDays -= daysRequested;

                    await _unitOfWork.LeaveAllocationRepository.Update(allocation);                    
                }
                await _unitOfWork.Save();
            }

            return Unit.Value;
        }
    }
}
