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
using ClientApplication;
using DatabaseInterface;
using MessageTypes;
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


        /// <summary>
        /// Constructor for CreateUserWindow. Referencer til instanserne af de pågældende parametre gemmes som private datamembers. 
        /// Derudover oprettes en instans af klassen CreateUserMsg, der skal sendes til server. Datacontexten sættes til instansen af UserClass.
        /// </summary>
        /// <param name="mWin">Reference til instansen af MainWindow, der CreateUserWindow oprettes fra</param>
        /// <param name="ccom">Reference til instansen af klassen ClienCmd, der står for kommunikation til serveren</param>
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

        /// <summary>
        /// Funktion der kaldes ved tryk på knap for at oprette en Bruger. Det som er indtastet af Bruger bliver sendt til server.
        /// Der er validering på felterne, således at der ikke sendes besked til server hvis indtastet data ikke er valide.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Event der kaldes når serveren svarer på GUIs request om at Bruger er oprettet. 
        /// ActivateUserWindow vises hvis Bruger er oprettet.
        /// </summary>
        /// <param name="msg">Besked modtaget fra serveren</param>
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

        /// <summary>
        /// Funktion der kaldes ved tryk på knap for at gå tilbage til MainWindow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

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

        /// <summary>
        /// Funktion der generer et event ved FocusedChange. Data lægges i objektet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbxName_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            createUserObj.User.FirstName = TbxName.Text;
        }

        /// <summary>
        /// Funktion der generer et event ved FocusedChange. Data lægges i objektet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbxSurname_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            createUserObj.User.LastName = TbxSurname.Text;
        }
        /// <summary>
        /// Funktion der generer et event ved FocusedChange. Data lægges i objektet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbxEmail_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            createUserObj.User.Email = TbxEmail.Text;
        }

        /// <summary>
        /// Funktion der generer et event ved FocusedChange. Data lægges i objektet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbxPhone_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            createUserObj.User.PhoneNumber = TbxPhone.Text;
        }

        /// <summary>
        /// Funktion der generer et event ved FocusedChange.Data lægges i objektet ved valid indtastning af password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbxPassword_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Color color;
            color = Color.FromArgb(255, 227, 233, 239);
            if (TbxPassword.Password.Count() >5)
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

        /// <summary>
        /// Funktion der generer et event ved GotFocus.Datavalidering af feltet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbxPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            Color color;
            color = Color.FromArgb(255, 227, 233, 239);
            TbxPassword.BorderBrush = new SolidColorBrush(color);
          

        }

        /// <summary>
        /// Funktion der generer et event ved FocusedChange. Datavalidering af objektet 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbxPasswordRepeat_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Color color;
            color = Color.FromArgb(255, 227, 233, 239);

            if (TbxPasswordRepeat.Password.Count() >5 && TbxPasswordRepeat.Password == TbxPassword.Password)
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
        }
        /// <summary>
        /// Funktion der generer et event ved GotFocus.Datavalidering af feltet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbxPasswordRepeat_GotFocus(object sender, RoutedEventArgs e)
        {
            Color color;
            color = Color.FromArgb(255, 227, 233, 239);
            TbxPasswordRepeat.BorderBrush = new SolidColorBrush(color);

        }


        // -----------------------------------
        // METHOD - Window closing -----------
        // -----------------------------------

        /// <summary>
        /// Funktion der kaldes når vinduet lukkes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ((ClientCmd)clientCom).onCreateUserMsgReceived -= new ClientCmd.CreateUserDelegate(createUserEvent);
            
        }

        // -----------------------------------
        // METHODS - Datavalidation -----------
        // -----------------------------------

        /// <summary>
        /// Funktionen der kaldes ved validering af Textboxes of Passwordboxes.
        /// Funktionen er taget fra GUI undervisning. 
        /// </summary>
        /// <param name="parent"></param>
        /// <returns>True hvis data er valid/False hvis data er ikke valid</returns>
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

    /// <summary>
    /// Funktion der Håndterer validerings resultat
    /// </summary>
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

        /// <summary>
        /// Hjælpefunktion for at sammenlige indtastet data med valid format
        /// </summary>
        private static Regex ValidFNameRegex = CreateValidFNameRegex();


        /// <summary>
        /// Funktion der opretter valid format  
        /// </summary>
        /// <returns>Kun bogstaver</returns>
        private static Regex CreateValidFNameRegex()
        {
            string validFNamePattern = @"^[\p{L}\p{M}' \.\-]+$";


            return new Regex(validFNamePattern, RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// Funktion der tjekker om indtastet data er valid
        /// </summary>
        /// <param name="FName">Firstname der skal valideres</param>
        /// <returns>True hvis valid/False hvis ikke valid</returns>
        internal static bool FnameIsValid(string FName)
        {
            bool isValid = ValidFNameRegex.IsMatch(FName);

            return isValid;
        }
    }
    /// <summary>
    /// Funktion der Håndterer validerings resultat
    /// </summary>
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


        /// <summary>
        /// Hjælpefunktion for at sammenlige indtastet data med valid format
        /// </summary>
        private static Regex ValidSNameRegex = CreateValidSNameRegex();


        /// <summary>
        /// Funktion der opretter valid format
        /// </summary>
        /// <returns>Kun bogstaver</returns>
        private static Regex CreateValidSNameRegex()
        {
            string validSNamePattern = @"^[\p{L}\p{M}' \.\-]+$";


            return new Regex(validSNamePattern, RegexOptions.IgnoreCase);
        }


        /// <summary>
        ///  Funktion der tjekker om indtastet data er valid
        /// </summary>
        /// <param name="Phone">Surname der skal valideres</param>
        /// <returns>True hvis valid/False hvis ikke valid</returns>
        internal static bool SnameIsValid(string Phone)
        {
            bool isValid = ValidSNameRegex.IsMatch(Phone);

            return isValid;
        }
    }

    /// <summary>
    /// Funktion der Håndterer validerings resultat
    /// </summary>
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

    /// <summary>
    /// Hjælpefunktion for at sammenlige indtastet data med valid format
    /// </summary>
    private static Regex ValidEmailRegex = CreateValidEmailRegex();


    /// <summary>
    /// Funktion der opretter valid format
    /// </summary>
    /// <returns>Store og små bogstaver samt Iha.dk domæne</returns>
    private static Regex CreateValidEmailRegex()
                     {
                        string validEmailPattern = "[a-zA-Z0-9]" + "@iha.dk$";

                        return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
                    }           
    /// <summary>
    /// Funktion der tjekker om indtastet data er valid
   /// </summary>
   /// <param name="emailAddress">Email der skal valideres</param>
   /// <returns>True hvis valid/False hvis ikke valid</returns>
   internal static bool EmailIsValid(string emailAddress)
                {
                    bool isValid = ValidEmailRegex.IsMatch(emailAddress);

                    return isValid;
                }
            }
    /// <summary>
    /// Funktion der Håndterer validerings resultat
    /// </summary>
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

        /// <summary>
        /// Hjælpefunktion for at sammenlige indtastet data med valid format
        /// </summary>
        private static Regex ValidPhoneRegex = CreateValidPhoneRegex();


        /// <summary>
        /// Funktion der opretter valid format
        /// </summary>
        /// <returns>Kun 8 cifre</returns>
        private static Regex CreateValidPhoneRegex()
        {
            string validPhonePattern = "^[0-9]{8}$";


            return new Regex(validPhonePattern, RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// Funktion der tjekker om indtastet data er valid
        /// </summary>
        /// <param name="Phone">Phone number der skal valideres</param>
        /// <returns>True hvis valid/False hvis ikke valid</returns>
        internal static bool PhoneIsValid(string Phone)
        {
            bool isValid = ValidPhoneRegex.IsMatch(Phone);

            return isValid;
        }
    }

    }

