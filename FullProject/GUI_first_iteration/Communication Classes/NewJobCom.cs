using System.Windows;

namespace GUI_first_iteration
{
    class NewJobCom
    {
        public string Material { set; get; }
        public string HolSol { set; get; }
        public string Date { set; get; }
        public string Comments { set; get; }

        public void Print()
        {
            MessageBox.Show("[upLink:NewJobCom]" + this.Material + " " + this.HolSol + " " + this.Date + " " + this.Comments + " ");
        }
    }
}