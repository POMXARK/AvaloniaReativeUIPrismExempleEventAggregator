
using Prism.Events;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;

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

    public class MessageSendEvent : PubSubEvent<string>{}
}
