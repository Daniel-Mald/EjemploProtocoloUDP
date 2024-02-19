﻿using Ejercicio1ServidorTalleres.Models;
using Ejercicio1ServidorTalleres.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Ejercicio1ServidorTalleres.ViewModels
{
    public class InscripcionesViewModel : INotifyPropertyChanged
    {
        public string IP { get; set; } = "0.0.0.0";
        public ObservableCollection<Taller> Talleres { get; set; } = new();
        List<Taller> talleres = new();//Datos Reales
        InscripcionesServer servidor = new();
        public InscripcionesViewModel()
        {
            var ips = Dns.GetHostAddresses(Dns.GetHostName());
            IP = ips.Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).
                Select(x => x.ToString()).FirstOrDefault()??"0.0.0.0";
            Cargar();
            Actualizar();

            servidor.InscripcionRealizada += Servidor_InscripcionRealizada;
        }

        private void Servidor_InscripcionRealizada(object? sender, Models.DTOs.InscripcionDTO e)
        {

        }

        public void Actualizar()
        {
            Talleres.Clear();
            foreach (var item in talleres)
            {
                Talleres.Add(item);
            }
        }
        public void Cargar()
        {

            var archivo = File.OpenRead("talleres.json");
            talleres = JsonSerializer.Deserialize<List<Taller>>(archivo) ??
                new(){
                    new Taller(){Nombre = "Canto",Alumnos = new List<Alumno>{ }},
                    new Taller(){Nombre= "Baile", Alumnos = new List<Alumno>{ } } };
            archivo.Close();
            
        }


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}