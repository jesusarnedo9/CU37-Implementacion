using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using ImplementacionCU37.Controlador;
using ImplementacionCU37.Entidades;
using Microsoft.VSDiagnostics;

namespace ImplementacionCU37.Benchmarks
{
    [CPUUsageDiagnoser]
    public class GestorOrdenInspeccionBench
    {
        private GestorOrdenInspeccion gestor;
        private Sistema sistema;
        private Dictionary<string, OrdenDeInspeccion> dictOrdenes;
        [GlobalSetup]
        public void Setup()
        {
            // Preparar un sistema con muchos objetos para simular carga
            sistema = new Sistema();
            // Crear muchos estados y asegurarnos que hay uno "Fuera de Servicio"
            sistema.EstadosDisponibles = new List<Estado>();
            for (int i = 0; i < 200; i++)
            {
                sistema.EstadosDisponibles.Add(new Estado { ambito = i % 2 == 0 ? Estado.AMBITO_SISMOGRAFO : Estado.AMBITO_OI, nombreEstado = "Otro" + i });
            }

            sistema.EstadosDisponibles.Add(new Estado { ambito = Estado.AMBITO_SISMOGRAFO, nombreEstado = Estado.ESTADO_FUERA_SERVICIO_S });
            // Crear muchas ordenes
            sistema.Ordenes = new List<OrdenDeInspeccion>();
            dictOrdenes = new Dictionary<string, OrdenDeInspeccion>();
            var estacionDummy = new EstacionSismologica(new Sismografo { identificadorSismografo = "S1" });
            for (int i = 0; i < 5000; i++)
            {
                var orden = new OrdenDeInspeccion(i, DateTime.Now.AddMinutes(-i), estacionDummy, null, null)
                {
                    fechaHoraFinalizacion = DateTime.Now.AddMinutes(-i)
                };
                sistema.Ordenes.Add(orden);
                dictOrdenes[i.ToString()] = orden;
            }

            gestor = new GestorOrdenInspeccion(sistema, null);
            // No podemos llamar a métodos que usan la UI en el gestor (pantalla == null),
            // pero necesitamos poner la lista interna "ordenes" para probar ordenarOI si fuera necesario.
            var ordenesField = typeof(GestorOrdenInspeccion).GetField("ordenes", BindingFlags.NonPublic | BindingFlags.Instance);
            ordenesField.SetValue(gestor, sistema.Ordenes);
        }

        [Benchmark]
        public Dictionary<string, OrdenDeInspeccion> OrdenarOI_Benchmark()
        {
            // Mide el método ordenarOI
            return gestor.ordenarOI(new Dictionary<string, OrdenDeInspeccion>(dictOrdenes));
        }

        [Benchmark]
        public Estado BuscarEstadoFueraServicio_Benchmark()
        {
            // Mide la búsqueda de estado fuera de servicio
            return gestor.buscarEstadoFueraServicio();
        }
    }
}