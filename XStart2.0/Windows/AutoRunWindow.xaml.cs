﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using XStart.Bean;
using XStart2._0.ViewModels;

namespace XStart2._0.Windows {
    /// <summary>
    /// AutoRunWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AutoRunWindow : Window {
        AutoRunViewModel vm = new AutoRunViewModel();
        public List<Project> AutoRunProjects { get; set; }
        public List<Project> Projects { get; set; }
        public bool IsExit { get; set; } = false;
        public AutoRunWindow() {
            InitializeComponent();
            Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            vm.AutoRunProjects = AutoRunProjects;
            DataContext = vm;
        }

        private void SelectAll_Click(object sender, RoutedEventArgs e) {
            CheckBox cb = sender as CheckBox;
            foreach(Project p in AutoRunProjects) {
                p.AutoRun = cb.IsChecked;
            }
        }

        private void Run_Click(object sender, RoutedEventArgs e) {
            Projects = AutoRunProjects.Where(item => true == item.AutoRun).ToList();
            if (null != Projects && Projects.Count > 0) {
                // 启动选中的自启项
                DialogResult = true;
            } else {
                MessageBox.Show("未选择启动项目", "警告");
            }
            
        }

        private void Ignore_Click(object sender, RoutedEventArgs e) {
            // 不启动自启项
            DialogResult = false;
        }

        private void Exit_Click(object sender, RoutedEventArgs e) {
            // 直接退出
            DialogResult = false;
            IsExit = true;
        }
    }
}
