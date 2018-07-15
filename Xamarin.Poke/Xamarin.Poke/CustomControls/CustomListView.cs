using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Xamarin.Poke.CustomControls
{
    public class CustomListView : Xamarin.Forms.ListView
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomListView"/> class.
        /// </summary>
        public CustomListView() : base(ListViewCachingStrategy.RetainElement)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomListView"/> class.
        /// </summary>
        /// <param name="strategy">The caching strategy to use.</param>
        public CustomListView(ListViewCachingStrategy strategy) : base(strategy)
        {
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Event that is raised after a scroll completes.
        /// </summary>
        public event EventHandler<CancelableScrolledEventArgs> Scrolled;

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to be called after a scroll completes.
        /// </summary>
        /// <param name="args">The scroll event arguments.</param>
        public void OnScrolled(CancelableScrolledEventArgs args)
        {
            Scrolled?.Invoke(this, args);
        }

        public class CancelableScrolledEventArgs : ScrolledEventArgs
        {
            public bool Cancel { get; set; }
            public CancelableScrolledEventArgs(double x, double y) : base(x, y)
            {

            }
        }

        #endregion
    }
}
