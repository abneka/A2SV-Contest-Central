using Application.Contracts.Persistence;
using Application.DTOs.UserType;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Application.Features.UserType.Commands.CreateUserType;

public class CreateUserTypeCommandValidator : AbstractValidator<CreateUserTypeCommand>
{
    public CreateUserTypeCommandValidator()
    {
        
    }
}