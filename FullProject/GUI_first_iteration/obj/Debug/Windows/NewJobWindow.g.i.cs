﻿#pragma checksum "..\..\..\Windows\NewJobWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1FDEE6B9A50B51F66897332F1CC62317"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
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
    /// NewJobWindow
    /// </summary>
    public partial class NewJobWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\Windows\NewJobWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbxMaterial;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Windows\NewJobWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbxHolSol;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Windows\NewJobWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dpDate;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Windows\NewJobWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbxComments;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Windows\NewJobWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbxFilePath;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\Windows\NewJobWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBrowse;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\Windows\NewJobWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCreateJob;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\Windows\NewJobWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBackToMain;
        
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
            System.Uri resourceLocater = new System.Uri("/GUI_first_iteration;component/windows/newjobwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\NewJobWindow.xaml"
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
            
            #line 5 "..\..\..\Windows\NewJobWindow.xaml"
            ((GUI_first_iteration.NewJobWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.cbxMaterial = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.cbxHolSol = ((System.Windows.Controls.ComboBox)(target));
            
            #line 30 "..\..\..\Windows\NewJobWindow.xaml"
            this.cbxHolSol.IsKeyboardFocusedChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.cbxHolSol_IsKeyboardFocusedChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.dpDate = ((System.Windows.Controls.DatePicker)(target));
            
            #line 34 "..\..\..\Windows\NewJobWindow.xaml"
            this.dpDate.IsKeyboardFocusedChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.dpDate_IsKeyboardFocusedChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.tbxComments = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.tbxFilePath = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.btnBrowse = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\..\Windows\NewJobWindow.xaml"
            this.btnBrowse.Click += new System.Windows.RoutedEventHandler(this.btnBrowse_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnCreateJob = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\..\Windows\NewJobWindow.xaml"
            this.btnCreateJob.Click += new System.Windows.RoutedEventHandler(this.btnCreateJob_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnBackToMain = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\..\Windows\NewJobWindow.xaml"
            this.btnBackToMain.Click += new System.Windows.RoutedEventHandler(this.btnBackToMain_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

