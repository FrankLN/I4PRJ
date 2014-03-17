using System.Windows;

namespace GUI_first_iteration
{
    class EditUserCom
    {
        public string FirstName { set; get; }
        public string Surname { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Password { set; get; }
        public string PasswordRepeat { set; get; }

        public void Print()
        {
            MessageBox.Show("[upLink:EditUserCom]" + this.FirstName + " " + this.Surname + " " + this.Email + " " + this.Phone + " " + this.Password + " " + this.PasswordRepeat);
        }
    }
}