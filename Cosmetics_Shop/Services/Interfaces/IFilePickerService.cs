using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Services.Interfaces
{
    /// <summary>
    /// Provides methods for picking files and managing window focus during file selection.
    /// </summary>
    public interface IFilePickerService
    {
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
        Task<string> PickFileAsync(List<string> filters);

        /// <summary>
        /// Sets the focus to a specific window for the file picker operation.
        /// </summary>
        /// <param name="focus">
        /// The <see cref="Window"/> object to set as the active window during file selection.
        /// </param>
        void SetWindowFocus(Window focus);
    }

}
