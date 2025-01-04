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
    /// <summary>
    /// File picker service for selecting files from the file system.
    /// </summary>
    public class FilePickerService : IFilePickerService
    {
        /// <summary>
        /// Ưindow focus for the file picker operation.
        /// </summary>
        private Window _windowFocus = null;

        /// <summary>
        /// File picker service constructor.
        /// </summary>
        public FilePickerService()
        { }

        /// <summary>
        /// Opens a file picker dialog to allow the user to select a file.
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Sets the focus to a specific window for the file picker operation.
        /// </summary>
        /// <param name="window"></param>
        public void SetWindowFocus(Window window)
        {
            _windowFocus = window;
        }
    }
}
