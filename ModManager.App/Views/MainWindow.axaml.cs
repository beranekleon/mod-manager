using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using ModManager.App.ViewModels;

namespace ModManager.App.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);

        if (DataContext is MainWindowViewModel vm)
        {
            vm.RequestFilePicker += async () =>
            {
                var files = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
                {
                    Title = "Select an Image",
                    FileTypeFilter = new[] { FilePickerFileTypes.ImagePng },
                    AllowMultiple = false
                });

                return files.FirstOrDefault();
            };
        }
    }
}