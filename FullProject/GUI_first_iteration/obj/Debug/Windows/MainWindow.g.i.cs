﻿#pragma checksum "..\..\..\Windows\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5B993B7798B9096FFA682D2934EDE7B4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34011
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace GUI_first_iteration {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LbExit;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LbMin;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LogoLabel;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox tbxPassword;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbxEmail;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbxOnPSW;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLogin;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNewUser;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/GUI_first_iteration;component/windows/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 4 "..\..\..\Windows\MainWindow.xaml"
            ((GUI_first_iteration.MainWindow)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseDown);
            
            #line default
            #line hidden
            
            #line 4 "..\..\..\Windows\MainWindow.xaml"
            ((GUI_first_iteration.MainWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.LbExit = ((System.Windows.Controls.Label)(target));
            
            #line 14 "..\..\..\Windows\MainWindow.xaml"
            this.LbExit.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.LbExit_OnMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.LbMin = ((System.Windows.Controls.Label)(target));
            
            #line 15 "..\..\..\Windows\MainWindow.xaml"
            this.LbMin.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.LbMin_OnMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.LogoLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.tbxPassword = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 36 "..\..\..\Windows\MainWindow.xaml"
            this.tbxPassword.GotFocus += new System.Windows.RoutedEventHandler(this.tbxPassword_GotFocus);
            
            #line default
            #line hidden
            
            #line 36 "..\..\..\Windows\MainWindow.xaml"
            this.tbxPassword.IsKeyboardFocusedChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.tbxPassword_IsKeyboardFocusedChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.tbxEmail = ((System.Windows.Controls.TextBox)(target));
            
            #line 37 "..\..\..\Windows\MainWindow.xaml"
            this.tbxEmail.GotFocus += new System.Windows.RoutedEventHandler(this.tbxEmail_GotFocus);
            
            #line default
            #line hidden
            
            #line 37 "..\..\..\Windows\MainWindow.xaml"
            this.tbxEmail.IsKeyboardFocusedChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.tbxEmail_IsKeyboardFocusedChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.tbxOnPSW = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.btnLogin = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\Windows\MainWindow.xaml"
            this.btnLogin.Click += new System.Windows.RoutedEventHandler(this.btnLogin_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnNewUser = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\..\Windows\MainWindow.xaml"
            this.btnNewUser.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

