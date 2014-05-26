using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using ClientApplication;
using MessageTypes;
using MessageTypes.Messages;
using MessageTypes.ReplyMessages;

namespace GUI_first_iteration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // -----------------------------------
        // DATA MEMBERS ----------------------
        // -----------------------------------

        private MainMenuWindow mainMenuWin;
        private IClientCmd clientCom;
        private LoginMsg loginObj;
        private bool ClosedInCode;

        // -----------------------------------
        // CONSTRUCTOR - MainWindow ----------
        // -----------------------------------


        /// <summary>
        /// Constructor for CreateUserWindow. Referencer til instanserne af de pågældende parametre gemmes som private datamembers.
        /// Derudover oprettes en instans af klassen LoginMsg, der skal sendes til server. Datacontexten sættes til instansen af LoginMsg.
        /// </summary>
        /// <param name="parent">Reference til instansen af MainMenuWindow, der MainWindow oprettes fra.</param>
        /// <param name="ccom">Reference til instansen af klassen ClienCmd, der står for kommunikation til serveren.</param>
        public MainWindow(MainMenuWindow parent, IClientCmd ccom)
        {
            mainMenuWin = parent;
            clientCom = ccom;
            loginObj = new LoginMsg();
            ClosedInCode = false;
            InitializeComponent();         
            DataContext = loginObj;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            ((ClientCmd)clientCom).onLogiMsgReceived += new ClientCmd.LogiDelegate(loginEvent);
        }

        // -----------------------------------
        // BUTTON - New user -----------------
        // -----------------------------------

        /// <summary>
        /// Funktion der kaldes ved tryk på knap for at oprette CreateUserWindow.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateUserWindow createUserWin = new CreateUserWindow(this, clientCom);

            createUserWin.Show(); // ShowDialog
            ClosedInCode = true;
            this.Hide();
        }

        // -----------------------------------
        // BUTTON - Login --------------------
        // -----------------------------------

        /// <summary>
        /// Funktion der kaldes ved tryk på knap for at Log in. Det som er indtastet af Bruger bliver sendt til server.
        /// Der er validering på felterne, således at der ikke sendes besked til server hvis indtastet data ikke er valide.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Send object
            //var clientCmd = new ClientCmd();
            //clientCmd = (ClientCmd) clientCom;
            Color color;
            color = Color.FromArgb(255, 227, 233, 239);

            if (tbxPassword.Password.Count()>5)

            {
                // Validate all controls
                if (ValidateBindings(this))
                {
                    clientCom.SendToServer(loginObj);
                }
                tbxPassword.ToolTip = null;
                tbxPassword.BorderBrush = new SolidColorBrush(color);
            }
            else
            {
                tbxPassword.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxPassword.BorderThickness = new Thickness(1.25);
                ValidateBindings(this);
                tbxPassword.ToolTip = "Indtast din Password";
            }

        }


        /// <summary>
        /// Event der kaldes når serveren svarer på GUIs request om at Bruger er logged in. 
        /// MainMenuWindow vises hvis Bruger er logged in.
        /// </summary>
        /// <param name="msg">Besked modtaget fra serveren</param>
        public void loginEvent(ILoginReplyMsg msg)
        {
            if (msg.Email)
            {
                if (msg.Password)
                {
                    if (msg.Activated)
                    {
                        mainMenuWin.loggedInUser = msg.User;
                        mainMenuWin.Show();
                        ClosedInCode = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("User not activated\n Please activate now...");
                        //Make Activation window show here
                        ActivateUserWindow activateUserWin = new ActivateUserWindow(msg.User, clientCom, new CreateUserWindow(this, clientCom), this);
                        activateUserWin.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Wrong password\n Please reenter...");
                }
            }
            else
            {
                MessageBox.Show("Wrong email\n Please reenter...");
            }
            

        }


        /// <summary>
        /// Funktion der generer et event ved FocusedChange. Data lægges i objektet.
        /// Datavalidering på feltet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxPassword_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           Color color;
            color = Color.FromArgb(255, 227, 233, 239);
            if (tbxPassword.Password.Count()<6)
            {
                tbxPassword.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxPassword.BorderThickness = new Thickness(1.25);
                tbxPassword.ToolTip = "Indtast din password";

            }
            else
            {
                loginObj.Password = tbxPassword.Password;
                tbxPassword.ToolTip = null;
                tbxPassword.BorderBrush = new SolidColorBrush(color);
            }
            


        }
        /// <summary>
        /// Funktion der generer et event ved MouseButtonLeft down.
        /// Window kan flyttes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        /// <summary>
        /// Funktion der kaldes når vinduet lukkes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ClosedInCode)
            {
                ((ClientCmd)clientCom).onLogiMsgReceived -= new ClientCmd.LogiDelegate(loginEvent);
                Application.Current.Shutdown();
            }

        }

        private void LbExit_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!ClosedInCode)
            {
                Application.Current.Shutdown();
            }

        }
        /// <summary>
        /// Funktion der generer et event ved MouseButtonLeft down
        /// Window minimeres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LbMin_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Funktionen der kaldes ved validering af Textbox of Passwordbox.
        /// Funktionen er taget fra GUI undervisning. 
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static bool ValidateBindings(DependencyObject parent)
        {
            // Validate all the bindings on the parent
            bool valid = true;
            LocalValueEnumerator localValues = parent.GetLocalValueEnumerator();
            while (localValues.MoveNext())
            {
                LocalValueEntry entry = localValues.Current;
                if (BindingOperations.IsDataBound(parent, entry.Property))
                {
                    Binding binding = BindingOperations.GetBinding(parent, entry.Property);
                    foreach (ValidationRule rule in binding.ValidationRules)
                    {
                        // TODO: where to get correct culture info?
                        ValidationResult result = rule.Validate(parent.GetValue(entry.Property), null);
                        if (!result.IsValid)
                        {
                            BindingExpression expression = BindingOperations.GetBindingExpression(parent, entry.Property);
                            Validation.MarkInvalid(expression,
                                new ValidationError(rule, expression, result.ErrorContent, null));
                            valid = false;
                        }
                    }
                }
            }

            // Validate all the bindings on the children
            for (int i = 0; i != VisualTreeHelper.GetChildrenCount(parent); ++i)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (!ValidateBindings(child))
                {
                    valid = false;
                }
            }

            return valid;
        }

        /// <summary>
        /// Funktion der generer et event ved FocusedChange.Datavalidering på feltet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            Color color;
            color = Color.FromArgb(255, 227, 233, 239);
         
            tbxPassword.BorderBrush = new SolidColorBrush(color);
        }
    }
}

