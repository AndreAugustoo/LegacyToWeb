using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace ExportTgWeb.Views
{
    /// <summary>
    /// Lógica interna para ExportView.xaml
    /// </summary>
    public partial class ExportView : Window
    {
        public ExportView(int empresaId)
        {
            InitializeComponent();
            txtEmpresaSelecionada.Text = $"Empresa selecionada: {empresaId}";
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

        private async void btnProdutos_Click(object sender, RoutedEventArgs e)
        {
            string caminhoArquivo = @"C:\csj\fiscal\ExportacaoTg\Produtos_TgWeb.csv";
            string consultaSQL = @"SELECT * FROM banco.sua_tabela;";

            await ExportarDadosTgwebAsync(caminhoArquivo, consultaSQL);
        }

        private async void btnParticipantes_Click(object sender, RoutedEventArgs e)
        {
            string caminhoArquivo = @"C:\csj\fiscal\ExportacaoTg\Participantes_TgWeb.csv";
            string consultaSQL = @"SELECT * FROM banco.sua_tabela;";

            await ExportarDadosTgwebAsync(caminhoArquivo, consultaSQL);
        }

        private async void btnFinanceiro_Click(object sender, RoutedEventArgs e)
        {
            string caminhoArquivo = @"C:\csj\fiscal\ExportacaoTg\Financeiro_TgWeb.csv";
            string consultaSQL = @"SELECT * FROM banco.sua_tabela;";

            await ExportarDadosTgwebAsync(caminhoArquivo, consultaSQL);
        }

        private async Task ExportarDadosTgwebAsync(string caminhoArquivo, string consultaSQL)
        {
            try
            {
                Dispatcher.Invoke(() => spinnerBorder.Visibility = Visibility.Visible);

                await Task.Run(() =>
                {
                    try
                    {
                        ExportarDadosTgweb(caminhoArquivo, consultaSQL);
                    }
                    catch (Exception ex)
                    {
                        Dispatcher.Invoke(() => MessageBox.Show($"Erro ao exportar dados: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error));
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro na execução assíncrona: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Dispatcher.Invoke(() =>
                {
                    spinnerBorder.Visibility = Visibility.Collapsed;
                    OpenFolder(caminhoArquivo);
                });
            }
        }

        private static void ExportarDadosTgweb(string caminhoArquivo, string consultaSQL)
        {
            using (OdbcConnection connection = new OdbcConnection("DSN=nome_banco;UID=user_db;PWD=password_db;"))
            {
                connection.Open();
                using (OdbcCommand command = new OdbcCommand(consultaSQL, connection))
                {
                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        using (StreamWriter writer = new StreamWriter(caminhoArquivo))
                        {
                            var columnNames = GetColumnNames(reader);
                            writer.WriteLine(string.Join(";", columnNames));

                            while (reader.Read())
                            {
                                var rowData = GetRowData(reader);
                                writer.WriteLine(string.Join(";", rowData));
                            }
                        }
                    }
                }
            }
        }

        private static IEnumerable<string> GetColumnNames(OdbcDataReader reader)
        {
            var columnNames = new List<string>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                columnNames.Add(reader.GetName(i));
            }
            return columnNames;
        }

        private static IEnumerable<string> GetRowData(OdbcDataReader reader)
        {
            var rowData = new List<string>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                rowData.Add(reader[i].ToString().Replace(";", ","));
            }
            return rowData;
        }
        private void OpenFolder(string filePath)
{
    try
    {
        var folderPath = Path.GetDirectoryName(filePath);
        if (Directory.Exists(folderPath))
        {
            System.Diagnostics.Process.Start("explorer.exe", $"/e,{folderPath}");
        }
        else
        {
            MessageBox.Show("A pasta de exportação não existe.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Erro ao tentar abrir a pasta: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
    }
}
