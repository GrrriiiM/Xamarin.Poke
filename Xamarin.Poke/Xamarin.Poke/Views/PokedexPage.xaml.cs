using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
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
        private double tamanhoMenorHeader = 40;

        private void MonsterResumeListView_Scrolled(object sender, CancelableScrolledEventArgs e)
        {
            var novoTamanho = tamanhoHeader - e.ScrollY;

            if (novoTamanho <= tamanhoMenorHeader) novoTamanho = tamanhoMenorHeader;
            if (novoTamanho >= tamanhoHeader) novoTamanho = tamanhoHeader;

            if (novoTamanho <= tamanhoHeader && novoTamanho >= tamanhoMenorHeader)
            {
                this.header.HeightRequest = novoTamanho;
            }
        }

        private void Header_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.header.HeightRequest))
            {
                var fontBig = (double)App.Current.Resources["fontBig"];
                var fontSmall = (double)App.Current.Resources["fontSmall"];
                var indice = (this.header.HeightRequest - this.tamanhoMenorHeader) / (this.tamanhoHeader - this.tamanhoMenorHeader);
                this.headerSubTitulo.Opacity = 1 - ((1 - indice) * 2);
                this.arrow.Opacity = 1 - ((1 - indice) * 2);
                AbsoluteLayout.SetLayoutBounds(this.headerTitulo, new Rectangle(0.5 * (indice), .6, -1, -1));
                this.headerTitulo.Scale = (fontSmall + ((fontBig - fontSmall) * indice)) / fontBig;
                if (this.pesquisaEntry.IsFocused)
                {
                    this.hidePesquisa();
                    this.pesquisaEntry.Unfocus();
                }
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
            this.pesquisa.TranslateTo(320, 0, 300, Easing.BounceOut);
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
                    view.RotateTo(180, 300, Easing.BounceOut);
                    //view.RotateTo(180, 100);
                    //view.TranslateTo(0, 500, 100, Easing.BounceOut);
                    this.selecaoArea.TranslationY = -500;
                    await this.selecaoArea.TranslateTo(0, 0, 100);
                    this.selecao.FadeTo(1, 200);
                }
                else
                {
                    view.ScaleTo(0.8, 100);
                    view.ScaleTo(1, 100);
                    view.RotateTo(0, 300, Easing.BounceOut);
                    //view.RotateTo(0, 100);
                    //view.TranslateTo(0, 0, 100, Easing.BounceOut);
                    await this.selecao.FadeTo(0, 200);
                    await this.selecaoArea.TranslateTo(0, -500, 100);
                    this.selecaoArea.TranslationY = 1000;
                }
            });

        }
    }
}