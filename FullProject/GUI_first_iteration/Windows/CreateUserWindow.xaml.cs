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
using DatabaseInterface;
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
        private UserClass userObj;
 

        public bool ClosedInCode;

        // -----------------------------------
        // CONSTRUCTOR - CreateUserWindow ----
        // -----------------------------------

        public CreateUserWindow(MainWindow mWin, IClientCmd ccom)
        {
            mainWin = mWin;
            clientCom = ccom;
            userObj= new UserClass();
            ClosedInCode = false;

            createUserObj = new CreateUserMsg();
            InitializeComponent();
            DataContext = userObj;
            // Center window at startup
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            ((ClientCmd)clientCom).onCreateUserMsgReceived += new ClientCmd.CreateUserDelegate(createUserEvent);
        }

        // -----------------------------------
        // BUTTON - Create user --------------
        // -----------------------------------

        private void btnCreateUser_Click(object sender, RoutedEventArgs e)
        {
            Color color;
            color = Color.FromArgb(255, 227, 233, 239);

            if (TbxPassword.Password != "" && TbxPasswordRepeat.Password != "" &&
                TbxPassword.Password == TbxPasswordRepeat.Password)
            {
                // Validate all controls
                if (ValidateBindings(this))
                {

                    
                    clientCom.SendToServer(createUserObj);
                }

                TbxPassword.ToolTip = null;
                TbxPasswordRepeat.ToolTip = null;
                TbxPassword.BorderBrush = new SolidColorBrush(color);
                TbxPasswordRepeat.BorderBrush = new SolidColorBrush(color);
            }
            else
            {
                TbxPassword.BorderBrush = new SolidColorBrush(Colors.Red);
                TbxPassword.BorderThickness = new Thickness(1.25);
                TbxPasswordRepeat.BorderBrush = new SolidColorBrush(Colors.Red);
                TbxPasswordRepeat.BorderThickness = new Thickness(1.25);
                ValidateBindings(this);
                TbxPassword.ToolTip = "Indtast din password";
                TbxPasswordRepeat.ToolTip = "Indtast din password igen";

            }

        }
        
        // -----------------------------------
        // Event - Activated on reply --------
        // -----------------------------------
        private void createUserEvent(ICreateUserReplyMsg msg)
        {
            if (msg.Created)
            {
                ActivateUserWindow activateUserWin = new ActivateUserWindow(createUserObj.User ,clientCom, this, mainWin);
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

            ((ClientCmd)clientCom).onCreateUserMsgReceived -= new ClientCmd.CreateUserDelegate(createUserEvent);

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
            Color color;
            color = Color.FromArgb(255, 227, 233, 239);
            if (TbxPassword.Password.Count() >7)
            {
                createUserObj.User.Password = TbxPassword.Password;
                TbxPassword.ToolTip = null;
                TbxPassword.BorderBrush = new SolidColorBrush(color);
                TbxPassword.UpdateLayout();
            }

            else
            {
                TbxPassword.BorderBrush = new SolidColorBrush(Colors.Red);
                TbxPassword.BorderThickness = new Thickness(1.25);
                TbxPassword.ToolTip = "Indtast din password";

            }

          
        }
        private void TbxPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            Color color;
            color = Color.FromArgb(255, 227, 233, 239);
            TbxPassword.BorderBrush = new SolidColorBrush(color);
          

        }

        private void TbxPasswordRepeat_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Color color;
            color = Color.FromArgb(255, 227, 233, 239);

            if (TbxPasswordRepeat.Password.Count() >7 && TbxPasswordRepeat.Password == TbxPassword.Password)
            {
                TbxPasswordRepeat.ToolTip = null;
                TbxPasswordRepeat.BorderBrush = new SolidColorBrush(color);

            }
            else
            {
                TbxPasswordRepeat.BorderBrush = new SolidColorBrush(Colors.Red);
                TbxPasswordRepeat.BorderThickness = new Thickness(1.25);
                TbxPasswordRepeat.ToolTip = "Indtast din password igen";
            }
            // remember to add repeat password attribute in CreateUserMsg
            //createUserObj.User.RepeatPassword = TbxPasswordRepeat.Password;
        }

        private void TbxPasswordRepeat_GotFocus(object sender, RoutedEventArgs e)
        {
            Color color;
            color = Color.FromArgb(255, 227, 233, 239);
            TbxPasswordRepeat.BorderBrush = new SolidColorBrush(color);

        }


        // -----------------------------------
        // METHOD - Window closing -----------
        // -----------------------------------

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ((ClientCmd)clientCom).onCreateUserMsgReceived -= new ClientCmd.CreateUserDelegate(createUserEvent);
            // Application should only shut down if window was closed manually (alt f4  or  x-button)
            if (!ClosedInCode)
            {
                //Application.Current.Shutdown();
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

    public class ValidFName : ValidationRule
    {

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (!ValidFNameRegex.IsMatch((string)value))
            {
                return new ValidationResult(false, "Indtast din fornavn");

            }

            return new ValidationResult(true, null);
        }

        private static Regex ValidFNameRegex = CreateValidFNameRegex();

        private static Regex CreateValidFNameRegex()
        {
            string validFNamePattern = @"^[\p{L}\p{M}' \.\-]+$";


            return new Regex(validFNamePattern, RegexOptions.IgnoreCase);
        }

        internal static bool FnameIsValid(string Phone)
        {
            bool isValid = ValidFNameRegex.IsMatch(Phone);

            return isValid;
        }
    }

    public class ValidSName : ValidationRule
    {

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (!ValidSNameRegex.IsMatch((string)value) )
            {
                return new ValidationResult(false, "Indtast din efternavn");

            }

            return new ValidationResult(true, null);
        }

        private static Regex ValidSNameRegex = CreateValidSNameRegex();

        private static Regex CreateValidSNameRegex()
        {
            string validSNamePattern = @"^[\p{L}\p{M}' \.\-]+$";


            return new Regex(validSNamePattern, RegexOptions.IgnoreCase);
        }

        internal static bool SnameIsValid(string Phone)
        {
            bool isValid = ValidSNameRegex.IsMatch(Phone);

            return isValid;
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
    public class ValidPhone : ValidationRule
    {

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (!ValidPhoneRegex.IsMatch((string)value))
            {
                return new ValidationResult(false, "Indtast din tlf nummer");

            }

            return new ValidationResult(true, null);
        }

        private static Regex ValidPhoneRegex = CreateValidPhoneRegex();

        private static Regex CreateValidPhoneRegex()
        {
            string validPhonePattern = "^[0-9]{8}$";


            return new Regex(validPhonePattern, RegexOptions.IgnoreCase);
        }

        internal static bool PhoneIsValid(string Phone)
        {
            bool isValid = ValidPhoneRegex.IsMatch(Phone);

            return isValid;
        }
    }

    }

