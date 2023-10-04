using Easyyyyy.Models;
using Newtonsoft.Json.Converters;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Easyyyyy.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private bool _isToggleMode;
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

        private bool _isEnabled;
        public bool isEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                onPropertyChanged(nameof(isEnabled));
            }
        }

        private bool _isDefaultClicks;
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

        private int _countCPS = 5;
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

        private bool _isEnabledRandom;
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

        private string _bindKey;
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

        private int _intBindKey;
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
                        if(Application.Current.MainWindow != null)
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
                } else if (obj is MouseButtonEventArgs && isStateChangeBindingKey)
                {
                    var eventArgs = (MouseButtonEventArgs)obj;
                    var binding = new Core.Binding();

                    if (eventArgs.ChangedButton == MouseButton.Left)
                        return;

                    switch (eventArgs.ChangedButton)
                    {
                        case MouseButton.Right:
                            bindKey = "MRight";
                            intBindKey = 0x02;
                            break;
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

        [DllImport("User32.dll")]
        public static extern bool GetAsyncKeyState(int vKey);

        private bool isStopped = false;

        private bool isToggleEnabled = false;

        private void runAutoClicker()
        {
            new Thread(() =>
            {
                var mouse = new Core.Mouse();
                isDefaultClicks = isDefaultClicks;
                isToggleMode = isToggleMode;

                while (true)
                {
                    if (isToggleMode)
                    {
                        isEnabled = isToggleEnabled;

                        if (isToggleEnabled)
                        {
                            // if double click
                            if (isDefaultClicks)
                            {
                                mouse.oneLeftClick();
                            }
                            else
                            {
                                mouse.doubleLeftClick();
                            }
                        }
                    }
                    else
                    {
                        if (GetAsyncKeyState(intBindKey))
                        {
                            isEnabled = true;

                            // if double click
                            if (isDefaultClicks)
                            {
                                mouse.oneLeftClick();
                            }
                            else
                            {
                                mouse.doubleLeftClick();
                            }
                        }
                        else
                        {
                            isEnabled = false;
                        }
                    }

                    if (isStopped)
                    {
                        break;
                    }

                    int timeToWait = 0;
                    if (!isEnabledRandom) timeToWait = 1000 / countCPS;
                    else if (countCPS > 5) timeToWait = 1000 / new Random().Next(countCPS - ((countCPS / 100) * 20), countCPS);

                    Thread.Sleep(timeToWait);
                }

                Application.Current.Shutdown();
            }).Start();
        }

        public MainViewModel()
        {
            runAutoClicker();
        }
    }
}
