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
    /// Provides methods for picking files and managing window focus during file selection.
    /// </summary>
    public class FilePickerService : IFilePickerService
    {
        private Window _windowFocus = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilePickerService"/> class.
        /// </summary>
        public FilePickerService()
        { }

        /// <summary>
        /// Opens a file picker dialog to allow the user to select a file.
        /// </summary>
        /// <param name="filters">
        /// A list of file type filters to apply in the file picker (e.g., ".txt", ".jpg").
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains the selected file path as a string, or <c>null</c> if no file is selected.
        /// </returns>
        public async Task<string> PickFileAsync(List<string> filters)
        {
            if (_windowFocus == null)
            {
                return string.Empty;
            }

            var filePicker = new FileOpenPicker();

            // Get HWND from the current window
            var hwnd = WindowNative.GetWindowHandle(_windowFocus);
            InitializeWithWindow.Initialize(filePicker, hwnd);

            // Configure file picker
            filePicker.SuggestedStartLocation = PickerLocationId.Desktop;

            foreach (var filter in filters)
            {
                filePicker.FileTypeFilter.Add(filter);
            }

            // Show file picker
            var file = await filePicker.PickSingleFileAsync();
            return file?.Path;
        }

        /// <summary>
        /// Sets the focus to a specific window for the file picker operation.
        /// </summary>
        /// <param name="window">
        /// The <see cref="Window"/> object to set as the active window during file selection.
        /// </param>
        public void SetWindowFocus(Window window)
        {
            _windowFocus = window;
        }
    }
}