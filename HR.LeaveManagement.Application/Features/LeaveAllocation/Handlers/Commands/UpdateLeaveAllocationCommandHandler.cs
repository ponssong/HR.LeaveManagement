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

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Handlers.Commands
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
    {
        //private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLeaveAllocationCommandHandler(
            //ILeaveAllocationRepository leaveAllocationRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            //_leaveAllocationRepository = leaveAllocationRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveAllocationDtoValidator(_unitOfWork.LeaveAllocationRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var leaveAllocation = await _unitOfWork.LeaveAllocationRepository.Get(request.LeaveAllocationDto.Id);

            if (leaveAllocation == null)
                throw new NotFoundException(nameof(leaveAllocation), request.LeaveAllocationDto.Id);

            _mapper.Map(request.LeaveAllocationDto, leaveAllocation);

            await _unitOfWork.LeaveAllocationRepository.Update(leaveAllocation);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
