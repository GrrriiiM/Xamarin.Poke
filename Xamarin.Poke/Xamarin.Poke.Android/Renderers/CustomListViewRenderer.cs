using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Poke.CustomControls;
using static Xamarin.Poke.CustomControls.CustomListView;

[assembly: Xamarin.Forms.ExportRenderer(typeof(CustomListView), typeof(Xamarin.Poke.Droid.Renderers.CustomListViewRenderer))]

namespace Xamarin.Poke.Droid.Renderers
{
    public class CustomListViewRenderer : Xamarin.Forms.Platform.Android.ListViewRenderer
    {

        public CustomListViewRenderer(Context context) : base(context)
        {

        }

        /// <summary>
        /// Called when the portable element changes.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Forms.ListView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var element = e.NewElement;

                Control.SetOnScrollListener(new PixelScrollDetector(this));
                Control.ScrollChange += Control_ScrollChange;
            }
        }

        private void Control_ScrollChange(object sender, ScrollChangeEventArgs e)
        {

        }



        /// <summary>
        /// Pixel scroll detector which can be attached to a <see cref="AndroidListView"/>.
        /// </summary>
        private class PixelScrollDetector : Java.Lang.Object, AbsListView.IOnScrollListener
        {


            private Xamarin.Forms.ListView _element;
            private ListViewRenderer renderer;
            private float _density;
            private int _contentOffset = 0;

            private TrackElement[] _trackElements =
            {
                new TrackElement(0),    // Top view, bottom Y
                new TrackElement(1),    // Mid view, bottom Y
                new TrackElement(2),    // Mid view, top Y
                new TrackElement(3)     // Bottom view, top Y
            };


            /// <summary>
            /// Initializes a new instance of the <see cref="PixelScrollDetector"/> class.
            /// </summary>
            /// <param name="renderer">The <see cref="ListViewRenderer"/>.</param>
            public PixelScrollDetector(ListViewRenderer renderer)
            {
                this.renderer = renderer;
                _element = renderer.Element;
                _density = renderer.Context.Resources.DisplayMetrics.Density;
            }

            /// <summary>
            /// Called when the <see cref="AndroidListView"/> has been scrolled.
            /// </summary>
            /// <param name="view">The <see cref="AndroidListView"/> whose scroll state is being reported.</param>
            /// <param name="scrollState">The current scroll state.</param>
            public void OnScrollStateChanged(AbsListView view, [GeneratedEnum] ScrollState scrollState)
            {
                // Initialize the values every time the list is moving.
                if (scrollState == ScrollState.TouchScroll || scrollState == ScrollState.Fling)
                {
                    foreach (var t in _trackElements)
                    {
                        t.SyncState(view);
                    }
                }
            }

            /// <summary>
            /// Called when the <see cref="AndroidListView"/> is being scrolled. 
            /// If the view is being scrolled, this method will be called before the next frame of the scroll is rendered.
            /// </summary>
            /// <param name="view">The <see cref="AndroidListView"/> whose scroll state is being reported.</param>
            /// <param name="firstVisibleItem">The index of the first visible cell.</param>
            /// <param name="visibleItemCount">The number of visible cells.</param>
            /// <param name="totalItemCount">The number of items in the list adaptor.</param>
            public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
            {
                var wasTracked = false;
                foreach (var t in _trackElements)
                {
                    if (!wasTracked)
                    {
                        if (t.IsSafeToTrack(view))
                        {
                            wasTracked = true;
                            _contentOffset += t.GetDeltaY();
                            
                            SendScrollEvent(_contentOffset);
                            t.SyncState(view);
                        }
                        else
                        {
                            t.Reset();
                        }
                    }
                    else
                    {
                        t.SyncState(view);
                    }
                }
            }

            /// <summary>
            /// Send the scrolled event to the portable event handler.
            /// </summary>
            /// <param name="y">The raw vertical content offset.</param>
            private void SendScrollEvent(double y)
            {
                var element = _element as CustomListView;

                // Calculate vertical offset in device-independent pixels (DIPs).
                var offset = Math.Abs(y) / _density;
                element.ScrollPosition = offset;
                var args = new CancelableScrolledEventArgs(0, offset);

                element?.OnScrolled(args);
                if (args.Cancel)
                {
                    renderer.Control.ScrollTo(0, 0);
                }

            }

            /// <summary>
            /// A wrapper to track a <see cref="Android.Views.View"/> within the native list view.
            /// </summary>
            private class TrackElement
            {

                private readonly int _position;

                private global::Android.Views.View _trackedView;
                private int _trackedViewPrevPosition;
                private int _trackedViewPrevTop;


                /// <summary>
                /// Initializes a new instance of the <see cref="TrackElement"/> class.
                /// </summary>
                /// <param name="position">The element position.</param>
                public TrackElement(int position)
                {
                    _position = position;
                }


                /// <summary>
                /// Synchronize the state for the tracked view.
                /// </summary>
                /// <param name="view">The native list view.</param>
                public void SyncState(AbsListView view)
                {
                    if (view.ChildCount > 0)
                    {
                        _trackedView = GetChild(view);
                        _trackedViewPrevTop = GetY();
                        _trackedViewPrevPosition = view.GetPositionForView(_trackedView);
                    }
                }

                /// <summary>
                /// Reset the tracked view.
                /// </summary>
                public void Reset()
                {
                    _trackedView = null;
                }

                /// <summary>
                /// Determine if it is safe to track the nested view.
                /// </summary>
                /// <param name="view">The native list view.</param>
                /// <returns>True if safe, false if not.</returns>
                public bool IsSafeToTrack(AbsListView view)
                {
                    return _trackedView != null
                        && _trackedView.Parent == view
                        && view.GetPositionForView(_trackedView) == _trackedViewPrevPosition;
                }

                /// <summary>
                /// Get the delta Y-movement for the tracked view.
                /// </summary>
                /// <returns>The delta (change) along the Y-axis.</returns>
                public int GetDeltaY()
                {
                    return GetY() - _trackedViewPrevTop;
                }


                /// <summary>
                /// Get a child view for the specified position.
                /// </summary>
                /// <param name="view">The native list view.</param>
                /// <returns>A child view for the specified position.</returns>
                private global::Android.Views.View GetChild(AbsListView view)
                {
                    switch (_position)
                    {
                        case 0:
                            return view.GetChildAt(0);
                        case 1:
                        case 2:
                            return view.GetChildAt(view.ChildCount / 2);
                        case 3:
                            return view.GetChildAt(view.ChildCount - 1);
                        default:
                            return null;
                    }
                }

                /// <summary>
                /// Get the Y-anchor point for the tracked view.
                /// </summary>
                /// <returns>The Y anchor point.</returns>
                private int GetY()
                {
                    return _position <= 1
                        ? _trackedView.Bottom
                        : _trackedView.Top;
                }


            }

        }
    }
}