using Application.Contracts.Persistence;
using Application.DTOs.ContestGroup;
using AutoMapper;
using MediatR;

namespace Application.Features.ContestGroup.Create;

public class ContestGroupCreateCommand : IRequest<ContestGroupResponseDto>
{ 
    public ContestGroupRequestDto ContestGroupRequestDto { get; set; } = null!;
}