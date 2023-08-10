using PokeApp.Services;

namespace PokeApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Register<IPokemonService, PokemonService>();

            MainPage = new AppShell();
        }
    }
}