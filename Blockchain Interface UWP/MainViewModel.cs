using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store.Preview.InstallControl;
using Blockchain_Interface_UWP.Annotations;

namespace Blockchain_Interface_UWP
{
    class MainViewModel : INotifyPropertyChanged
    {
        private NodeHandler _nodeHandler;
        private string _uri;
        private Nodes _nodes;
        private Chain _chain;
        private Message _message;
        public MainViewModel()
        {
            _message = new Message();
            _nodeHandler = new NodeHandler();
            Uri = "127.0.0.1:5000";
            GetNodesCommand = new RelayCommand<bool>((() => Nodes = _nodeHandler.GetNodes(_uri).Result));
            GetChainCommand = new RelayCommand<bool>((() =>
                                                      {
                                                          Chain = _nodeHandler.GetChain(_uri).Result;
                                                          OnPropertyChanged(nameof(AllMessages));
                                                      }));
            PostMessageCommand = new RelayCommand<bool>((() =>
                                                         {
                                                             _nodeHandler.PostMessage(_uri, Message);
                                                             Message.data = "";
                                                             OnPropertyChanged(nameof(Message));
                                                         }));
            MineCommand = new RelayCommand<bool>((() => _nodeHandler.Mine(_uri)));
        }


        public string Uri {
            get { return _uri.Split("//")[1]; }
            set
            {
                _uri = "http://" + value;
                OnPropertyChanged();
            }
        }

        public Chain Chain
        {
            get { return _chain;}
            set
            {
                _chain = value;
                OnPropertyChanged();
            }
        }

        public List<Message> AllMessages {
            get
            {
                List<Message> allMessages = new List<Message>();
                if (Chain != null)
                {
                    foreach (var block in Chain.chain)
                    {
                        if (block.transactions != null)
                        {
                            allMessages.AddRange(block.transactions);
                        }
                    }
                }
                

                return allMessages;
            } }
        public Nodes Nodes
        {
            get { return _nodes; }
            set
            {
                _nodes = value;
                OnPropertyChanged();
            }
        }

        public Message Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<bool> GetNodesCommand { get; }
        public RelayCommand<bool> GetChainCommand { get; }
        public RelayCommand<bool> PostMessageCommand { get; }
        public RelayCommand<bool> MineCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
