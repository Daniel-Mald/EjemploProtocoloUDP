using LibrosMaui.Services;

namespace LibrosMaui
{
    public partial class App : Application
    {
        public static LibroService _librosService { get; set; } = new();
        public App()
        {
            InitializeComponent();
            Thread _thread = new Thread(Sincronizador) { IsBackground = true };
            _thread.Start();
            MainPage = new AppShell();
        }
        async void Sincronizador()
        {
            while (true)
            {
                await _librosService.GetLibros();
                Thread.Sleep(TimeSpan.FromMinutes(1));
            }
            
        }
    }
}
