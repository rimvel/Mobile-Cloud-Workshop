﻿using System;
using System.Collections.ObjectModel;
using ContosoFieldService.Models;
using FreshMvvm;
using Microsoft.AppCenter.Push;
using Plugin.VersionTracking;
using Xamarin.Forms;

namespace ContosoFieldService.ViewModels
{
    public class SettingsViewModel : FreshBasePageModel
    {
        string baseUrl;
        public string BaseUrl
        {
            get { return baseUrl; }
            set
            {
                baseUrl = value;
                Helpers.Constants.BaseUrl = value;
                RaisePropertyChanged();
            }
        }

        string apiManagementKey;
        public string ApiManagementKey
        {
            get { return apiManagementKey; }
            set
            {
                apiManagementKey = value;
                Helpers.Constants.ApiManagementKey = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ThirdPartyLibrary> ThirdPartyLibraries { get; set; }
        public string Version { get; set; }

        bool notificationsEnabled;
        public bool NotificationsEnabled
        {
            get
            {
                return notificationsEnabled;
            }
            set
            {
                notificationsEnabled = value;
                Push.SetEnabledAsync(notificationsEnabled);
            }
        }

        ThirdPartyLibrary selectedLibrary;
        public ThirdPartyLibrary SelectedLibrary
        {
            get
            {
                return selectedLibrary;
            }
            set
            {
                selectedLibrary = value;
                RaisePropertyChanged();
                if (value != null)
                    OpenUrlCommand.Execute(selectedLibrary.Url);
            }
        }

        public Command GitHubCommand
        {
            get
            {
                return new Command(() =>
                {
                    Device.OpenUri(new Uri("http://aka.ms/mobilecloudworkshop"));
                });
            }
        }

        public Command<string> OpenUrlCommand
        {
            get
            {
                return new Command<string>((string url) =>
                {
                    Device.OpenUri(new Uri(url));
                    SelectedLibrary = null;
                });
            }
        }

        public SettingsViewModel()
        {
            Version = $"{CrossVersionTracking.Current.CurrentVersion} (Build {CrossVersionTracking.Current.CurrentBuild})";

            ThirdPartyLibraries = new ObservableCollection<ThirdPartyLibrary>();
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("CarouselView", "Alex Rainman", "https://github.com/alexrainman/CarouselView"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("Lottie", "AirBnB, Martijn van Dijk", "https://github.com/martijn00/LottieXamarin"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("Corcav.Behaviors", "Corrado Cavalli", "https://github.com/corradocavalli/Corcav.Behaviors"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("Forms Toolkit", "James Montemagno", "https://github.com/jamesmontemagno/xamarin.forms-toolkit"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("FreshMvvm", "Michael Ridland", "https://github.com/rid00z/FreshMvvm"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("Humanizer", "Mehdi Khalili", "https://github.com/Humanizr/Humanizer"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("Refit", "Paul Betts", "https://github.com/paulcbetts/refit"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("MvvmHelpers", "James Montemagno", "https://github.com/jamesmontemagno/mvvm-helpers"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("FFImageLoading", "Daniel Luberda", "https://github.com/luberda-molinet/FFImageLoading"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("Visual Studio App Center", "Microsoft", "http://appcenter.ms"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("Version Tracking Plugin", "Colby Williams", "https://github.com/colbylwilliams/VersionTrackingPlugin"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("Media Plugin for Xamarin and Windows", "James Montemagno", "https://github.com/jamesmontemagno/MediaPlugin"));
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            Version = $"{CrossVersionTracking.Current.CurrentVersion} (Build {CrossVersionTracking.Current.CurrentBuild})";
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            NotificationsEnabled = await Push.IsEnabledAsync();
            BaseUrl = Helpers.Constants.BaseUrl;
            ApiManagementKey = Helpers.Constants.ApiManagementKey;
        }
    }
}
