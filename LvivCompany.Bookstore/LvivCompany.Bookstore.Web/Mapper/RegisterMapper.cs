using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Web.ViewModels;

namespace LvivCompany.Bookstore.Web.Mapper
{
    public class RegisterMapper : IMapper<User, RegisterViewModel>
    {
        public RegisterMapper()
        {
        }

        public User Map(RegisterViewModel model)
        {
            User tempUser = new User();
            tempUser.FirstName = model.FirstName;
            tempUser.LastName = model.LastName;
            tempUser.Address1 = model.Address1;
            tempUser.Address2 = model.Address2;
            tempUser.PhoneNumber = model.PhoneNumber;
            tempUser.Email = model.Email;
            tempUser.UserName = model.Email;
            return tempUser;
        }

        public User Map(RegisterViewModel model, User tempUser)
        {
            tempUser.FirstName = model.FirstName;
            tempUser.LastName = model.LastName;
            tempUser.Address1 = model.Address1;
            tempUser.Address2 = model.Address2;
            tempUser.PhoneNumber = model.PhoneNumber;
            tempUser.Email = model.Email;
            tempUser.UserName = model.Email;
            return tempUser;
        }

        public RegisterViewModel Map(User user)
        {
            RegisterViewModel model = new RegisterViewModel();
            model.Address1 = user.Address1;
            model.Address2 = user.Address2;
            model.Email = user.Email;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.PhoneNumber = user.PhoneNumber;
            return model;
        }

        public List<RegisterViewModel> Map(List<User> entity)
        {
            List<RegisterViewModel> models = new List<RegisterViewModel>();
            foreach (var item in entity)
            {
                models.Add(Map(item));
            }

            return models;
        }

        public List<User> Map(List<RegisterViewModel> entity)
        {
            List<User> models = new List<User>();
            foreach (var item in entity)
            {
                models.Add(Map(item));
            }

            return models;
        }
    }
}
