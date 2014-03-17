using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI_first_iteration
{
    /// <summary>
    /// Interaction logic for JobDetailsWindow.xaml
    /// </summary>
    public partial class JobDetailsWindow : Window
    {
        // -----------------------------------
        // DATA MEMBERS ----------------------
        // -----------------------------------

        private JobHistoryWindow main_parent;
        private IClientCom clientCom;

        // -----------------------------------
        // CONSTRUCTOR -----------------------
        // -----------------------------------
     
        public JobDetailsWindow(JobHistoryWindow parent, IClientCom ccom)
        {
            main_parent = parent;
            clientCom = ccom;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            main_parent.Show();
        }
    }
}
