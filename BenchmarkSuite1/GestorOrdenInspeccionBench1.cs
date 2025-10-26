using System;
using System.Collections.Generic;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
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
            var baseTime = DateTime.UtcNow;
            sistema = new Sistema();
            // Preparar estados disponibles
            sistema.EstadosDisponibles = new List<Estado>(201);
            for (int i = 0; i < 200; i++)
            {
                sistema.EstadosDisponibles.Add(new Estado { ambito = i % 2 == 0 ? Estado.AMBITO_SISMOGRAFO : Estado.AMBITO_OI, nombreEstado = "Otro" + i });
            }

            sistema.EstadosDisponibles.Add(new Estado { ambito = Estado.AMBITO_SISMOGRAFO, nombreEstado = Estado.ESTADO_FUERA_SERVICIO_S });
            // Preparar órdenes
            sistema.Ordenes = new List<OrdenDeInspeccion>(5000);
            dictOrdenes = new Dictionary<string, OrdenDeInspeccion>(5000);
            var estacionDummy = new EstacionSismologica(new Sismografo { identificadorSismografo = "S1" });
            for (int i = 0; i < 5000; i++)
            {
                var tiempo = baseTime.AddMinutes(-i);
                var orden = new OrdenDeInspeccion(i, tiempo, estacionDummy, null, null)
                {
                    fechaHoraFinalizacion = tiempo
                };
                sistema.Ordenes.Add(orden);
                dictOrdenes[i.ToString()] = orden;
            }

            gestor = new GestorOrdenInspeccion(sistema, null);
            // Intentar asignar el campo privado 'ordenes' de forma robusta
            var ordenesField = typeof(GestorOrdenInspeccion).GetField("ordenes", BindingFlags.NonPublic | BindingFlags.Instance);
            if (ordenesField != null)
            {
                var fieldType = ordenesField.FieldType;
                if (typeof(IList<OrdenDeInspeccion>).IsAssignableFrom(fieldType))
                {
                    ordenesField.SetValue(gestor, sistema.Ordenes);
                }
                else if (typeof(IDictionary<string, OrdenDeInspeccion>).IsAssignableFrom(fieldType))
                {
                    ordenesField.SetValue(gestor, dictOrdenes);
                }
                else if (fieldType.IsInstanceOfType(sistema.Ordenes))
                {
                    ordenesField.SetValue(gestor, sistema.Ordenes);
                }
                else if (fieldType.IsInstanceOfType(dictOrdenes))
                {
                    ordenesField.SetValue(gestor, dictOrdenes);
                }
                else
                {
                    throw new InvalidOperationException($"Campo privado 'ordenes' de tipo inesperado: {fieldType.FullName}");
                }
            }
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