using System.Collections.Generic;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.BusinessLogic.ViewModels;

namespace LvivCompany.Bookstore.BusinessLogic.Mapper
{
    public class ProfileMapper : IMapper<User, EditProfileViewModel>
    {
        public ProfileMapper()
        {
        }

        public User Map(EditProfileViewModel model)
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

        public User Map(EditProfileViewModel model, User tempUser)
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

        public EditProfileViewModel Map(User user)
        {
            EditProfileViewModel model = new EditProfileViewModel();
            model.Address1 = user.Address1;
            model.Address2 = user.Address2;
            model.Email = user.Email;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.PhoneNumber = user.PhoneNumber;
            model.Photo = user.Photo;
            return model;
        }

        public List<EditProfileViewModel> Map(List<User> entity)
        {
            List<EditProfileViewModel> models = new List<EditProfileViewModel>();
            foreach (var item in entity)
            {
                models.Add(Map(item));
            }

            return models;
        }

        public List<User> Map(List<EditProfileViewModel> entity)
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