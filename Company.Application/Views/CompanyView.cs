using System.Collections.Generic;
using CompanySystem.Domain.Entities;

namespace CompanySystem.Application.Views
{
    public class CompanyView
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}