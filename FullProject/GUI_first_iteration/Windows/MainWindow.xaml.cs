using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClientApplication;
using ConsoleApplication1;
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

        public MainWindow(MainMenuWindow parent, IClientCmd ccom)
        {
            mainMenuWin = parent;
            clientCom = ccom;
            loginObj = new LoginMsg();
            ClosedInCode = false;
            InitializeComponent();
            DataContext = loginObj;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // -----------------------------------
        // BUTTON - New user -----------------
        // -----------------------------------

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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Send object
            var clientCmd = new ClientCmd();
            clientCmd = (ClientCmd) clientCom;
            if (tbxPassword.Password != "")

            {
                // Validate all controls
                if (ValidateBindings(this))
                {
                    clientCmd.onLogiMsgReceived += new ClientCmd.LogiDelegate(loginEvent);
                    clientCom.SendToServer(loginObj);
                }
                tbxPassword.ToolTip = null;
                tbxPassword.BorderBrush = new SolidColorBrush(Colors.Tan);
            }
            else
            {
                tbxPassword.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxPassword.BorderThickness = new Thickness(1.25);
                ValidateBindings(this);
                tbxPassword.ToolTip = "Indtast din Password";
            }

        }

        public void loginEvent(ILoginReplyReplyMsg msg)
        {

            if (msg.Email)
            {
                if (msg.Password)
                {
                    mainMenuWin.loggedInUser = msg.User;
                    mainMenuWin.Show();
                    ClosedInCode = true;
                    this.Close();
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

/*
        private void tbxPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            tbxOnPSW.Visibility = Visibility.Hidden;
        }

        private void tbxEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            tbxEmail.Text = string.Empty;
        }
*/

        private void tbxEmail_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {



            loginObj.Email = tbxEmail.Text;

        }

        private void tbxPassword_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (tbxPassword.Password == "")
            {
                tbxPassword.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxPassword.BorderThickness = new Thickness(1.25);
                tbxPassword.ToolTip = "Indtast din password";

            }
            else
            {
                loginObj.Password = tbxPassword.Password;
                tbxPassword.ToolTip = null;
                tbxPassword.BorderBrush = new SolidColorBrush(Colors.Tan);
            }



        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ClosedInCode)
            {
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

        private void LbMin_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }


        // This is here 'til future versions of WPF provide this functionality
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
    }

    public class ValidEmail : ValidationRule
        {

            public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
            {
                if (!ValidEmailRegex.IsMatch((string)value))
                {
                    return new ValidationResult(false, " Indtast en rigtig email");

                }

                return new ValidationResult(true, null);
            }

            private static Regex ValidEmailRegex = CreateValidEmailRegex();

            private static Regex CreateValidEmailRegex()
            {
                string validEmailPattern = "[a-zA-Z0-9]" + "@iha.dk$";

                return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
            }

            internal static bool EmailIsValid(string emailAddress)
            {
                bool isValid = ValidEmailRegex.IsMatch(emailAddress);

                return isValid;
            }
        }
    }

