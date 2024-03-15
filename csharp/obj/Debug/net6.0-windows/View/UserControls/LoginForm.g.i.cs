﻿#pragma checksum "..\..\..\..\..\View\UserControls\LoginForm.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "BEC122E275EFBDFE1E5EFC15445C3AF3594FD3AC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MagicQuizDesktop.CustomControls;
using MagicQuizDesktop.View.UserControls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace MagicQuizDesktop.View.UserControls {
    
    
    /// <summary>
    /// LoginForm
    /// </summary>
    public partial class LoginForm : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 2 "..\..\..\..\..\View\UserControls\LoginForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MagicQuizDesktop.View.UserControls.LoginForm loginForm;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\..\View\UserControls\LoginForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock UserNameTitle;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\..\..\View\UserControls\LoginForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EmailData;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\..\..\View\UserControls\LoginForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock PasswordTitle;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\..\..\View\UserControls\LoginForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordData;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\..\..\View\UserControls\LoginForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ForgetPassword;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\..\..\View\UserControls\LoginForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock User;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\..\..\View\UserControls\LoginForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Admin;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.2.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MagicQuizDesktop;component/view/usercontrols/loginform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\UserControls\LoginForm.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.2.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.loginForm = ((MagicQuizDesktop.View.UserControls.LoginForm)(target));
            return;
            case 2:
            this.UserNameTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.EmailData = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.PasswordTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.PasswordData = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 76 "..\..\..\..\..\View\UserControls\LoginForm.xaml"
            this.PasswordData.PasswordChanged += new System.Windows.RoutedEventHandler(this.PasswordBox_PasswordChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ForgetPassword = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.User = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.Admin = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

