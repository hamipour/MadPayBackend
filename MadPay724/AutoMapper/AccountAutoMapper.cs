using AutoMapper;
using MadPay724.ViewModels.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadPay724.Presentation.AutoMapper
{
    public class AccountAutoMapper: Profile
    {
        public AccountAutoMapper()
        {
            CreateMap<SignupViewModel, MadPay724.Data.Models.User>();
        }
    }
}
