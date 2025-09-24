﻿using BloggingPlatform.Application.Models;
using BloggingPlatform.Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace BloggingPlatform.Application.Validators
{
    public class CommentRequestValidator : AbstractValidator<CommentRequest>
    {
        public CommentRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(255)
                .WithMessage("Title is required and the maximum length is 255 characters.");

            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(4000)
                .WithMessage("Content is required and the maximum length is 4000 characters.");
        }
    }
}
