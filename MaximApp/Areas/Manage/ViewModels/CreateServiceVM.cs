﻿using FluentValidation;

namespace MaximApp.Areas.Manage.ViewModels
{
    public class CreateServiceVM
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
    }
    public class CreateServiceValidator : AbstractValidator<CreateServiceVM>
    {
        public CreateServiceValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title bos ola bilmez");
            RuleFor(x => x.Title).MinimumLength(3).WithMessage("Title 3-den kicik ola bilmez");
            RuleFor(x => x.Title).MaximumLength(30).WithMessage("Title 30-dan boyuk ola bilmez");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Description bos ola bilmez");
            RuleFor(x => x.Description).MinimumLength(3).WithMessage("Description 3-den kicik ola bilmez");
            RuleFor(x => x.Description).MaximumLength(100).WithMessage("Description 30-dan boyuk ola bilmez");

            RuleFor(x => x.Icon).NotEmpty().WithMessage("Icon bos ola bilmez");


        }
    }
}
