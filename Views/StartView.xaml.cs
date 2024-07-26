using System.Data.Odbc;
using System;
using System.Windows;
using System.Windows.Input;


namespace ExportTgWeb.Views
{
    /// <summary>
    /// Lógica interna para StartView.xaml
    /// </summary>
    public partial class StartView : Window
    {
        public StartView()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnAvancar_Click(object sender, RoutedEventArgs e)
        {
            txtEmpresaError.Text = "";

            if (string.IsNullOrWhiteSpace(txtEmpresa.Text))
            {
                txtEmpresaError.Text = "Obrigatório preencher empresa!";
                return;
            }

            if (int.TryParse(txtEmpresa.Text, out int empresaId) && empresaId >= 1 && empresaId <= 99)
            {
                if (VerificarEmpresaNoBanco(empresaId))
                {
                    ExportView exportView = new ExportView(empresaId);
                    exportView.Show();
                    this.Close();
                }
                else
                {
                    txtEmpresaError.Text = "Empresa não encontrada!";
                }
            }
            else
            {
                txtEmpresaError.Text = "Número de empresa inválido! Digite um número entre 1 e 99.";
            }
        }

        private bool VerificarEmpresaNoBanco(int empresaId)
        {
            bool empresaExiste = false;
            string connectionString = "DSN=seu_banco;UID=user_db;PWD=password_db;";

            using (OdbcConnection conn = new OdbcConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT CONCAT(Codigo) FROM banco.sua_tabela WHERE Codigo = ?";
                    using (OdbcCommand cmd = new OdbcCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Codigo", empresaId);
                        using (OdbcDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                empresaExiste = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    txtEmpresaError.Text = $"Ocorreu um erro: {ex.Message}";
                }
            }
            return empresaExiste;
        }

        private void TxtEmpresa_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private static bool IsTextNumeric(string text)
        {
            return int.TryParse(text, out _);
        }

        private void TxtEmpresa_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (int.TryParse(txtEmpresa.Text, out int value))
            {
                if (value < 1 || value > 99)
                {
                    txtEmpresa.Text = "";
                }
            }
        }
    }
}
