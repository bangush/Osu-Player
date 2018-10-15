﻿using Milkitic.OsuPlayer.Media.Lyric;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Milkitic.OsuPlayer.Pages.Settings
{
    /// <summary>
    /// LyricPage.xaml 的交互逻辑
    /// </summary>
    public partial class LyricPage : Page
    {
        public LyricPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            EnableLyric.IsChecked = App.Config.Lyric.EnableLyric;
            if (App.Config.Lyric.LyricSource == LyricSource.Auto)
                SourceAuto.IsChecked = true;
            else if (App.Config.Lyric.LyricSource == LyricSource.Netease)
                SourceNetease.IsChecked = true;
            else if (App.Config.Lyric.LyricSource == LyricSource.Kugou)
                SourceKugou.IsChecked = true;
            else if (App.Config.Lyric.LyricSource == LyricSource.QqMusic)
                SourceQqMusic.IsChecked = true;
            if (App.Config.Lyric.ProvideType == LyricProvider.ProvideTypeEnum.PreferBoth)
                ShowAll.IsChecked = true;
            else if (App.Config.Lyric.ProvideType == LyricProvider.ProvideTypeEnum.Original)
                ShowOrigin.IsChecked = true;
            else if (App.Config.Lyric.ProvideType == LyricProvider.ProvideTypeEnum.PreferTranslated)
                ShowTrans.IsChecked = true;
            StrictMode.IsChecked = App.Config.Lyric.StrictMode;
            EnableCache.IsChecked = App.Config.Lyric.EnableCache;
        }

        private void EnableLyric_CheckChanged(object sender, RoutedEventArgs e)
        {
            if (EnableLyric.IsChecked.HasValue && EnableLyric.IsChecked.Value)
            {
                App.Config.Lyric.EnableLyric = true;
                // todo: action
            }
            else
            {
                App.Config.Lyric.EnableLyric = false;
                // todo: action
            }

            App.SaveConfig();
        }

        private void Source_CheckChanged(object sender, RoutedEventArgs e)
        {
            if (SourceAuto.IsChecked.HasValue && SourceAuto.IsChecked.Value)
                App.Config.Lyric.LyricSource = LyricSource.Auto;
            else if (SourceNetease.IsChecked.HasValue && SourceNetease.IsChecked.Value)
                App.Config.Lyric.LyricSource = LyricSource.Netease;
            else if (SourceKugou.IsChecked.HasValue && SourceKugou.IsChecked.Value)
                App.Config.Lyric.LyricSource = LyricSource.Kugou;
            else if (SourceQqMusic.IsChecked.HasValue && SourceQqMusic.IsChecked.Value)
                App.Config.Lyric.LyricSource = LyricSource.QqMusic;
            // todo: action
            App.SaveConfig();
        }

        private void Show_CheckChanged(object sender, RoutedEventArgs e)
        {
            if (ShowAll.IsChecked.HasValue && ShowAll.IsChecked.Value)
                App.Config.Lyric.ProvideType = LyricProvider.ProvideTypeEnum.PreferBoth;
            else if (ShowOrigin.IsChecked.HasValue && ShowOrigin.IsChecked.Value)
                App.Config.Lyric.ProvideType = LyricProvider.ProvideTypeEnum.Original;
            else if (ShowTrans.IsChecked.HasValue && ShowTrans.IsChecked.Value)
                App.Config.Lyric.ProvideType = LyricProvider.ProvideTypeEnum.PreferTranslated;
            // todo: action
            App.SaveConfig();
        }

        private void StrictMode_CheckChanged(object sender, RoutedEventArgs e)
        {
            if (StrictMode.IsChecked.HasValue && StrictMode.IsChecked.Value)
                App.Config.Lyric.StrictMode = true;
            else
                App.Config.Lyric.StrictMode = false;
            // todo: action
            App.SaveConfig();
        }

        private void EnableCache_CheckChanged(object sender, RoutedEventArgs e)
        {
            if (EnableCache.IsChecked.HasValue && EnableCache.IsChecked.Value)
                App.Config.Lyric.EnableCache = true;
            else
                App.Config.Lyric.EnableCache = false;
            // todo: action
            App.SaveConfig();
        }

    }
}