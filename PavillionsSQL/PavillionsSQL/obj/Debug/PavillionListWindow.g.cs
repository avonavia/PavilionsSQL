﻿#pragma checksum "..\..\PavillionListWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "66934E60C4D6F74460574A7297DD92B1532840636AEE200EAAB2D1C6DE08BDD0"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using PavillionsSQL;
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


namespace PavillionsSQL {
    
    
    /// <summary>
    /// PavillionListWindow
    /// </summary>
    public partial class PavillionListWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\PavillionListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button back_button;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\PavillionListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid PavGrid;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\PavillionListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ShopBox;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\PavillionListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button booklist_button;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\PavillionListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button book_button;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\PavillionListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox StatusBox;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\PavillionListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label statusLabel;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\PavillionListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button status_change_button;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\PavillionListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button status_apply_button;
        
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
            System.Uri resourceLocater = new System.Uri("/PavillionsSQL;component/pavillionlistwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\PavillionListWindow.xaml"
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
            this.back_button = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\PavillionListWindow.xaml"
            this.back_button.Click += new System.Windows.RoutedEventHandler(this.back_button_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.PavGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 13 "..\..\PavillionListWindow.xaml"
            this.PavGrid.SelectedCellsChanged += new System.Windows.Controls.SelectedCellsChangedEventHandler(this.PavGrid_SelectedCellsChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ShopBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 24 "..\..\PavillionListWindow.xaml"
            this.ShopBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ShopBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.booklist_button = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\PavillionListWindow.xaml"
            this.booklist_button.Click += new System.Windows.RoutedEventHandler(this.booklist_button_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.book_button = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\PavillionListWindow.xaml"
            this.book_button.Click += new System.Windows.RoutedEventHandler(this.book_button_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.StatusBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.statusLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.status_change_button = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\PavillionListWindow.xaml"
            this.status_change_button.Click += new System.Windows.RoutedEventHandler(this.status_change_button_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.status_apply_button = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\PavillionListWindow.xaml"
            this.status_apply_button.Click += new System.Windows.RoutedEventHandler(this.status_apply_button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

