using Easyyyyy.Core;
using Easyyyyy.Models;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Easyyyyy.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private bool _isToggleMode = App.configApplication.isToggleMode;
        public bool isToggleMode
        {
            get => _isToggleMode;
            set
            {
                _isToggleMode = value;
                onPropertyChanged(nameof(isToggleMode));
                App.configApplication.isToggleMode = value;
                App.updateConfig();
            }
        }

        private bool _isLeftClick = App.configApplication.isLeftClick;
        public bool isLeftClick
        {
            get => _isLeftClick;
            set
            {
                _isLeftClick = value;
                onPropertyChanged(nameof(isLeftClick));
                App.configApplication.isLeftClick = value;
                App.updateConfig();
            }
        }

        private bool _isEnabled;
        public bool isEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                onPropertyChanged(nameof(isEnabled));

                statusText = value ? "ON" : "OFF";
            }
        }

        private bool _isDefaultClicks = App.configApplication.isDefaultClicks;
        public bool isDefaultClicks
        {
            get => _isDefaultClicks;
            set
            {
                _isDefaultClicks = value;
                onPropertyChanged(nameof(isDefaultClicks));
                App.configApplication.isDefaultClicks = value;
                App.updateConfig();
            }
        }

        private int _countCPS = App.configApplication.countCPS;
        public int countCPS
        {
            get => _countCPS;
            set
            {
                _countCPS = value;
                onPropertyChanged(nameof(countCPS));
                App.configApplication.countCPS = value;
                App.updateConfig();
            }
        }

        private int _currentClicks = 0;
        public int currentClicks
        {
            get => _currentClicks;
            set
            {
                _currentClicks = value;
                onPropertyChanged(nameof(currentClicks));
            }
        }

        private int _totalClicks = 0;
        public int totalClicks
        {
            get => _totalClicks;
            set
            {
                _totalClicks = value;
                onPropertyChanged(nameof(totalClicks));
            }
        }

        private bool _isEnabledRandom = App.configApplication.isEnabledRandom;
        public bool isEnabledRandom
        {
            get => _isEnabledRandom;
            set
            {
                _isEnabledRandom = value;
                onPropertyChanged(nameof(isEnabledRandom));
                App.configApplication.isEnabledRandom = value;
                App.updateConfig();
            }
        }

        private string _statusText = "OFF";
        public string statusText
        {
            get => _statusText;
            set
            {
                _statusText = value;
                onPropertyChanged(nameof(statusText));
            }
        }

        private string _bindKey = App.configApplication.bindKey;
        public string bindKey
        {
            get
            {
                if (_bindKey == null)
                {
                    return "None";
                }

                return _bindKey;
            }
            set
            {
                _bindKey = value;
                onPropertyChanged(nameof(bindKey));
                App.configApplication.bindKey = value;
                App.updateConfig();
            }
        }

        private int _intBindKey = App.configApplication.intBindKey;
        public int intBindKey
        {
            get => _intBindKey;
            set
            {
                _intBindKey = value;
                onPropertyChanged(nameof(intBindKey));
                App.configApplication.intBindKey = value;
                App.updateConfig();
            }
        }

        private bool _isStateChangeBindingKey;
        public bool isStateChangeBindingKey
        {
            get => _isStateChangeBindingKey;
            set
            {
                _isStateChangeBindingKey = value;
                onPropertyChanged(nameof(isStateChangeBindingKey));
            }
        }

        public RelayCommand toggleMode
        {
            get => new RelayCommand(obj =>
            {
                isToggleMode = !isToggleMode;
            });
        }

        public RelayCommand toggleClicks
        {
            get => new RelayCommand(obj =>
            {
                isDefaultClicks = !isDefaultClicks;
            });
        }

        public RelayCommand toggleRandom
        {
            get => new RelayCommand(obj =>
            {
                isEnabledRandom = !isEnabledRandom;
            });
        }

        public RelayCommand closeApplication
        {
            get => new RelayCommand(obj =>
            {
                isStopped = true;
                Application.Current.MainWindow.Close();
            });
        }

        public RelayCommand moveApplication
        {
            get => new RelayCommand(obj =>
            {
                if (obj is MouseButtonEventArgs)
                {
                    var eventArgs = (MouseButtonEventArgs)obj;
                    if (eventArgs.ChangedButton == MouseButton.Left)
                        if (Application.Current.MainWindow != null)
                            Application.Current.MainWindow.DragMove();
                }
            });
        }

        public RelayCommand changeBindKey
        {
            get => new RelayCommand(obj =>
            {
                if (obj is KeyEventArgs && isStateChangeBindingKey)
                {
                    var eventArgs = (KeyEventArgs)obj;
                    var binding = new Core.Binding();

                    intBindKey = binding.GetIntVirtualKey(eventArgs.Key);
                    bindKey = binding.GetStringVirtualKey(eventArgs.Key);

                    isStateChangeBindingKey = false;
                }
                else if (obj is MouseButtonEventArgs && isStateChangeBindingKey)
                {
                    var eventArgs = (MouseButtonEventArgs)obj;
                    var binding = new Core.Binding();

                    if (eventArgs.ChangedButton == MouseButton.Left || eventArgs.ChangedButton == MouseButton.Right)
                        return;

                    switch (eventArgs.ChangedButton)
                    {
                        case MouseButton.Middle:
                            bindKey = "MMiddle";
                            intBindKey = 0x04;
                            break;
                        case MouseButton.XButton1:
                            bindKey = "XBtn1";
                            intBindKey = 0x05;
                            break;
                        case MouseButton.XButton2:
                            bindKey = "XBtn2";
                            intBindKey = 0x06;
                            break;
                    }

                    isStateChangeBindingKey = false;
                }
            });
        }

        public RelayCommand toggleLeftClick
        {
            get => new RelayCommand(obj =>
            {
                isLeftClick = !isLeftClick;
            });
        }

        public RelayCommand setStateChangeBindKey
        {
            get => new RelayCommand(obj =>
            {
                if (!isStateChangeBindingKey)
                {
                    isStateChangeBindingKey = true;
                }
            });
        }

        public RelayCommand openGithub
        {
            get => new RelayCommand(obj =>
            {
                Process.Start(new ProcessStartInfo("https://github.com/mentolaass/Easyyyyy"));
            });
        }

        public RelayCommand startLoops
        {
            get => new RelayCommand(obj =>
            {
                loopToggleMode();
                loopAutoClick();
                loopCounterCPS();
            });
        }


        private bool isStopped = false;

        private bool isToggleEnabled = false;

        private void loopAutoClick()
        {
            new Thread(() =>
            {
                while (true)
                {
                    if (isStopped)
                        break;

                    if (Native.GetAsyncKeyState((uint)intBindKey) || isToggleEnabled)
                    {
                        Click.execClick(countCPS, isEnabledRandom, isToggleEnabled, isLeftClick, isDefaultClicks, isToggleMode);
                        totalClicks += isDefaultClicks ? 1 : 2;
                        isEnabled = true;
                    } else
                    {
                        isEnabled = false;
                    }
                    
                    Thread.Sleep(1);
                }
            }).Start();
        }

        private void loopToggleMode()
        {
            new Thread(() =>
            {
                while (true)
                {
                    if (isToggleMode && Native.GetAsyncKeyState((uint)intBindKey))
                    {
                        isToggleEnabled = !isToggleEnabled;
                        // delay
                        Thread.Sleep(250);
                    }

                    if (isStopped)
                        break;

                    Thread.Sleep(5);
                }
            }).Start();
        }

        private void loopCounterCPS()
        {
            new Thread(() =>
            {
                while (true)
                {
                    currentClicks = totalClicks - currentClicks;
                    totalClicks = currentClicks;

                    if (isStopped)
                        break;

                    Thread.Sleep(1000);
                }
            }).Start();
        }

        public MainViewModel()
        {

        }
    }
}

