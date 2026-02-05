using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;

namespace ModManager.App.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private Bitmap? _selectedImage;
    public Bitmap? SelectedImage
    {
        get => _selectedImage;
        set => SetProperty(ref _selectedImage, value);
    }

    public IAsyncRelayCommand OpenFileCommand { get; }

    public event System.Func<Task<IStorageFile?>>? RequestFilePicker;

    public MainWindowViewModel()
    {
        OpenFileCommand = new AsyncRelayCommand(async () =>
        {
            if (RequestFilePicker != null)
            {
                var file = await RequestFilePicker.Invoke();
                if (file != null)
                {
                    await using var stream =  await file.OpenReadAsync();
                    SelectedImage = new Bitmap(stream);
                }
            }
        });
    }
}
