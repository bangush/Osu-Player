﻿using DMSkin.WPF;
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

namespace Milkitic.OsuPlayer.Control
{
    /// <summary>
    /// MessageBoxWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MessageBoxWindow : DMSkinSimpleWindow
    {
        public MessageBoxResult MessageBoxResult { get; set; }

        public MessageBoxWindow(string messageBoxText)
        {
            InitializeComponent();
            LblMessage.Content = messageBoxText;
        }

        public MessageBoxWindow(string messageBoxText, string caption) : this(messageBoxText)
        {
            Title = caption;
        }

        public MessageBoxWindow(string messageBoxText, string caption, MessageBoxButton button)
            : this(messageBoxText, caption)
        {
            switch (button)
            {
                case MessageBoxButton.OK:
                    BtnYes.Visibility = Visibility.Collapsed;
                    BtnNo.Visibility = Visibility.Collapsed;
                    BtnCancel.Visibility = Visibility.Collapsed;
                    break;
                case MessageBoxButton.OKCancel:
                    BtnYes.Visibility = Visibility.Collapsed;
                    BtnNo.Visibility = Visibility.Collapsed;
                    break;
                case MessageBoxButton.YesNoCancel:
                    BtnOk.Visibility = Visibility.Collapsed;
                    break;
                case MessageBoxButton.YesNo:
                    BtnOk.Visibility = Visibility.Collapsed;
                    BtnCancel.Visibility = Visibility.Collapsed;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(button), button, null);
            }
        }

        public MessageBoxWindow(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
            : this(messageBoxText, caption, button)
        {
            switch (icon)
            {
                case MessageBoxImage.None:
                    TitleBarArea.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                    ImgIcon.Visibility = Visibility.Collapsed;
                    BtnClose.DMSystemButtonForeground = new SolidColorBrush(Color.FromArgb(224, 128, 128, 128));
                    LblTitle.Foreground = new SolidColorBrush(Color.FromRgb(48, 48, 48));
                    break;
                case MessageBoxImage.Error:
                    TitleBarArea.Background = new SolidColorBrush(Color.FromRgb(242, 51, 63));
                    break;
                case MessageBoxImage.Question:
                    TitleBarArea.Background = new SolidColorBrush(Color.FromRgb(75, 154, 254));
                    break;
                case MessageBoxImage.Exclamation:
                    TitleBarArea.Background = new SolidColorBrush(Color.FromRgb(255, 170, 53));
                    break;
                case MessageBoxImage.Information:
                    TitleBarArea.Background = new SolidColorBrush(Color.FromRgb(78, 192, 69));
                    break;
            }
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult = MessageBoxResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult = MessageBoxResult.Cancel;
            Close();
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult = MessageBoxResult.Yes;
            Close();
        }

        private void BtnNo_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult = MessageBoxResult.No;
            Close();
        }

        private void DMSystemCloseButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult = MessageBoxResult.Cancel;
        }
    }
}
