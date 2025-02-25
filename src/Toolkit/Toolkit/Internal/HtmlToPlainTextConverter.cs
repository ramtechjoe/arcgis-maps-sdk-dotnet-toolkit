﻿// /*******************************************************************************
//  * Copyright 2012-2018 Esri
//  *
//  *  Licensed under the Apache License, Version 2.0 (the "License");
//  *  you may not use this file except in compliance with the License.
//  *  You may obtain a copy of the License at
//  *
//  *  http://www.apache.org/licenses/LICENSE-2.0
//  *
//  *   Unless required by applicable law or agreed to in writing, software
//  *   distributed under the License is distributed on an "AS IS" BASIS,
//  *   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  *   See the License for the specific language governing permissions and
//  *   limitations under the License.
//  ******************************************************************************/

#if WPF || WINDOWS_XAML
using System.Globalization;

namespace Esri.ArcGISRuntime.Toolkit.Internal
{
    /// <summary>
    /// *FOR INTERNAL USE* Strips out html-code to return plain text.
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public sealed partial class HtmlToPlainTextConverter : IValueConverter
    {
        /// <inheritdoc />
        object? IValueConverter.Convert(object? value, Type targetType, object parameter,
#if WINDOWS_XAML
            string language)
#else
            CultureInfo culture)
#endif
        {
            return value is string ? ((string)value)?.ToPlainText() : value;
        }

        /// <inheritdoc />
        object? IValueConverter.ConvertBack(object? value, Type targetType, object parameter,
#if WINDOWS_XAML
            string language)
#else
            CultureInfo culture)
#endif
        {
            throw new NotSupportedException();
        }
    }
}
#endif