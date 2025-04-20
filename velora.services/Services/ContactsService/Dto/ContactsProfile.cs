using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using velora.core.Entites;

namespace velora.services.Services.ContactsService.Dto
{
    public class ContactsProfile :Profile
    {
        public ContactsProfile()
        {
            CreateMap<ContactsDto, Contacts>();
        }
    }
}
