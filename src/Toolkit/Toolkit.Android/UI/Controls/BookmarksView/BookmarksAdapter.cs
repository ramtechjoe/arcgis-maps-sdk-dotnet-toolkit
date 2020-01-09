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

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Esri.ArcGISRuntime.Mapping;

namespace Esri.ArcGISRuntime.Toolkit.UI.Controls
{
    /// <summary>
    /// Creates the UI for the list items in the associated list of bookmarks.
    /// </summary>
    internal class BookmarksAdapter : RecyclerView.Adapter
    {
        private IEnumerable<Bookmark> _bookmarks;
        private readonly Context _context;

        internal BookmarksAdapter(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Sets the list used by the adapter. Avoids re-drawing if the <paramref name="bookmarks"/> list
        /// is the same as what has already been shown.
        /// </summary>
        /// <param name="bookmarks">List of bookmarks to display.</param>
        public void SetList(IEnumerable<Bookmark> bookmarks)
        {
            if (ReferenceEquals(bookmarks, _bookmarks))
            {
                return;
            }

            _bookmarks = bookmarks;
            if (_bookmarks is INotifyCollectionChanged incc)
            {
                var listener = new Internal.WeakEventListener<INotifyCollectionChanged, object, NotifyCollectionChangedEventArgs>(incc)
                {
                    OnEventAction = (instance, source, eventArgs) =>
                    {
                        // TODO - be more specific about data changes; current approach causes crashes
                        NotifyDataSetChanged();
                    },
                    OnDetachAction = (instance, weakEventListener) => instance.CollectionChanged -= weakEventListener.OnEvent
                };

                incc.CollectionChanged += listener.OnEvent;
            }

            // TODO - be more specific about data changes; current approach causes crashes
            NotifyDataSetChanged();
        }

        public override int ItemCount => _bookmarks?.Count() ?? 0;

        /// <inheritdoc />
        public override long GetItemId(int position) => position;

        // TODO - implement click events
        public event EventHandler<BookmarkSelectedEventArgs> BookmarkSelected;

        public void ClearList()
        {
            _bookmarks = null;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            BookmarkItemViewHolder bookmarkHolder = holder as BookmarkItemViewHolder;
            if (_bookmarks != null && _bookmarks.Count() > position)
            {
                bookmarkHolder.BookmarkLabel.Text = _bookmarks.ElementAt(position).Name;
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = new BookmarkItemView(_context);

            BookmarkItemViewHolder holder = new BookmarkItemViewHolder(itemView);
            return holder;
        }
    }
}