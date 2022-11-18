
using Prism.Events;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Reactive;

namespace AvaloniaReativeUIPrismExempleEventAggregator.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;

        public string Greeting => "Welcome to Avalonia!";

        [Reactive] public string Rx1 { get; set; }  


        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            this.WhenAnyValue(x => x.Rx1)
                .Subscribe(x => _eventAggregator.GetEvent<MessageSendEvent>().Publish(x));
        }
    }

    public class TwoWindowViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;

        [Reactive] public string Rx2 { get; set; }

        public TwoWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<MessageSendEvent>().Subscribe(x => Rx2 = x); // принять изменение
        }
    }


    public class UseCommandWindowViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;

        [Reactive] public string Rx3 { get; set; }
        [Reactive] public string Rx4 { get; set; }

        public ReactiveCommand<Unit, Unit> DoTheThing { get; }

        public UseCommandWindowViewModel(IEventAggregator eventAggregator)
        {
            DoTheThing = ReactiveCommand.Create(RunTheThing);

            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<MessageSendEvent>().Subscribe(x => Rx4 = x); // принять изменение
        }

        void RunTheThing()
        {
            _eventAggregator.GetEvent<MessageSendEvent>().Publish(Rx3);
        }
    }

    public class MessageSendEvent : PubSubEvent<string>{}
}
