using ReactiveUI;
using System;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Forms;

namespace ReactiveUI_WinForms_Sample
{
    public class SimpleBasicViewModel : ReactiveObject
    {

        public SimpleBasicViewModel()
        {
            var canExecute = this.WhenAnyValue(vm => vm.Text1, vm => vm.Text2)
                .Select(x => !string.IsNullOrWhiteSpace(x.Item1) && !string.IsNullOrWhiteSpace(x.Item2));

            CombiningText = ReactiveCommand.Create(Disp, canExecute);
        }
        private void Disp()
        {
            // Only updating the binding property
            Result = Text1 + Text2;
        }

        #region Binding property
        private string _text1;

        public string Text1
        {
            get { return _text1; }
            set { this.RaiseAndSetIfChanged(ref _text1, value); }
        }

        private string _text2;
        public string Text2
        {
            get { return _result; }
            set { this.RaiseAndSetIfChanged(ref _result, value); }
        }

        private string _result;
        public string Result
        {
            get { return _text2; }
            set { this.RaiseAndSetIfChanged(ref _text2, value); }
        }

        public ReactiveCommand CombiningText { get; private set; }


        private Color _text1color;
        public Color Text1Color
        {
            get { return _text1color; }
            set { this.RaiseAndSetIfChanged(ref _text1color, value); }

        }

        #endregion

        public void ChangeTextColor( EventArgs _)
        {
            if (Text1Color == Color.Pink )
            {
                Text1Color = Color.White;
            }
            else
            {
                Text1Color = Color.Pink;
            }

        }


    }
}
