using Cosmetics_Shop.Services.Interfaces;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace Cosmetics_Shop.Services
{
    public class FilePickerService : IFilePickerService
    {
        private Window _windowFocus = null;

        public FilePickerService()
        { }

        public async Task<string> PickFileAsync(List<string> filters)
        {
            if (_windowFocus == null)
            {
                return string.Empty;
            }

            var filePicker = new FileOpenPicker();

            // Lấy HWND từ cửa sổ hiện tại
            var hwnd = WindowNative.GetWindowHandle(_windowFocus);
            InitializeWithWindow.Initialize(filePicker, hwnd);

            // Cấu hình file picker
            filePicker.SuggestedStartLocation = PickerLocationId.Desktop;
            
            foreach (var filter in filters)
            {
                filePicker.FileTypeFilter.Add(filter);
            }

            // Hiển thị file picker
            var file = await filePicker.PickSingleFileAsync();
            return file?.Path;
        }
    

        public void SetWindowFocus(Window window)
        {
            _windowFocus = window;
        }
    }
}
