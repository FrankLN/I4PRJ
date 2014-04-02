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
using System.Windows.Shapes;
using ClientApplication;
using ConsoleApplication1;
using MessageTypes.Messages;
using MessageTypes.ReplyMessages;

namespace GUI_first_iteration
{
    /// <summary>
    /// Interaction logic for CreateUserWindow.xaml
    /// </summary>
    public partial class CreateUserWindow : Window
    {
        // -----------------------------------
        // DATA MEMBERS ----------------------  
        // -----------------------------------

        private MainWindow mainWin;
        private IClientCmd clientCom;
        private CreateUserMsg createUserObj;

        public bool ClosedInCode;

        // -----------------------------------
        // CONSTRUCTOR - CreateUserWindow ----
        // -----------------------------------

        public CreateUserWindow(MainWindow mWin, IClientCmd ccom)
        {
            mainWin = mWin;
            clientCom = ccom;
            ClosedInCode = false;

            createUserObj = new CreateUserMsg();
            InitializeComponent();

            // Center window at startup
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // -----------------------------------
        // BUTTON - Create user --------------
        // -----------------------------------

        private void btnCreateUser_Click(object sender, RoutedEventArgs e)
        {
            var clientCmd = new ClientCmd();
            clientCmd = (ClientCmd)clientCom;
            clientCmd.onCreateUserMsgReceived += new ClientCmd.CreateUserDelegate(createUserEvent);
            clientCom.SendToServer(createUserObj);
            //ActivateUserWindow activateUserWin = new ActivateUserWindow(this);
            //activateUserWin.Show();
        }
        
        // -----------------------------------
        // Event - Activated on reply --------
        // -----------------------------------
        private void createUserEvent(ICreateUserReplyMsg msg)
        {
            if (msg.Created)
            {
                
                ActivateUserWindow activateUserWin = new ActivateUserWindow(msg, this, mainWin);
                activateUserWin.Show();
            }
            else
            {
                MessageBox.Show("Please try again");
            }
        }

        // -----------------------------------
        // BUTTON - Back ---------------------
        // -----------------------------------

        private void btnBack_Click_1(object sender, RoutedEventArgs e)
        {
            mainWin.Show();

            // Indicate that the window is closed in code
            ClosedInCode = true;
            this.Close();
        }

        // ----------------------------------------------------------------------------
        // Methods for updating class with content of textbox, upon leaving the textbox
        // ----------------------------------------------------------------------------

        private void TbxName_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            createUserObj.User.FirstName = TbxName.Text;
        }

        private void TbxSurname_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            createUserObj.User.LastName = TbxSurname.Text;
        }

        private void TbxEmail_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            createUserObj.User.Email = TbxEmail.Text;
        }

        private void TbxPhone_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            createUserObj.User.PhoneNumber = TbxPhone.Text;
        }

        private void TbxPassword_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            createUserObj.User.Password = TbxPassword.Password;
        }

        private void TbxPasswordRepeat_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // remember to add repeat password attribute in CreateUserMsg
            //createUserObj.User.RepeatPassword = TbxPasswordRepeat.Password;
        }

        // -----------------------------------
        // METHOD - Window closing -----------
        // -----------------------------------

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Application should only shut down if window was closed manually (alt f4  or  x-button)
            if (!ClosedInCode)
            {
                Application.Current.Shutdown();
            }
            
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

