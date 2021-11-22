using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.ViewModels.System.Users
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Không được để trống tên đăng nhập");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Không được để trống tên mật khẩu")
                .MinimumLength(6).WithMessage("Mật khẩu phải dài ít nhất 6 ký tự.");
        }
    }
}
