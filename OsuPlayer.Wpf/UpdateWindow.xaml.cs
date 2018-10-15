﻿using Milkitic.OsuPlayer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;
namespace Milkitic.OsuPlayer
{
    /// <summary>
    /// UpdateWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateWindow : Window
    {
        private readonly Release _release;
        private Downloader _downloader;

        public UpdateWindow(Release release)
        {
            _release = release;
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var asset = _release?.Assets.FirstOrDefault(k => k.Name == "Osu-Player.zip");
            if (asset == null) return;
            _downloader = new Downloader(asset.BrowserDownloadUrl);
            _downloader.OnStartDownloading += Downloader_OnStartDownloading;
            _downloader.OnDownloading += Downloader_OnDownloading;
            _downloader.OnFinishDownloading += Downloader_OnFinishDownloading;
            await _downloader.DownloadAsync(Path.Combine(Domain.CurrentPath, "update.zip"));
        }

        private void Downloader_OnStartDownloading(long size)
        {
            Dispatcher.BeginInvoke(new Action(() => DlProgress.Maximum = size));
        }

        private void Downloader_OnDownloading(long size, long downloadedSize, long speed)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                DlProgress.Value = downloadedSize;
                LblSpeed.Content = CountSize(speed) + "/s";
                LblProgress.Content = $"{Math.Round(downloadedSize / (float)size * 100)} %";
            }));
        }

        private void Downloader_OnFinishDownloading()
        {
            //todo
            Dispatcher.BeginInvoke(new Action(Close));
        }

        private static string CountSize(long size)
        {
            string strSize = "";
            long factSize = size;
            if (factSize < 1024)
                strSize = factSize.ToString("F2") + " B";
            else if (factSize >= 1024 && factSize < 1048576)
                strSize = (factSize / 1024f).ToString("F2") + " KB";
            else if (factSize >= 1048576 && factSize < 1073741824)
                strSize = (factSize / 1024f / 1024f).ToString("F2") + " MB";
            else if (factSize >= 1073741824)
                strSize = (factSize / 1024f / 1024f / 1024f).ToString("F2") + " GB";
            return strSize;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _downloader.Interrupt();
        }
    }
}