using System.Windows;

namespace GUI_first_iteration
{
    class JobHistoryCom
    {
        public string Email { set; get; }

        public void Print()
        {
            MessageBox.Show("[upLink:JobHistoryCom]" + this.Email);
        }
    }
}