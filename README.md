# Exportador de Dados para CSV

Este sistema foi desenvolvido utilizando **WPF .NET Framework**. O objetivo principal é exportar os dados de um sistema legado para arquivos no formato **CSV**, visando a futura importação desses dados em um sistema web.

## Funcionalidades

- **Exportação de Dados:** Exporta dados de um banco de dados legado para arquivos CSV.
- **Conexão ODBC:** Realiza a conexão com o banco de dados utilizando ODBC.
- **Parâmetro de Empresa:** Necessário passar um identificador de empresa como parâmetro para as consultas.
- **Validação de Dados:** Os dados e a empresa são validados através de queries SQL.
- **Customização:** Para adaptar a aplicação a outros sistemas, basta ajustar as consultas SELECT no código.

## Requisitos

- **.NET Framework:** A aplicação foi desenvolvida utilizando o .NET Framework.
- **ODBC Driver:** Certifique-se de que o driver ODBC apropriado está instalado e configurado para conectar ao banco de dados.

## Instruções de Uso

1. **Configuração da Conexão:**
   - Abra o arquivo de configuração da aplicação e ajuste as strings de conexão ODBC conforme necessário.

2. **Parâmetro de Empresa:**
   - Ao iniciar a aplicação, insira o identificador da empresa para a qual os dados devem ser exportados.

3. **Customização das Consultas:**
   - Modifique os SELECTs no código-fonte para atender às suas necessidades específicas de exportação de dados.

4. **Execução:**
   - Compile e execute a aplicação. A exportação dos dados será realizada e os arquivos CSV serão salvos no caminho especificado.

## Estrutura do Projeto

- **ExportView.xaml:** Interface de usuário para seleção de dados e início da exportação.
- **ExportViewModel.cs:** Lógica de exportação e manipulação dos dados.
- **BaseViewModel.cs:** Implementação da base para todos os ViewModels, incluindo notificação de mudanças de propriedades.

## Contribuindo

Se você quiser contribuir para o projeto, por favor, faça um fork do repositório e envie um pull request com suas alterações. Para questões ou sugestões, abra uma issue no GitHub.

---
