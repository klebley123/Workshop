﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Workshop.ViewModel;
using Workshop.Views;

namespace Workshop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //var loginWindow = new LoginView();
            //if (loginWindow.ShowDialog() == true)
            //{
            //    Application.Current.Shutdown();
            //}
            //InitializeComponent();
            //ConnectToDatabase();
            //DataContext = new MainViewModel();
           

            var loginWindow = new LoginView();
            bool? loginResult = loginWindow.ShowDialog(); //Pobieramy wynik logowania
            Console.WriteLine($"Wynik logowania przed sprawdzeniem: {loginResult}");
            Console.WriteLine($"Wynik logowania: {loginResult}");

            if (loginResult == true) // Jeśli logowanie zakończyło się sukcesem
            {
                Console.WriteLine("Logowanie udane! Otwieram MainWindow...");
                InitializeComponent();
                ConnectToDatabase();
                DataContext = new MainViewModel();
            }
            else
            {
                Console.WriteLine("Logowanie nieudane. Zamykam aplikację...");
                Application.Current?.Shutdown();  // Zamykamy aplikację, jeśli logowanie nie było udane
            }
        }

        private void ConnectToDatabase()
        {
            string connetionString;
            SqlConnection connection;                             //   
            connetionString = @"Data Source=LAPTOP-RJLJ2K5G;Initial Catalog=Workshop; Integrated Security=True; User ID=sa;Password=haslo";
            connection = new SqlConnection(connetionString);
            connection.Open();
            MessageBox.Show("Connection Open  !");
            connection.Close();
        }
        //TO DO ==================================================================
        //wyswietlanie drugiego okna zrobione zrób jeszcze obsługę przycisku wstecz w drugim oknie 
        //obsługę bazy danych wyświetlenia danych z bazy w pierwszym oknie 
    }
}
