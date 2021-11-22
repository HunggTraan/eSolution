using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.ViewModels.System.Users
{
    public class AddUserValidator : AbstractValidator<CreateUserRequest>
    {
        public AddUserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Không được để trống tên đăng nhập");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Không được để trống tên mật khẩu")
                .MinimumLength(6).WithMessage("Mật khẩu phải dài ít nhất 6 ký tự.")
                .MaximumLength(30).WithMessage("Mật khẩu chỉ được tối đa 30 ký tự");
            RuleFor(x => x.DepartmentId).NotEmpty().WithMessage("Không được để trống phòng ban");
            RuleFor(x => x.JobTitleId).NotEmpty().WithMessage("Không được để trống chức vụ");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Không được để họ và tên")
                .MaximumLength(200).WithMessage("Họ và tên chỉ được tối đa 200 ký tự");
            RuleFor(x => x.Dob).GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Ngày sinh không cách quá 100 năm.");
            RuleFor(x => x.UserEmail).Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
                .WithMessage("Thư điện tử không đúng định dạng");

            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.Password != request.ConfirmPassword)
                {
                    context.AddFailure("Mật khẩu nhập lại không khớp");
                }
            });
        }
    }
}
