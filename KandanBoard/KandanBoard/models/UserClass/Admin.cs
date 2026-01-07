using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KandanBoard.models.UserClass
{
    internal class Admin : User
    {
        public Admin(int userId, string firstName, string lastName, string email, string password) : base(userId, firstName, lastName, email, password)
        {
        }

        public override bool CanDeleteTask()
        {
            return true;
        }
    }
}
