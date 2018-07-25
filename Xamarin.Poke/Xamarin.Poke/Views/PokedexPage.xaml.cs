using Poke.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Poke.CustomControls;
using Xamarin.Poke.ViewModels;
using static Xamarin.Poke.CustomControls.CustomListView;

namespace Xamarin.Poke.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PokedexPage : ContentPage
	{
		public PokedexPage ()
		{
			InitializeComponent ();

            this.header.PropertyChanged += Header_PropertyChanged;
            this.monsterResumeListView.Scrolled += MonsterResumeListView_Scrolled;
            this.tamanhoHeader = this.header.HeightRequest;
            this.pesquisaEntry.Unfocused += PesquisaEntry_Unfocused;
        }

        private void PesquisaEntry_Unfocused(object sender, FocusEventArgs e)
        {
            this.hidePesquisa();
        }

        private double tamanhoHeader;
        private double buttonSize = (double)App.Current.Resources["buttonSize"];
        private double screenHeight = (double)App.Current.Resources["screenDensityHeight"];
        private double screenWidth = (double)App.Current.Resources["screenDensityWidth"];
        private double density = (double)App.Current.Resources["screenDensity"];

        private void MonsterResumeListView_Scrolled(object sender, CancelableScrolledEventArgs e)
        {
            var novoTamanho = tamanhoHeader - e.ScrollY;

            if (novoTamanho <= buttonSize) novoTamanho = buttonSize;
            if (novoTamanho >= tamanhoHeader) novoTamanho = tamanhoHeader;

            if (novoTamanho <= tamanhoHeader && novoTamanho >= buttonSize)
            {
                this.header.HeightRequest = novoTamanho;
            }
        }

        private void Header_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.header.HeightRequest) || e.PropertyName == nameof(this.header.Height))
            {
                var height = e.PropertyName == nameof(this.header.HeightRequest) ? this.header.HeightRequest : this.header.Height;

                var indiceMenor = (height - this.buttonSize) / (this.tamanhoHeader - this.buttonSize);
                indiceMenor = indiceMenor > 1 ? 1 : indiceMenor;

                var fontBig = (double)App.Current.Resources["fontBig"];
                var fontSmall = (double)App.Current.Resources["fontMedium"];
                    
                this.headerSubTitulo.Opacity = 1 - ((1 - indiceMenor) * 2);
                this.arrow.Opacity = 1 - ((1 - indiceMenor) * 2);
                AbsoluteLayout.SetLayoutBounds(this.headerTitulo, new Rectangle(0.5 * (indiceMenor), .6, -1, -1));
                this.headerTitulo.Scale = (fontSmall + ((fontBig - fontSmall) * indiceMenor)) / fontBig;
                if (this.pesquisaEntry.IsFocused)
                {
                    this.hidePesquisa();
                    this.pesquisaEntry.Unfocus();
                }

                //var indiceMaior = (this.screenHeight - height) / (this.screenHeight - this.tamanhoHeader);
                //indiceMaior = indiceMaior > 1 ? 1 : indiceMaior;
                //this.headerTitulo.Opacity = 1 - ((1 - indiceMaior) * 2);

                //this.listViewSelecaoRegiao.Opacity = ((1 - indiceMaior) * 2);

            }
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            App.Current.MontersListAllResumeViewModel.LoadCommand.Execute(null);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var view = (ContentView)sender;
            Task.Run(async () =>
            {
                if (this.pesquisa.TranslationX <= 0)
                {
                    
                    view.ScaleTo(0.8, 100);
                    view.ScaleTo(1, 100);
                    this.hidePesquisa();
                }
                else
                {
                    view.ScaleTo(0.8, 100);
                    view.ScaleTo(1, 100);
                    this.showPesquisa();
                    this.pesquisaEntry.Focus();
                }
            });
            
        }

        private async Task showPesquisa()
        {
            this.pesquisa.TranslateTo(0, 0, 300, Easing.BounceOut);
            this.pesquisaEntry.FadeTo(1, 200);
        }

        private async Task hidePesquisa()
        {
            this.pesquisa.TranslateTo(screenWidth - buttonSize, 0, 300, Easing.BounceOut);
            this.pesquisaEntry.FadeTo(0, 200);
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            var view = (ContentView)sender;
            Task.Run(async () =>
            {
                if (view.Rotation <= 10)
                {
                    view.ScaleTo(0.8, 100);
                    view.ScaleTo(1, 100);
                    
                    //view.RotateTo(180, 100);
                    //view.TranslateTo(0, 500, 100, Easing.BounceOut);
                    this.headerTitulo.FadeTo(0, 200, Easing.SinOut);
                    this.headerSubTitulo.TranslateTo(0, - 190, 200, Easing.SinOut);
                    //this.search.TranslateTo(buttonSize, 0, 200, Easing.SinOut);
                    this.search.FadeTo(0, 200, Easing.SinOut);
                    //this.arrow.TranslateTo(0, 20, 200);
                    await this.header.LayoutTo(new Rectangle(this.header.X, this.header.Y, this.header.Width, screenHeight), 200, Easing.SinOut);
                    this.listViewSelecaoRegiao.FadeTo(1, 100);
                    view.RotateTo(180, 300, Easing.BounceOut);
                    //this.selecao.FadeTo(1, 200);
                }
                else
                {
                    view.ScaleTo(0.8, 100);
                    view.ScaleTo(1, 100);
                    
                    //view.RotateTo(0, 100);
                    //view.TranslateTo(0, 0, 100, Easing.BounceOut);
                    //await this.selecao.FadeTo(0, 200);
                    //await this.selecaoArea.TranslateTo(0, -500, 100);
                    //this.selecaoArea.TranslationY = 1000;
                    this.listViewSelecaoRegiao.FadeTo(0, 100);
                    await view.RotateTo(0, 300, Easing.BounceOut);
                    this.headerTitulo.FadeTo(1, 200, Easing.SinOut);
                    this.headerSubTitulo.TranslateTo(0, 0, 200, Easing.SinOut);
                    //this.search.TranslateTo(0, 0, 200, Easing.SinOut);
                    this.search.FadeTo(1, 200, Easing.SinOut);
                    //this.arrow.TranslateTo(0, 0, 200);
                    this.header.LayoutTo(new Rectangle(this.header.X, this.header.Y, this.header.Width, this.tamanhoHeader), 200, Easing.SinOut);
                }
            });

        }

        private void monsterResumeListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var listView = (CustomListView)sender;
            App.Current.MontersListAllResumeViewModel.Monster = (MonsterResume)e.SelectedItem;
            listView.SelectedItem = null;
            
        }

        private double position;

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            var view = (View)sender;
            var listView = this.monsterResumeListView;
            var m = this.main;
            var h = this.header;
            var mv = this.monsterView;
            var s = this.sombra;
            var monsterResume = (MonsterResume)view.BindingContext;
            App.Current.MontersListAllResumeViewModel.Monster = monsterResume;
            Task.Run(async () =>
            {
                
                await view.ScaleTo(0.85, 100);
                await view.ScaleTo(1, 100);

                var scrollPosition = (listView.ScrollPosition);
                var index = ((IEnumerable<MonsterResume>)listView.ItemsSource).ToList().IndexOf(monsterResume);
                this.position = ((index * listView.RowHeight) - scrollPosition) + 200;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //m.Children.Add(new ContentView { BackgroundColor = Color.Blue },
                    //new Rectangle(0, position, 1, listView.RowHeight),
                    //flags: AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.XProportional);

                    AbsoluteLayout.SetLayoutFlags(mv, AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.XProportional);
                    AbsoluteLayout.SetLayoutBounds(mv, new Rectangle(0, position, 1, listView.RowHeight));
                    //mv.Layout(new Rectangle(0, position, this.screenWidth, listView.RowHeight));
                    mv.IsVisible = true;
                    s.IsVisible = true;
                    s.FadeTo(0.7, 200);

                    var an = new Animation(
                        v =>
                        {

                            AbsoluteLayout.SetLayoutFlags(mv, AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.XProportional);
                            AbsoluteLayout.SetLayoutBounds(mv, new Rectangle(0, position - (position * v), 1, listView.RowHeight + ((screenHeight - listView.RowHeight) * v)));
                            this.fechaMonsterView.HeightRequest = v * 40;
                        }, 0, 1, Easing.SinOut);
                    an.Commit(this, "teste", length: 300, finished: async (d, b) =>
                    {
                        this.fechaMonsterView.FadeTo(1, 100, Easing.SinOut);
                    });
                    

                });
                 
                
            });
            
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            var view = (View)sender;
            var listView = this.monsterResumeListView;
            var mv = this.monsterView;
            var s = this.sombra;
            Task.Run(async () =>
            {
                await view.ScaleTo(0.85, 100);
                await view.ScaleTo(1, 100);

                await this.fechaMonsterView.FadeTo(0, 100, Easing.SinOut);
                var an = new Animation(
                    v =>
                    {
                        s.Opacity = 0.8 * ((v * 2) > 1 ? 1 : (v * 2));
                        AbsoluteLayout.SetLayoutFlags(mv, AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.XProportional);
                        AbsoluteLayout.SetLayoutBounds(mv, new Rectangle(0, position - (position * v), 1, listView.RowHeight + ((screenHeight - listView.RowHeight) * v)));
                        this.fechaMonsterView.HeightRequest = v * 40;
                    }, 1, 0, Easing.SinOut);
                an.Commit(this, "teste", length: 300, finished: async (d, b) =>
                {
                    await s.FadeTo(0, 200, Easing.SinOut);
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        mv.IsVisible = false;
                        s.IsVisible = false;
                    });
                });

            });
        }
    }
}