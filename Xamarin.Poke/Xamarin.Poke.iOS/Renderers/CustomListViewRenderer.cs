using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Poke.CustomControls;

[assembly: Xamarin.Forms.ExportRenderer(typeof(CustomListView), typeof(Xamarin.Poke.iOS.Renderers.CustomListViewRenderer))]

namespace Xamarin.Poke.iOS.Renderers
{
    public class CustomListViewRenderer : Xamarin.Forms.Platform.iOS.ListViewRenderer
    {
        
        /// <summary>
        /// Called when the portable element changes.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnElementChanged(Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                // Stock XF renderer uses a combined UITableViewSource to configure both UITableViewDataSource and UITableViewDelegate.
                // However, as this class is internal within the XF renderer we can't extend it. So we do this...
                // By overriding just the delegate, we can intercept what we wish and route the rest back to the UITableViewSource in the XF renderer.
                Control.Delegate = new ListViewTableViewDelegate(this);
            }
        }

       
        /// <summary>
        /// Custom <see cref="UITableViewDelegate"/> to use when <see cref="ListView.HasUnevenRows"/> is false.
        /// Note: This allows us to intercept base renderer lifecycle methods i.e. capture scrolling.
        /// </summary>
        private class ListViewTableViewDelegate : UITableViewDelegate
        {
           
            private ListView _element;
            private UITableViewSource _source;

           
            /// <summary>
            /// Initializes a new instance of the <see cref="ListViewTableViewDelegate"/> class.
            /// </summary>
            /// <param name="renderer">The <see cref="CustomListViewRenderer"/>.</param> 
            public ListViewTableViewDelegate(CustomListViewRenderer renderer)
            {
                _element = renderer.Element;
                _source = renderer.Control.Source;
            }


            /// <summary>
            /// Handle the dragging ended behavior.
            /// </summary>
            /// <param name="scrollView">The scroll view.</param>
            /// <param name="willDecelerate">If set to <c>true</c> will decelerate.</param>
            public override void DraggingEnded(UIScrollView scrollView, bool willDecelerate)
            {
                _source.DraggingEnded(scrollView, willDecelerate);
            }

            /// <summary>
            /// Handle the dragging started behavior.
            /// </summary>
            /// <param name="scrollView">The scroll view.</param>
            public override void DraggingStarted(UIScrollView scrollView)
            {
                _source.DraggingStarted(scrollView);
            }

            /// <summary>
            /// Gets the height for a header.
            /// </summary>
            /// <returns>The height for the header.</returns>
            /// <param name="tableView">The <see cref="UITableView"/>.</param>
            /// <param name="section">The section index.</param>
            public override System.nfloat GetHeightForHeader(UITableView tableView, System.nint section)
            {
                return _source.GetHeightForHeader(tableView, section);
            }

            /// <summary>
            /// Gets the view for header.
            /// </summary>
            /// <returns>The view for header.</returns>
            /// <param name="tableView">The <see cref="UITableView"/>.</param>
            /// <param name="section">The section index.</param>
            public override UIView GetViewForHeader(UITableView tableView, System.nint section)
            {
                return _source.GetViewForHeader(tableView, section);
            }

            /// <summary>
            /// Handle the row deselected behavior.
            /// </summary>
            /// <param name="tableView">The <see cref="UITableView"/>.</param>
            /// <param name="indexPath">The index path for the row.</param>
            public override void RowDeselected(UITableView tableView, Foundation.NSIndexPath indexPath)
            {
                _source.RowDeselected(tableView, indexPath);
            }

            /// <summary>
            /// Handle the row selected behavior.
            /// </summary>
            /// <param name="tableView">The <see cref="UITableView"/>.</param>
            /// <param name="indexPath">The index path for the row.</param>
            public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
            {
                _source.RowSelected(tableView, indexPath);
            }

            /// <summary>
            /// Handle the scroll behavior.
            /// </summary>
            /// <param name="scrollView">The scroll view.</param>
            public override void Scrolled(UIScrollView scrollView)
            {
                _source.Scrolled(scrollView);
                SendScrollEvent(scrollView.ContentOffset.Y);
            }


            /// <summary>
            /// Send the scrolled event to the portable event handler.
            /// </summary>
            /// <param name="y">The vertical content offset.</param>
            private void SendScrollEvent(double y)
            {
                var element = _element as CustomListView;
                var args = new CustomListView.CancelableScrolledEventArgs(0, y);

                element?.OnScrolled(args);
            }

        }
    }
}
