# Introduction  
To the friends still developing on Windows Forms.  
Have you ever wanted to introduce test-driven development (TDD), or more primitively to bring order to the source code with the concept of PDS (presentation domain separation)?  
I thought so, but the framework that was useful in WPF and Xamarin could not be used on Windows Forms.  
Fortunately, you can use ReactiveUI as an MVVM framework for Windows Forms.  
As a side effect, you will be introduced to RX.  
  
  
# About ReactiveUI  
In ReactiveUI 7.0, the syntax of the command changed and a sample source up to version 6 occupying the majority of the net was pasted, but caused a compile error.  
This sample confirmed the operation with 7.0.0 as the oldest version and 7.4.0 as the latest version.  
We do not need to explain how to introduce ReactiveUI, so search nuget package manager for "ReactiveUI - WinForms".  
  
# Code description  
I have one form under the Views folder and one source of ViewModel under the ViewModels folder.  
Please look at Views / SimpleBasic.cs  
  
```   
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
```  
\# Region 1. ReactiveUI Basic is the part that BV format is MVVM-like by ReactiveUI.  
Bind is the property of the ViewModel, OneWayBind is just the property to reference, BindCommand is the command when the button is pressed.  
Please confirm with "move to definition" on ViewModel side.  
  
　  
  
```  
#region 2.Event processing using System.Reactive.Linq  
  
var onMouse = Observable.FromEvent<EventHandler, EventArgs>(  
   h => (_, e) => h(e),  
   ev => textBox1.Click += ev,  
   ev => textBox1.Click += ev);  
  
onMouse.Subscribe(ViewModel.ChangeTextColor);  
  
// When introducing reactiveui-events-winforms  
// this.textBox1.Events().Click.Subscribe(ViewModel.ChangeTextColor);  
#endregion  
```  
  
\#region 2.Event processing processing using System.Reactive.Linq is not a ReactiveUI, but it is thought that an example of event processing is necessary and added.  
If you just register an event procedure you can write it more simply, but keep the door of RX open at least.  
If reactiveui-events-winforms is introduced, it's a little simpler to write. (I wrote it in the comment)  
