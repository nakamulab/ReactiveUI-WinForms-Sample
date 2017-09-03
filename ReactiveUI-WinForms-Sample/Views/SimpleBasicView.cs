using ReactiveUI;
using System;
using System.Reactive.Linq;
using System.Windows.Forms;

namespace ReactiveUI_WinForms_Sample
{
    public partial class SimpleBasicView : Form, IViewFor<SimpleBasicViewModel>
    {
         #region 1.ReactiveUI MVVM
        public SimpleBasicViewModel ViewModel { get; set; }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (SimpleBasicViewModel)value; }
        }
        #endregion

        public SimpleBasicView()
        {
            InitializeComponent();


            #region 1.ReactiveUI MVVM
            // Bind the view to the ReactiveUI viewmode
            this.WhenActivated(d =>
            {
                d(this.Bind(ViewModel, vm => vm.Text1, v => v.textBox1.Text));
                d(this.Bind(ViewModel, vm => vm.Text2, v => v.textBox2.Text));
                d(this.OneWayBind(ViewModel, vm => vm.Result, v => v.label1.Text));
                d(this.BindCommand(ViewModel, vm => vm.CombiningText, v => v.button1));

                d(this.Bind(ViewModel, vm => vm.Text1Color, v => v.textBox1.BackColor));

            });

            ViewModel = new SimpleBasicViewModel();
            #endregion

            #region 2.Event processing using System.Reactive.Linq
            var onMouse = Observable.FromEvent<EventHandler, EventArgs>(
               h => (_, e) => h(e),
               ev => textBox1.Click += ev,
               ev => textBox1.Click += ev);

            onMouse.Subscribe(ViewModel.ChangeTextColor);

            // When introducing reactiveui-events-winforms
            // this.textBox1.Events().Click.Subscribe(ViewModel.ChangeTextColor);
            #endregion
        }



    }
}
