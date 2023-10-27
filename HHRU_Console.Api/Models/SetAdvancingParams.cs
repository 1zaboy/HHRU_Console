using FluentValidation;
using System;

namespace HHRU_Console.Api.Models;

public class SetAdvancingParams
{
    public string ResumeId { get; set; }
    public bool IsAdvancing { get; set; }
}

public class SetAdvancingParamsValidator : AbstractValidator<SetAdvancingParams>
{
    public SetAdvancingParamsValidator()
    {
        RuleFor(x => x.ResumeId).NotEmpty().WithMessage($"{nameof(SetAdvancingParams.ResumeId)} cannot be empty null");
    }
}
